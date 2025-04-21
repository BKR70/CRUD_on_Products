using CRUD_YouTube.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUD_YouTube.DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {
            
        }
        public DbSet<Category> Categories { get; set; }

        // Category created statically
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Bat", DisplayOrder = 33 },
                new Category { Id = 2, Name = "Ball", DisplayOrder = 10 },
                new Category { Id = 3, Name = "Stamp", DisplayOrder = 12 }
            );
        }
    }
}
