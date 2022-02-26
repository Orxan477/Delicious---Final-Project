using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant.Core.Models;

namespace Restaurant.Data.Configurations
{
    internal class PizzaPriceConfiguration : IEntityTypeConfiguration<PizzaPrice>
    {
        public void Configure(EntityTypeBuilder<PizzaPrice> builder)
        {
            builder.Property(x => x.Value).HasDefaultValue(0).IsRequired();
            builder.Property(x => x.Content).HasMaxLength(50).IsRequired();
        }
    }
}
