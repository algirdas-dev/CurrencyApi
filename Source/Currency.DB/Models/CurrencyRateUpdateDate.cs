using System;

namespace Currency.DB.Models
{
    public class CurrencyRateUpdateDate
    {
        public int CurrencyRateUpdateDateId { get; set; }
        public string UpdatesFrom { get; set; }
        public DateTimeOffset UpdateDate { get; set; }
        public bool Updated { get; set; }
    }
}
