using Core.Domain.Common.Models;
using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Infrastructure.Persistence.Users.Configurations
{
    public class UserRoleEntityTypeConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Role).IsRequired().HasMaxLength(50);

            builder.HasData(new[] {
                new UserRole (1,Roles.Admin),
                new UserRole(2,Roles.User)
            }); 
        }
    }
}
