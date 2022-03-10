using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant.Core.Models;
using System;

namespace Restaurant.Data.Configurations
{
    internal class ChooseRestaurantConfiguration : IEntityTypeConfiguration<ChooseRestaurant>
    {
        public void Configure(EntityTypeBuilder<ChooseRestaurant> builder)
        {
            builder.Property(x => x.CardHead).HasMaxLength(20).IsRequired();
            builder.Property(x => x.CardContent).HasMaxLength(255).IsRequired();

        }
    }
}
