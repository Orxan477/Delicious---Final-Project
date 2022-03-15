using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant.Core.Models;

namespace Restaurant.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(x => x.ProductId).IsRequired();
            builder.Property(x => x.TypeId).IsRequired();
            builder.Property(x => x.FullOrderId).IsRequired();
            builder.Property(x => x.Count).IsRequired();
            builder.Property(x=>x.Price).IsRequired();
            builder.Property(x => x.IsDeleted).HasDefaultValue(false);
        }
    }
}
