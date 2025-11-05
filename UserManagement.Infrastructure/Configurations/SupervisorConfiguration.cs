using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserManagement.Infrastructure.Constants;

namespace UserManagement.Infrastructure.Configurations
{
    internal class SupervisorConfiguration : IEntityTypeConfiguration<Domain.Entities.Supervisor>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Supervisor> builder)
        {
            builder.ToTable(TableNames.Supervisors);

            builder.HasKey(s => s.Id);

            builder.HasOne(c => c.User)
                   .WithMany()
                   .HasForeignKey(c => c.UserId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
