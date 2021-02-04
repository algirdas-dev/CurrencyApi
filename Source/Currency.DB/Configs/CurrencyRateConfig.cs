using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Currency.DB.Models;

namespace Currency.DB.Configs
{
    public static class CurrencyRateConfig
    {
        public static void Configs(this EntityTypeBuilder<CurrencyRate> model) {
            model.ToTable("CurrencyRates");
            model.HasKey(c => c.Code);
            model.Property(c => c.Code).IsRequired().HasMaxLength(10);
            model.Property(c => c.Rate).IsRequired();
        }
    }
}
