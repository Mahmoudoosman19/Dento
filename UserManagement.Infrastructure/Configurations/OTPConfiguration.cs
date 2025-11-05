using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserManagement.Domain.Entities;
using UserManagement.Infrastructure.Constants;

namespace UserManagement.Infrastructure.Configurations
{
    internal class OTPConfiguration : IEntityTypeConfiguration<OTP>
    {
        public void Configure(EntityTypeBuilder<OTP> builder)
        {
            builder.ToTable(TableNames.OTPs);

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Code)
               .IsRequired()
               .HasMaxLength(10);

            builder.Property(p => p.Type)
                .IsRequired();

            builder.Property(p => p.ExpireOn)
                .IsRequired();

            builder.Property(p => p.IsUsed)
                .IsRequired();

            builder.Property(p => p.Purpose)
                .HasMaxLength(250);

            builder.Property(p => p.CreatedOnUtc)
                .IsRequired();

            builder.Property(p => p.ModifiedOnUtc);

            builder.HasIndex(p => p.Code)
                .IsUnique();
        }
    }
}
