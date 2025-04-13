using Core.Application.Repositories;
using Core.Domain.Entities;
using Microsoft.AspNetCore.SignalR.Protocol;

namespace Core.Infrastructure.Persistence.Users.Repository.Interfaces
{
    public interface IUserRepository : IRepository<User>, IReadRepository<User>
    {
        Task AddUserRoleToUser(Guid userId, string roleName);
        Task AddSecurityDetailsOnDb(SecurityDetails securityDetails);
        Task UpdateSecurityDetailsOnDb(SecurityDetails securityDetails);
    }
}
