using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant.Core.Models;

namespace Restaurant.Data.Configurations
{
    internal class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.Property(x => x.FullName).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Email).HasMaxLength(255);
            builder.Property(x => x.Number).IsRequired().HasMaxLength(14); 
            builder.Property(x => x.Date).IsRequired();
            builder.Property(x => x.PeopleCount).HasDefaultValue(0).IsRequired();
            builder.Property(x => x.Message).HasMaxLength(255);
            builder.Property(x => x.IsDeleted).HasDefaultValue(false);
        }
    }
}
