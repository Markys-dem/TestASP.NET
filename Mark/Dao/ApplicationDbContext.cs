using Mark.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Mark.Dao
{
    public class ApplicationDbContext:IdentityDbContext
    {
        public DbSet<Category> categories { get; set; }
        public DbSet<Toy> toys { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<Basket> baskets { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    }
}
