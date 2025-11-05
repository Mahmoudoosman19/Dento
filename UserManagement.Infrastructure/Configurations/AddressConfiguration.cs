using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserManagement.Domain.Entites;
using UserManagement.Infrastructure.Constants;



namespace UserManagement.Infrastructure.Configurations
{
    internal class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.ToTable(TableNames.Address);

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
               .IsRequired()
               .HasMaxLength(100);

            builder.Property(p => p.City)
                .IsRequired();

            builder.Property(p => p.AddressName)
                .IsRequired();

            builder.Property(p => p.PhoneNumber)
                .HasMaxLength(11);

            builder.Property(p => p.CreatedOnUtc)
                .IsRequired();

            builder.Property(p => p.ModifiedOnUtc);


            builder.Property(p => p.UserId)
                .IsRequired();

            builder.Property(p => p.Floor);

           
        }
    }
}
