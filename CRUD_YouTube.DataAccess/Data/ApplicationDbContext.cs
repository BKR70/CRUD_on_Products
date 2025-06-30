using CRUD.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CRUD.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {
            
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        // Category created statically
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Bat", DisplayOrder = 33 },
                new Category { Id = 2, Name = "Ball", DisplayOrder = 10 },
                new Category { Id = 3, Name = "Stamp", DisplayOrder = 12 }
            );
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Title = "Ansi C", Author = "Bilas",
                              Description="Great book by my side", ISBN = "19323", 
                              ListPrice = 100, Price = 18, Price100 = 199, Price50 = 50, CategoryId = 12,
                              ImageUrl = ""
                }
            );
        }
    }
}
