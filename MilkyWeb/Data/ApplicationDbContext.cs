using Microsoft.EntityFrameworkCore;
using MilkyWeb.Models;

namespace MilkyWeb.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category{Name = "Action", DisplayOrder = 1, Id = 1},
                new Category{Name = "SciFi", DisplayOrder = 2, Id = 2},
                new Category{Name = "History", DisplayOrder = 3, Id = 3 }
                );
        }
    }
}
