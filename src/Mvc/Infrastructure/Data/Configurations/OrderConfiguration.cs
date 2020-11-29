using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mvc.Infrastructure.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Order", schema: "dbo");
            builder.HasKey("Id");
            builder.HasOne(x => x.Customer);
            builder.HasMany(x => x.OrderItems).WithOne();

            // Order items are encapsulated in readonly collection,
            // therefore we have to map EF to backing field.
            builder.Metadata
                .FindNavigation(nameof(Order.OrderItems))
                .SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
