using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Logging.Abstractions;

namespace Core.Infrastructure.Persistence.Users.Configurations
{
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Email).IsRequired().HasMaxLength(50);
            builder.Property(x => x.FirstName).HasMaxLength(50);
            builder.Property(x => x.LastName).HasMaxLength(50);
            builder.Property(x => x.DateOfBirth);
            builder.Property(x => x.Country).HasMaxLength(50);
            builder.Property(x => x.Bio).HasMaxLength(500);
            builder.HasOne(x => x.Image).WithOne(x=>x.User)
                .HasForeignKey<Image>(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.SecurityDetails)
                .WithOne(x=>x.User)
                .HasForeignKey<SecurityDetails>(c=>c.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
