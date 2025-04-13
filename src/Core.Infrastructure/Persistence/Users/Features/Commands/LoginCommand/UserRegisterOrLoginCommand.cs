using Core.Application.Abstractions;
using Core.Infrastructure.Persistence.Users.Features.Dtos;

namespace Core.Infrastructure.Persistence.Users.Features.Commands.LoginCommand
{
    public record UserRegisterOrLoginCommand(string email, string ipAddress) : ICommand<UserDto> { }

}
