using Core.Application.Abstractions;
using Core.Infrastructure.Persistence.Users.Features.Dtos;
using Microsoft.AspNetCore.Http;

namespace Core.Infrastructure.Persistence.Users.Features.Commands.ModifyCommand
{
    public record UserModifyCommand(
        string firstName,
        string lastName,
        string dateOfBirth,
        string gender,
        string country,
        IFormFile image = null,
        string bio = null) : ICommand<UserDto>
    {
    }
}
