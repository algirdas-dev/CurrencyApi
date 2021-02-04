
using Currency.Domain.Dtos;
using Currency.Domain.IDtos;
using Microsoft.Extensions.Options;

namespace Currency.Infrastructure.Services
{
    public abstract class BaseService
    {
        protected readonly AppSettingsDto AppSettings;
        protected BaseService(IOptions<AppSettingsDto> settings = null) {
            AppSettings = settings.Value;
        }
    }
}
