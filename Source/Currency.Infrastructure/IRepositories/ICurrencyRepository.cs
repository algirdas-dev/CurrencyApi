using Currency.DB.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Currency.Infrastructure.IRepositories
{
    public interface ICurrencyRepository
    {
        void ResetCurrencyRates(List<CurrencyRate> rate, string updatedFrom);
        Task<List<CurrencyRate>> GetCurrencyRates();
    }
}
