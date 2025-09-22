using Microsoft.EntityFrameworkCore;

namespace DbOperationWithEfCoreApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CurrencyType>().HasData
                (new CurrencyType(){ Id = 1, Currency = "INR", Description = "Indian INR" },
                new CurrencyType(){ Id = 2, Currency = "Dollar", Description = "USAD" },
                new CurrencyType(){ Id = 3, Currency = "Euro", Description = "Europe" },
                new CurrencyType(){ Id = 4, Currency = "Dinar", Description = "Dinar" }
                );

            modelBuilder.Entity<Language>().HasData
                (new Language() { Id = 1, Title = "Hindi", Description = "For Indian" },
                new Language() { Id = 2, Title = "English", Description = "For USA" },
                new Language() { Id = 3, Title = "Italian", Description = "For Itely" },
                new Language() { Id = 4, Title = "German", Description = "For Germany" }
                );
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<BookPrice> BookPrices { get; set; }
        public DbSet<CurrencyType> CurrencyType { get; set; }
        public DbSet<Author> Authors { get; set; }
    }
}
