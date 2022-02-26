using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant.Core.Models;

namespace Restaurant.Data.Configurations
{
    internal class AboutImageConfiguration : IEntityTypeConfiguration<AboutImage>
    {
        public void Configure(EntityTypeBuilder<AboutImage> builder)
        {
            builder.Property(x => x.Image).IsRequired();
        }
    }
}
