using Currency.DB.Models;
using Currency.Domain;
using Currency.Domain.Dtos;
using Currency.Domain.IDtos;
using Currency.Domain.IHttpClients;
using Currency.Domain.IServices;
using Currency.Infrastructure.IRepositories;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Currency.Infrastructure.Services
{
    public class CurrencyService : BaseService, ICurrencyService
    {
        private readonly ICurrencyRepository _repository;
        private readonly IRatesClient _httpClient;
        public CurrencyService( ICurrencyRepository repository, IOptions<AppSettingsDto> settings, IRatesClient httpClient) : base(settings: settings)
        {
            _repository = repository;
            _httpClient = httpClient;
        }

        public async Task<List<CurrencyRateDto>> GetRates()
        {

            List<CurrencyRateDto> rates = (await _repository.GetCurrencyRates().ConfigureAwait(false))
                .Select(c => new CurrencyRateDto { Code = c.Code, Rate = c.Rate }).ToList();

            if (!rates.Any())
                rates = SaveEcbEuropaCurrencyRates();

            return rates;
        }

        public virtual List<CurrencyRateDto> SaveEcbEuropaCurrencyRates()
        {
            XDocument xDoc = _httpClient.GetEcbEuropaCurrenciesXML();
            XNamespace ns = xDoc.Root.GetDefaultNamespace();

            XElement xElement = XElement.Parse(xDoc.ToString());
            List<CurrencyRate> currencyRates = xDoc.Descendants(ns + "Cube").Where(x => x.Attribute("currency") != null)
               .GroupBy(x => (string)x.Attribute("currency"), y => (decimal)y.Attribute("rate"))
               .Select(x => new CurrencyRate { Code = x.Key, Rate = x.FirstOrDefault() } ).ToList();

            _repository.ResetCurrencyRates(currencyRates, AppSettings.EcbEuropaCurrenciesXML);

            return currencyRates.Select(c=> new CurrencyRateDto { Code = c.Code, Rate = c.Rate}).ToList();
        }
    }
}
