using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant.Core.Models;

namespace Restaurant.Data.Configurations
{
    internal class AboutConfiguration : IEntityTypeConfiguration<About>
    {
        public void Configure(EntityTypeBuilder<About> builder)
        {
            builder.Property(x => x.Head).IsRequired().HasMaxLength(100);
            builder.Property(x => x.NormalContent).IsRequired().HasMaxLength(200);
            builder.Property(x => x.ItalicContent).HasMaxLength(100);
            builder.Property(x => x.NormalContent2).HasMaxLength(255);
            builder.Property(x => x.Image).IsRequired();


        }
    }
}
