using Core.Application.Abstractions;
using Core.Infrastructure.Persistence.Users.Features.Dtos;

namespace Core.Infrastructure.Persistence.Users.Features.Commands.VerifyRefreshTokenCommand
{
    public record VerifyRefreshTokenCommand(string email, string refreshToken,string ipAddress) : ICommand<TokenModelDto>
    {
    }
}
