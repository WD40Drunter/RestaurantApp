using Microsoft.EntityFrameworkCore;

namespace RestaurantApp.Model
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<Adress> Adresses { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
