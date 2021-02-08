using Microsoft.EntityFrameworkCore;
using LearnApiUsingMiddleware.Models;

namespace LearnApiUsingMiddleware.Models
{
    public class MovieContext : DbContext
    {
        public MovieContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<ShoppingItem> ShoppingContext { get; set; }

    }
}
