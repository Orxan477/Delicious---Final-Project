using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant.Core.Models;

namespace Restaurant.Data.Configurations
{
    public class FullOrderConfiguration : IEntityTypeConfiguration<FullOrder>
    {
        public void Configure(EntityTypeBuilder<FullOrder> builder)
        {
            //builder.Property(x => x.AppUserId).IsRequired();
            builder.Property(x => x.Total).IsRequired();
            builder.Property(x=>x.CreatedAt).IsRequired();
            builder.Property(x=>x.Status).IsRequired();
            builder.Property(x=>x.IsDeleted).HasDefaultValue(false);
        }
    }
}
