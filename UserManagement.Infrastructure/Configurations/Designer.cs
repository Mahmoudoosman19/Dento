using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Infrastructure.Constants;

namespace UserManagement.Infrastructure.Configurations
{
    internal class DesignerConfiguration : IEntityTypeConfiguration<Domain.Entities.Designer>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Designer> builder)
        {
            builder.ToTable(TableNames.Designer);
            builder.HasKey(s => s.Id);
            builder.HasOne(c => c.User)
                   .WithMany()
                   .HasForeignKey(c => c.UserId)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
