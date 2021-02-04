using Currency.Domain.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Currency.Domain.IServices
{
    public interface ICurrencyService
    {
        Task<List<CurrencyRateDto>> GetRates();
        List<CurrencyRateDto> SaveEcbEuropaCurrencyRates();
    }
}
