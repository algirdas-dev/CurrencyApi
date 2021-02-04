using Currency.Domain.Dtos;
using Currency.Domain.IDtos;
using Currency.Domain.IHttpClients;
using Microsoft.Extensions.Options;
using System.Xml.Linq;

namespace Currency.Infrastructure.HttpClients
{
    public class RatesClient:IRatesClient
    {
        private readonly AppSettingsDto _appSettings;
        public RatesClient(IOptions<AppSettingsDto> settings) {
            _appSettings = settings.Value;
        }
        public XDocument GetEcbEuropaCurrenciesXML() {
            return XDocument.Load(_appSettings.EcbEuropaCurrenciesXML);
        }
    }
}
