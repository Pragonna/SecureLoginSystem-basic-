using Core.Domain.Entities;
using Core.Infrastructure.Securities.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Core.Infrastructure.Securities
{
    public class JwtTokenHelper : ITokenHelper
    {
        private DateTime _accessTokenExpiration;
        private TokenOptions _tokenOptions;
        public JwtTokenHelper(IOptions<TokenOptions> options)
        {
            _tokenOptions = options.Value;
        }
        public AccessToken GenerateAccessToken(User user, List<UserRole> roles)
        {
            _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenOptions.SecretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken jwt = CreateJwtSecurityToken(_tokenOptions, user, credentials, roles);
            JwtSecurityTokenHandler jwtSecurityTokenHandler = new();
            string? token = jwtSecurityTokenHandler.WriteToken(jwt);

            return new AccessToken(token, _accessTokenExpiration);
        }
        public string RefreshToken()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
        }

        private JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions,
                                                       User user,
                                                       SigningCredentials signingCredentials,
                                                       List<UserRole> roles)
        {
            JwtSecurityToken jwt = new(
                tokenOptions.Issuer,
                tokenOptions.Audience,
                expires: _accessTokenExpiration,
                notBefore: DateTime.Now,
                claims: SetClaims(user, roles),
                signingCredentials: signingCredentials
            );
            return jwt;
        }

        private IEnumerable<Claim> SetClaims(User user, IList<UserRole> roles)
        {
            List<Claim> claims = new();
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            claims.Add(new Claim(ClaimTypes.Email, user.Email));
            claims.Add(new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"));

            roles
                .Select(c => c.Role)
                .ToList()
                .ForEach(role => claims.Add(new Claim(ClaimTypes.Role, role)));

            return claims;
        }
    }
}
