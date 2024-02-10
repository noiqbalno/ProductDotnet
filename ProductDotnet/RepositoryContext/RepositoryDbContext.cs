using Microsoft.EntityFrameworkCore;
using ProductDotnet.Models;

namespace ProductDotnet.RepositoryContext
{
    public class RepositoryDbContext : DbContext
    {
        public RepositoryDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
