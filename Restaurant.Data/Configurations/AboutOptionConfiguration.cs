using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant.Core.Models;

namespace Restaurant.Data.Configurations
{
    internal class AboutOptionConfiguration : IEntityTypeConfiguration<AboutOption>
    {
        public void Configure(EntityTypeBuilder<AboutOption> builder)
        {
            builder.Property(x => x.Option).IsRequired();
        }
    }
}
