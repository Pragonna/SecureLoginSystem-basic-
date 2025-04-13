using AutoMapper;
using Core.Application.Abstractions;
using Core.Application.Common;
using Core.Domain.Entities;
using Core.Infrastructure.Persistence.Users.Features.Dtos;
using Core.Infrastructure.Persistence.Users.Repository.Interfaces;

namespace Core.Infrastructure.Persistence.Users.Features.Commands.ModifyCommand
{
    public class UserModifyCommandHandler : CommandHandler<UserModifyCommand, UserDto>
    {
        private readonly IMapper mapper;
        private readonly IUserRepository userRepository;

        public UserModifyCommandHandler(
            IUserUnitOfWork unitOfWork,
            IMapper mapper,
            IUserRepository userRepository) : base(unitOfWork)
        {
            this.mapper = mapper;
            this.userRepository = userRepository;
        }
        public override async Task<Result<UserDto, Error>> ExecuteAsync(UserModifyCommand request, CancellationToken cancellationToken)
        {
            var mappedUser = mapper.Map<User>(request);
            var result = await userRepository.UpdateAsync(mappedUser);
            var userDto = mapper.Map<UserDto>(result);

            return Result<UserDto, Error>.Success(userDto);
        }
    }
}
