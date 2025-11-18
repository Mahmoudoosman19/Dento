using Case.Domain.Entities;
using Case.Infrastructure.Constants;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Case.Infrastructure.Configuration
{
    internal class CaseConfiguration : IEntityTypeConfiguration<Domain.Entities.Case>
    {

        public void Configure(EntityTypeBuilder<Domain.Entities.Case> builder)
        {
            builder.ToTable(TableName.Cases);
            builder.HasKey(c => c.Id);

            builder.Property(c => c.CaseName).IsRequired().HasMaxLength(100);

            builder.Property(c=>c.DueDate).IsRequired();
            builder.Property(c=>c.CaseType).IsRequired();
        }
    }
}
