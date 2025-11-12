using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserManagement.Domain.Entities;
using UserManagement.Infrastructure.Constants;
using UserManagement.Infrastructure.Constants;

namespace UserManagement.Persistence.Configurations;

internal class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
{
    public void Configure(EntityTypeBuilder<RolePermission> builder)
    {
        builder.ToTable(TableNames.RolePermission);

        builder.HasKey(rp => rp.Id);

        builder.HasOne(rp => rp.Role)
               .WithMany()
               .HasForeignKey(rp => rp.RoleId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(rp => rp.Permission)
               .WithMany()
               .HasForeignKey(rp => rp.PermissionId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.Property(rp => rp.CreatedOnUtc)
               .IsRequired();

        builder.Property(rp => rp.ModifiedOnUtc)
               .IsRequired(false);

        builder.HasIndex(rp => new { rp.RoleId, rp.PermissionId }).IsUnique();
    }
}
