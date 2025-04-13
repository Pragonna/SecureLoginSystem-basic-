using Core.Application.Abstractions;
using Core.Application.Common;
using Core.Infrastructure.Persistence.Users.Features.Dtos;
using Core.Infrastructure.Persistence.Users.Features.Manager;
using Core.Infrastructure.Persistence.Users.Repository.Interfaces;
using EventBus.EventBus.Base.Abstraction;

namespace Core.Infrastructure.Persistence.Users.Features.Commands.VerifyOTPCommand
{
    public class OTPValidateCommandHandler : CommandHandler<OTPValidateCommand, VerificationOTPDto>
    {
        private readonly IUserManager userManager;
        public OTPValidateCommandHandler(
            IEventBus eventBus,
            IUserUnitOfWork unitOfWork,
            IUserManager userManager) : base(unitOfWork, eventBus)
        {
            this.userManager = userManager;
        }
        public override async Task<Result<VerificationOTPDto, Error>> ExecuteAsync(OTPValidateCommand request, CancellationToken cancellationToken)
        {
            return await userManager.VerifyOTP(request.email,request.OTP);
        }
    }
}
