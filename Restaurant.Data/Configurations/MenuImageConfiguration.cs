using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant.Core.Models;

namespace Restaurant.Data.Configurations
{
    internal class MenuImageConfiguration : IEntityTypeConfiguration<MenuImage>
    {
        public void Configure(EntityTypeBuilder<MenuImage> builder)
        {
            builder.Property(x => x.Image).IsRequired();
        }
    }
}
