using Core.Application.Repositories;
using Core.Domain.Entities;
using Core.Infrastructure.Persistence.Users.Context;
using Core.Infrastructure.Persistence.Users.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Core.Infrastructure.Persistence.Users.Repository
{
    public class UserRepository : BaseRepository<User, UserDbContext>, IUserRepository
    {
        private readonly UserDbContext dbContext;

        public UserRepository(UserDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task AddUserRoleToUser(Guid userId, string roleName)
        {
            var user = await dbContext.Users.FindAsync(userId);
            if (user == null)
                throw new Exception("User not found");

            var _role = await dbContext.UserRoles.FirstOrDefaultAsync(r => r.Role == roleName);

            var userRole = new UserAndUserRole
            {
                UserId = userId,
                RoleId = _role.Id,
                Role = _role,
                User = user
            };

            await _context.UserAndUserRoles.AddAsync(userRole);
        }
        public async Task AddSecurityDetailsOnDb(SecurityDetails securityDetails)
        {
            if (securityDetails == null) throw new ArgumentNullException();

            await dbContext.SecurityDetails.AddAsync(securityDetails);
        }
   
        public Task UpdateSecurityDetailsOnDb(SecurityDetails securityDetails)
        {
            if (securityDetails == null) throw new ArgumentNullException();

            dbContext.SecurityDetails.Update(securityDetails);

            return Task.CompletedTask;
        }
    }
}
