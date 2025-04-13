using Core.Application.Abstractions;
using Core.Infrastructure.Persistence.Users.Features.Dtos;

namespace Core.Infrastructure.Persistence.Users.Features.Commands.VerifyOTPCommand
{
    public record OTPValidateCommand(string email,string OTP) : ICommand<VerificationOTPDto> { }
}
