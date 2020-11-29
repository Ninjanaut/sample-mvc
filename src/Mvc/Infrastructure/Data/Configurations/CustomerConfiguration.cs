using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mvc.Infrastructure.Data.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customer", schema: "dbo");
            builder.HasKey("Id");

            // FullName is value object,
            // therefore we have to specify column mapping.
            builder
                .OwnsOne(o => o.FullName)
                .Property(p => p.FirstName)
                .HasColumnName("FirstName");

            builder
                .OwnsOne(o => o.FullName)
                .Property(p => p.LastName)
                .HasColumnName("LastName");
        }
    }
}
