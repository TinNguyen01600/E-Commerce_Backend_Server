using Microsoft.EntityFrameworkCore;
using Npgsql;
using Server.Core.Entity;
using Server.Core.src.Entity;
using Server.Core.src.ValueObject;

namespace Server.Infrastructure.src.Database
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; } // table `Users` -> `users`
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<OrderProduct> OrderProducts{ get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        static AppDbContext()
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresEnum<Role>();
            modelBuilder.HasPostgresEnum<Status>();

            modelBuilder.Entity<User>(e =>
            {
                e.HasData(SeedingData.GetUsers());
                e.HasIndex(u => u.Email).IsUnique();
            });
            // -----------------------------------------------------------------------------------------------

            modelBuilder.Entity<Category>(e =>
            {
                e.HasData(SeedingData.GetCategories());
                e.HasIndex(e => e.Name).IsUnique();
            });
            // -----------------------------------------------------------------------------------------------

            modelBuilder.Entity<Product>(e =>
            {
                e.HasData(SeedingData.Products);
                e.HasIndex(p => p.Name).IsUnique();
            });
            // -----------------------------------------------------------------------------------------------

            modelBuilder.Entity<ProductImage>(e =>
            {
                e.HasData(SeedingData.GetProductImages());
            });

            base.OnModelCreating(modelBuilder);
            // -----------------------------------------------------------------------------------------------

            // modelBuilder.Entity<Order>(e =>
            // {
            //     e.HasData(SeedingData.GetOrders());
            // });

            // base.OnModelCreating(modelBuilder);
            // -----------------------------------------------------------------------------------------------

            // modelBuilder.Entity<OrderProduct>(e =>
            // {
            //     e.HasData(SeedingData.GetOrderProducts());
            // });

            // base.OnModelCreating(modelBuilder);
            // -----------------------------------------------------------------------------------------------

            // modelBuilder.Entity<Review>(e =>
            // {
            //     e.HasData(SeedingData.GetReviews());
            // });

            // base.OnModelCreating(modelBuilder);
        }
    }
}