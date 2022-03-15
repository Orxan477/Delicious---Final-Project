using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant.Core.Models;

namespace Restaurant.Data.Configurations
{
    public class BillingAdressConfiguration : IEntityTypeConfiguration<BillingAdress>
    {
        public void Configure(EntityTypeBuilder<BillingAdress> builder)
        {
            //builder.Property(x => x.AppUserId).IsRequired();
            builder.Property(x => x.Adress).HasMaxLength(70).IsRequired();
            builder.Property(x => x.Phone).HasMaxLength(14).IsRequired();
        }
    }
}
