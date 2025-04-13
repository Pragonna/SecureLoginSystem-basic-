using Core.Domain.Entities;
using Core.Infrastructure.Securities.Models;

namespace Core.Infrastructure.Securities
{
    public interface ITokenHelper
    {
        AccessToken GenerateAccessToken(User user,List<UserRole>roles);
        string RefreshToken();
    }
}
