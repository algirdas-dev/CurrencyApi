using Currency.Domain.Dtos;
using System.Collections.Generic;

namespace Currency.App.Models.Product
{
    public class GetRatesResponse
    {
        public List<CurrencyRateDto> CurrencyRate { get; set; }
    }
}
