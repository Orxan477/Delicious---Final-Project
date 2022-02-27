using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant.Core.Models;

namespace Restaurant.Data.Configurations
{
    internal class SectionContentConfiguration : IEntityTypeConfiguration<SectionContent>
    {
        public void Configure(EntityTypeBuilder<SectionContent> builder)
        {
            builder.Property(x => x.Key).IsRequired();
            builder.Property(x => x.Content).HasMaxLength(255);
        }
    }
}
