using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Currency.App.Models.Product;
using Currency.Domain.Dtos;
using Currency.Domain.IServices;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Swashbuckle.Swagger.Annotations;

namespace Currency.App.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CurrencyController : BaseController<ICurrencyService>
    {
        public CurrencyController(ILogger<CurrencyController> logger, ICurrencyService service) :base(logger, service)
        {
        }


        [HttpGet("GetRates")]
        [SwaggerOperation("GetRates")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]

        public async Task<IActionResult> GetRates()
        {
            Logger.LogInformation($"Get rates action");
            var result = new GetRatesResponse();
            result.CurrencyRate = await Service.GetRates().ConfigureAwait(false);

            return Ok(result);
        }

        
    }
}
