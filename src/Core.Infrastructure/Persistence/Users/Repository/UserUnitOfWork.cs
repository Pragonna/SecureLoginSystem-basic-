using Core.Application.Repositories;
using Core.Infrastructure.Persistence.Users.Context;
using Core.Infrastructure.Persistence.Users.Repository.Interfaces;

namespace Core.Infrastructure.Persistence.Users.Repository
{
    public class UserUnitOfWork: UnitOfWork<UserDbContext>, IUserUnitOfWork
    {
        public UserUnitOfWork(UserDbContext context) : base(context)
        {
        }
    }
}
