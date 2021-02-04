using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Currency.DB.Configs
{
    public static class CurrencyRateUpdateDateConfig
    {
        public static void Configs(this EntityTypeBuilder<Models.CurrencyRateUpdateDate> model) {
            model.ToTable("CurrencyRateUpdateDates");
            model.HasKey(c => c.CurrencyRateUpdateDateId);
            model.Property(c => c.UpdatesFrom).HasMaxLength(50).IsRequired();
            model.Property(c => c.Updated).IsRequired().HasDefaultValue(false);
        }
    }
}
