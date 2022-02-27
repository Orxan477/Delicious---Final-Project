using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant.Core.Models;

namespace Restaurant.Data.Configurations
{
    internal class SectionHeadConfiguration : IEntityTypeConfiguration<SectionHead>
    {
        public void Configure(EntityTypeBuilder<SectionHead> builder)
        {
            builder.Property(x => x.Key).IsRequired();
            builder.Property(x => x.Head).HasMaxLength(100).IsRequired();
        }
    }
}
