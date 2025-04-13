using Core.Application.Abstractions;
using Core.Application.Common;
using Core.Infrastructure.Persistence.Users.Features.Dtos;
using Core.Infrastructure.Persistence.Users.Features.Manager;
using Core.Infrastructure.Persistence.Users.Repository.Interfaces;

namespace Core.Infrastructure.Persistence.Users.Features.Commands.VerifyRefreshTokenCommand
{
    public class VerifyRefreshTokenCommandHandler : CommandHandler<VerifyRefreshTokenCommand, TokenModelDto>
    {
        private readonly IUserManager userManager;
        public VerifyRefreshTokenCommandHandler(IUserManager userManager,IUserUnitOfWork uOw):base(uOw)
        {
            this.userManager = userManager;
        }
        public override async Task<Result<TokenModelDto, Error>> ExecuteAsync(VerifyRefreshTokenCommand request, CancellationToken cancellationToken)
        {
            return await userManager.VerifyRefreshToken(request.email, request.refreshToken, request.ipAddress);
        }
    }
}
