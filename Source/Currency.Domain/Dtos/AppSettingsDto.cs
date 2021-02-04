using Currency.Domain.IDtos;

namespace Currency.Domain.Dtos
{
    public class AppSettingsDto : IAppSettingsDto
    {
        public string EcbEuropaCurrenciesXML { get; set; }
    }
}
