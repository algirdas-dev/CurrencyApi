using System.Xml.Linq;

namespace Currency.Domain.IHttpClients
{
    public interface IRatesClient
    {
        XDocument GetEcbEuropaCurrenciesXML();
    }
}
