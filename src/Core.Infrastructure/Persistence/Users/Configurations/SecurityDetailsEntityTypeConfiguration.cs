using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Infrastructure.Persistence.Users.Configurations
{
    public class SecurityDetailsEntityTypeConfiguration : IEntityTypeConfiguration<SecurityDetails>
    {
        public void Configure(EntityTypeBuilder<SecurityDetails> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.OTPCode)
                .IsRequired()
                .HasMaxLength(6);

            builder.Property(x => x.OTPExpiryDate)
                .IsRequired();

            builder.Property(x => x.RefreshToken)
                .HasMaxLength(500);

            builder.Property(x => x.IPAddress)
                .HasMaxLength(100);

            builder.Property(x => x.CreatedDate)
                .IsRequired();

            builder.HasOne(sd => sd.User)
                .WithOne(u => u.SecurityDetails)
                .HasForeignKey<SecurityDetails>(sd => sd.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
