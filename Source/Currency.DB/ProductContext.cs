using Microsoft.EntityFrameworkCore;
using Currency.DB.Models;
using Currency.DB.Configs;

namespace Currency.DB
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions options)
        : base(options)
        { }

        //protected override void OnConfiguring(DbContextOptionsBuilder options)
        //    => options.UseSqlServer("Data Source=.;Initial Catalog=Products;Integrated Security=True;");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.CurrencyRateUpdateDate>().Configs();
            modelBuilder.Entity<CurrencyRate>().Configs();
        }

        public DbSet<Models.CurrencyRateUpdateDate> CurrencyRateUpdateDates { get; set; }
        public DbSet<CurrencyRate> CurrencyRates { get; set; }
    }
}
