using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant.Core.Models;

namespace Restaurant.Data.Configurations
{
    internal class RestaurantPhotosConfiguration : IEntityTypeConfiguration<RestaurantPhotos>
    {
        public void Configure(EntityTypeBuilder<RestaurantPhotos> builder)
        {
            builder.Property(x => x.Image).IsRequired();
        }
    }
}
