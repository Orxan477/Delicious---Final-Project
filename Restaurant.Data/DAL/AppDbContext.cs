using Microsoft.EntityFrameworkCore;
using Restaurant.Core.Models;
using Restaurant.Data.Configurations;

namespace Restaurant.Data.DAL
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext>options):base(options) {}

        public DbSet<About> Abouts { get; set; }
        public DbSet<AboutOption> AboutOptions { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ChooseRestaurant> ChooseRestaurants { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<HomeIntro> HomeIntros { get; set; }
        public DbSet<MenuImage> MenuImages { get; set; }
        public DbSet<PizzaPrice> PizzaPrices { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Product> Prouducts { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<RestaurantPhotos> RestaurantPhotos { get; set; }
        public DbSet<Special> Specials { get; set; }
        public DbSet<Subscribe> Subscribes { get; set; }
        public DbSet<Team> Teams { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AboutConfiguration());
            modelBuilder.ApplyConfiguration(new AboutOptionConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new ChooseRestaurantConfiguration());
            modelBuilder.ApplyConfiguration(new FeedbackConfiguration());
            modelBuilder.ApplyConfiguration(new HomeIntroConfiguration());
            modelBuilder.ApplyConfiguration(new MenuImageConfiguration());
            modelBuilder.ApplyConfiguration(new PizzaPriceConfiguration());
            modelBuilder.ApplyConfiguration(new PositionConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new ReservationConfiguration());
            modelBuilder.ApplyConfiguration(new RestaurantPhotosConfiguration());
            modelBuilder.ApplyConfiguration(new SpecialConfiguration());
            modelBuilder.ApplyConfiguration(new SubscribeConfiguration());
            modelBuilder.ApplyConfiguration(new TeamConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
