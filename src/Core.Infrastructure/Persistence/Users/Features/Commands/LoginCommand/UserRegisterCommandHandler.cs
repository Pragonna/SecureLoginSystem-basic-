using Core.Application.Abstractions;
using Core.Application.Common;
using Core.Infrastructure.Persistence.Users.Features.Dtos;
using Core.Infrastructure.Persistence.Users.Features.Manager;
using Core.Infrastructure.Persistence.Users.Repository.Interfaces;
using EventBus.EventBus.Base.Abstraction;

namespace Core.Infrastructure.Persistence.Users.Features.Commands.LoginCommand
{
    public class UserRegisterCommandHandler : CommandHandler<UserRegisterOrLoginCommand, UserDto>
    {
        private readonly IUserUnitOfWork unitOfWork;
        private readonly IUserManager userManager;

        public UserRegisterCommandHandler(
            IEventBus eventBus,
            IUserUnitOfWork unitOfWork,
            IUserManager userManager) : base(unitOfWork, eventBus)
        {
            this.unitOfWork = unitOfWork;
            this.userManager = userManager;
        }

        public override async Task<Result<UserDto, Error>> ExecuteAsync(UserRegisterOrLoginCommand request, CancellationToken cancellationToken)
        {
            return await userManager.Login(request.email, request.ipAddress, unitOfWork);
        }
    }
}