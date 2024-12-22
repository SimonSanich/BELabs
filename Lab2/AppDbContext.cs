using Lab2.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab2
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        DbSet<User> Users { get; set; }
        DbSet<Currency> Currency { get; set; }
        DbSet<Record> Records { get; set; }
        DbSet<Category> Categories { get; set; }

    }
}
