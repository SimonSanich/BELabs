using Lab2.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab2
{
    public interface IDbInitializer
    {
        void Initialize();
    }

    public class DbInitialize : IDbInitializer
    {
        private readonly AppDbContext _db;

        public DbInitialize(AppDbContext db)
        {
            _db = db;
        }

        public void Initialize()
        {
            try
            {
                if (_db.Database.GetPendingMigrations().Any())
                {
                    _db.Database.Migrate();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error during migration: " + ex.Message);
            }

            if (_db.Currency.Any())
            {
                return;
            }

            Currency currency = new Currency()
            {
                Id = 1,
                Name = "USD"
            };
            _db.Currency.Add(currency);
            _db.SaveChanges();
        }
    }
}
