using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant.Core.Models;

namespace Restaurant.Data.Configurations
{
    internal class FeedbackConfiguration : IEntityTypeConfiguration<Feedback>
    {
        public void Configure(EntityTypeBuilder<Feedback> builder)
        {
            builder.Property(x=>x.FullName).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Comment).HasMaxLength(255).IsRequired();
            builder.Property(x => x.Image).IsRequired();
            builder.Property(x => x.Position).IsRequired();
        }
    }
}
