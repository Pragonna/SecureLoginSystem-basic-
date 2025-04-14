using AutoMapper;
using Core.Application.Common;
using Core.Domain.Common.Models;
using Core.Domain.Entities;
using Core.Infrastructure.Persistence.Users.Features.Dtos;
using Core.Infrastructure.Persistence.Users.Features.Events;
using Core.Infrastructure.Persistence.Users.Repository.Interfaces;
using Core.Infrastructure.Securities;
using Core.Infrastructure.Securities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Runtime.InteropServices;

namespace Core.Infrastructure.Persistence.Users.Features.Manager
{
    public class UserManager(
        IUserRepository userRepository,
        IMapper mapper,
        ITokenHelper tokenHelper,
        IOptions<TokenOptions> tokenOptions) : IUserManager
    {
        private readonly TokenOptions _tokenOptions = tokenOptions.Value;

        public async Task<Result<UserDto, Error>> Login(string email, string ipAddress, IUserUnitOfWork uOw)
        {
            var users = await userRepository.GetAllAsync(
               expression: u => u.Email == email,
               include: query => query.Include(c => c.UserAndUserRoles)
                                      .ThenInclude(ur => ur.Role)
                                      .Include(c => c.SecurityDetails));

            User? _user = users.FirstOrDefault();

            if (!users.Any())
            {
                _user = await userRepository.AddAsync(new User(email));
                _user.SecurityDetails = new SecurityDetails
                {
                    UserId = _user.Id,
                    OTPCode = new Random().Next(100000, 999999).ToString(),
                    OTPExpiryDate = DateTime.UtcNow.AddMinutes(10),
                    IPAddress = ipAddress
                };
                await userRepository.AddUserRoleToUser(_user.Id, Roles.User);
                await userRepository.AddSecurityDetailsOnDb(_user.SecurityDetails);

                await uOw.CommitAsync();
            }
            List<UserRole>? roles = _user?.UserAndUserRoles.Select(x => x.Role).ToList();
            //create token
            var accessToken = tokenHelper.GenerateAccessToken(_user, roles);
            var refreshToken = tokenHelper.RefreshToken();
            _user.SecurityDetails.OTPCode = new Random().Next(100000, 999999).ToString();
            _user.SecurityDetails.OTPExpiryDate = DateTime.UtcNow.AddMinutes(10);
            _user.SecurityDetails.IPAddress = ipAddress;

            await userRepository.UpdateSecurityDetailsOnDb(_user.SecurityDetails);

            var userLoginDto = mapper.Map<UserDto>(_user);
            // userLoginDto.HasUser = true;

            return Result<UserDto, Error>.Success(userLoginDto, new SendVerificationEmailIntegrationEvent(userLoginDto.Email, _user.SecurityDetails));
        }
        public async Task<Result<VerificationOTPDto, Error>> VerifyOTP(string email, string OTPCode)
        {
            var users = await userRepository.GetAllAsync(
                expression: u => u.Email == email && u.SecurityDetails.OTPCode == OTPCode && u.SecurityDetails.OTPExpiryDate > DateTime.UtcNow,
                include: i => i.Include(u => u.SecurityDetails)!
                                .Include(u => u.UserAndUserRoles)
                                .ThenInclude(x => x.Role));
            User? user = users.FirstOrDefault();

            if (!users.Any())
            {
                return Result<VerificationOTPDto, Error>.Failure(Error.BadRequest());
            }
            var roles = user?.UserAndUserRoles.Select(x => x.Role).ToList();

            user.SecurityDetails.RefreshToken = tokenHelper.RefreshToken();
            user.SecurityDetails.RefreshTokenExpirationDate = DateTime.UtcNow.AddDays(_tokenOptions.RefreshTokenTTL);
            user.SecurityDetails.OTPCode = string.Empty;
            var accessToken = tokenHelper.GenerateAccessToken(user, roles);
            await userRepository.UpdateSecurityDetailsOnDb(user.SecurityDetails);

            return Result<VerificationOTPDto, Error>.Success(new VerificationOTPDto
            {
                IsValid = true,
                RefreshToken = user.SecurityDetails.RefreshToken,
                RefreshTokenExpirationDate = user.SecurityDetails.RefreshTokenExpirationDate,
                AccessToken = accessToken.token,
                AccessTokenExpiryDate = accessToken.expiration,
                Email = user.Email
            });
        }

        // When the client attempts to access the same endpoint within the defined time limit, the previously stored refresh token—kept either in a cookie or a more secure location—must be validated. This mechanism is used to obtain a new access token
        public async Task<Result<TokenModelDto, Error>> VerifyRefreshToken(string email, string refreshToken, string ipAddress)
        {
            var users = await userRepository.GetAllAsync(
                expression: u => u.Email == email && u.SecurityDetails.RefreshToken == refreshToken && u.SecurityDetails.RefreshTokenExpirationDate > DateTime.UtcNow,
                include: i => i.Include(u => u.SecurityDetails)!);
            User? user = users.FirstOrDefault();

            if (!users.Any())
            {
                return Result<TokenModelDto, Error>.Failure(Error.BadRequest("Expiration session"));
            }
            var roles = user?.UserAndUserRoles.Select(x => x.Role).ToList();
            var _refreshToken = tokenHelper.RefreshToken();
            user.SecurityDetails.RefreshToken = _refreshToken;
            user.SecurityDetails.RefreshTokenExpirationDate = DateTime.UtcNow.AddDays(_tokenOptions.RefreshTokenTTL);
            user.SecurityDetails.IPAddress = ipAddress;

            await userRepository.UpdateSecurityDetailsOnDb(user.SecurityDetails);

            return Result<TokenModelDto, Error>.Success(new TokenModelDto { RefreshToken = _refreshToken, AccessToken = tokenHelper.GenerateAccessToken(user, roles).token });
        }
    }
}
