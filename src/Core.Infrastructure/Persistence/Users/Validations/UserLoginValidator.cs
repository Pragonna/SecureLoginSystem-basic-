using Core.Infrastructure.Persistence.Users.Features.Commands.LoginCommand;
using FluentValidation;

namespace Core.Infrastructure.Persistence.Users.Validations
{
    public class UserLoginValidator : AbstractValidator<UserRegisterOrLoginCommand>
    {
        public UserLoginValidator()
        {
            RuleFor(p => p.email).NotEmpty().EmailAddress();
        }
    }
}
