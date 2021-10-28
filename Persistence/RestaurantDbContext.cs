using DataServerRestaurant.Model;
using Microsoft.EntityFrameworkCore;

namespace DataServerRestaurant.Persistence
{
    public class RestaurantDbContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Menu> Menus { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=1234;SearchPath=restaurants_db;")
                .UseSnakeCaseNamingConvention();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { 
            modelBuilder.Entity<Order>().Property(o => o.OrderId).UseIdentityAlwaysColumn();
            modelBuilder.Entity<Customer>().Property(c => c.CustomerId).UseIdentityAlwaysColumn();
            modelBuilder.Entity<Menu>().Property(m => m.MenuId).UseIdentityAlwaysColumn();
            modelBuilder.Entity<Order>().HasOne(o => o.Customer);
            modelBuilder.Entity<Order>().HasOne(o => o.Menu);
        }
    }

}