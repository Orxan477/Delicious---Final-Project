using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant.Core.Models;

namespace Restaurant.Data.Configurations
{
    internal class SpecialConfiguration : IEntityTypeConfiguration<Special>
    {
        public void Configure(EntityTypeBuilder<Special> builder)
        {
            builder.Property(x => x.FoodName).HasMaxLength(50).IsRequired();
            builder.Property(x=>x.PropHead).HasMaxLength(100).IsRequired();
            builder.Property(x => x.PropContent).HasMaxLength(500).IsRequired();
            builder.Property(x => x.PropContentItalic).HasMaxLength(255).IsRequired();
            //builder.Property(x=>x.MenuImage).IsRequired();
        }
    }
}
