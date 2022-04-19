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
            builder.Property(x=>x.InformationTabHead).HasMaxLength(100).IsRequired();
            builder.Property(x => x.InformationTabContent).HasMaxLength(500);
            builder.Property(x => x.InformationTabItalicContent).HasMaxLength(255);
            builder.Property(x => x.IsDeleted).HasDefaultValue(false);
        }
    }
}
