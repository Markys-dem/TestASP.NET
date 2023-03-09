using Mark.Models;
using Microsoft.EntityFrameworkCore;

namespace Mark.Dao
{
    public class ApplicationDbContext:DbContext
    {
        public DbSet<Category> categories { get; set; }
        public DbSet<Toy> toys { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    }
}
