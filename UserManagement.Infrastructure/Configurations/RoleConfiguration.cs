using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserManagement.Domain.Entities;
using UserManagement.Infrastructure.Constants;

namespace UserManagement.Infrastructure.Configurations;

internal class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable(TableNames.Roles);

        builder.HasKey(r => r.Id);

        builder.Property(r => r.NameAr)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(r => r.NameEn)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(r => r.CreatedOnUtc)
            .IsRequired();

        builder.Property(r => r.ModifiedOnUtc)
            .IsRequired(false);

        builder.HasIndex(r => r.NameAr).IsUnique();
        builder.HasIndex(r => r.NameEn).IsUnique();
    }
}
