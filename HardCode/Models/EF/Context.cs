using Microsoft.EntityFrameworkCore;

namespace HardCode.Models.EF
{
    public class Context:DbContext
    {
        private readonly string _connectionString;
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public Context(DbContextOptions<Context> options)
             : base(options)
        {
        }

    }
}
