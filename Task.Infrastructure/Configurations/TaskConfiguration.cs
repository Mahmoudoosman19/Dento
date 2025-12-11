using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.Domain.Entities;

namespace Task.Infrastructure.Configurations
{
    internal class TaskConfiguration : IEntityTypeConfiguration<CaseTask>
    {
        public void Configure(EntityTypeBuilder<CaseTask> builder)
        {
            builder.ToTable("CaseTasks");

            // ---------- Key ----------
            builder.HasKey(t => t.Id);

            // ---------- Properties ----------

            builder.Property(t => t.CaseId)
                   .IsRequired();

            builder.Property(t => t.DesignerId)
                   .IsRequired();


   

            builder.Property(t => t.Status)
                   .HasConversion<int>()       // Store Enum as int
                   .IsRequired();

            builder.Property(t => t.Notes)
                   .HasMaxLength(2000)
                   .IsRequired(false);

            builder.Property(t => t.CreatedOnUtc)
                   .IsRequired();

            builder.Property(t => t.ModifiedOnUtc)
                   .IsRequired(false);


            // ------------ Indexes (Recommended) ------------

            builder.HasIndex(t => t.CaseId);

            builder.HasIndex(t => t.DesignerId);

            builder.HasIndex(t => t.Status);
        }
    }
}
