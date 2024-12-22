using Lab2.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab2
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Currency> Currency { get; set; }
        public DbSet<Record> Records { get; set; }
        public DbSet<Category> Categories { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Currency>()
                .HasData(new Currency
                {
                    Id = 1,
                    Name = "USD"
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}
