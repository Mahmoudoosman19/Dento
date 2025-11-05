using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserManagement.Domain.Entites;
using UserManagement.Domain.Entities;
using UserManagement.Domain.Enums;
using UserManagement.Infrastructure.Constants;

namespace UserManagement.Infrastructure.Configurations;

internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable(TableNames.Users);

        builder.HasKey(u => u.Id);

        builder.Property(u => u.FullNameEn)
               .HasMaxLength(100);

        builder.Property(u => u.FullNameAr)
               .HasMaxLength(100);

        builder.Property(u => u.Status)
               .IsRequired();

        builder.Property(u => u.CreatedOnUtc)
               .IsRequired();

        builder.Property(u => u.ModifiedOnUtc)
               .IsRequired(false);

        builder.HasOne(u => u.Role)
               .WithMany()
               .HasForeignKey(u => u.RoleId)
               .OnDelete(DeleteBehavior.SetNull);

        builder.Property(u => u.BirthDate)
               .IsRequired(false);

        builder.Property(u => u.Gender)
                           .IsRequired()
                           .HasConversion(
                               v => v.ToString(),
                               v => (UserGender)Enum.Parse(typeof(UserGender), v))
                           .HasMaxLength(10);

        builder.HasIndex(u => u.Email).IsUnique();
        builder.HasIndex(u => u.PhoneNumber).IsUnique();
        
        builder.HasOne(u => u.Otp)
               .WithOne()
               .OnDelete(DeleteBehavior.Cascade);

        builder.Property(u => u.DeletedAt)
               .IsRequired(false);

        builder.Property(u => u.RestoredAt)
                .IsRequired(false);

        builder.Property(u => u.IsDeleted)
                .IsRequired()
                .HasDefaultValue(false);
    }
}
