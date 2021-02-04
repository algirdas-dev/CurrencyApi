using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Currency.App.Controllers
{
    
    public abstract class BaseController<T> : ControllerBase
    {
        protected readonly ILogger<CurrencyController> Logger;
        protected readonly T Service;
        public BaseController(ILogger<CurrencyController> logger, T service) {
            Logger = logger;
            Service = service;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public NotFoundResult NotFound(string logMessage)
        {
            Logger.LogWarning($"Not Found: {logMessage}");
            return this.NotFound();
        }
    }
}
