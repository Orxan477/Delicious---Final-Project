using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant.Core.Models;

namespace Restaurant.Data.Configurations
{
    public class SettingConfiguration : IEntityTypeConfiguration<Setting>
    {
        public void Configure(EntityTypeBuilder<Setting> builder)
        {
            builder.Property(x=>x.Key).IsRequired();
            builder.Property(x=>x.Value).IsRequired();
            builder.Property(x=>x.Type).IsRequired();
        }
    }
}
