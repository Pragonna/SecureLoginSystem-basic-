using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Infrastructure.Persistence.Users.Configurations
{
    public class UserAndUserRoleTypeConfiguration : IEntityTypeConfiguration<UserAndUserRole>
    {
        public void Configure(EntityTypeBuilder<UserAndUserRole> builder)
        {
            builder.HasKey(ur => new { ur.UserId, ur.RoleId });

            builder
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserAndUserRoles)
                .HasForeignKey(ur => ur.UserId);

            builder
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserAndUserRoles)
                .HasForeignKey(ur => ur.RoleId);
        }
    }
}
