using Core.Application.Common;
using Core.Infrastructure.Persistence.Users.Features.Dtos;
using Core.Infrastructure.Persistence.Users.Repository.Interfaces;

namespace Core.Infrastructure.Persistence.Users.Features.Manager
{
    public interface IUserManager
    {
        Task<Result<UserDto, Error>> Login(string email, string ipAddress,IUserUnitOfWork uOw);
        Task<Result<VerificationOTPDto, Error>> VerifyOTP(string email, string OTPCode);
        Task<Result<TokenModelDto, Error>> VerifyRefreshToken(string email,string refreshToken, string ipAddress);
    }
}
