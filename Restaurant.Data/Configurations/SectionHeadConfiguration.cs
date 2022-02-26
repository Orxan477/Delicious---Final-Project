using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant.Core.Models;

namespace Restaurant.Data.Configurations
{
    internal class SectionHeadConfiguration : IEntityTypeConfiguration<SectionHead>
    {
        public void Configure(EntityTypeBuilder<SectionHead> builder)
        {
            builder.Property(x => x.Head).HasMaxLength(50).IsRequired();
            builder.Property(x=>x.Content).HasMaxLength(255).IsRequired();
        }
    }
}
