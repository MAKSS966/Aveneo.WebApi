using Aveneo.WebApi.Data;
using Aveneo.WebApi.Services.ExchangeRate.ECB;
using Aveneo.WebApi.Services.ExchangeRate.Interfaces;
using Aveneo.WebApi.Services.ExchangeRate.Structs;
using Aveneo.WebApi.Services.Token.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aveneo.WebApi.Controllers.Currency
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly ILogger<CurrencyController> _logger;
        private readonly IExchangeRateService _exchangeRate;
        private readonly ITokenService _tokenService;
        public CurrencyController(IConfiguration config, ILogger<CurrencyController> logger, IExchangeRateService exchangeRate, AveneoContext context, ITokenService tokenService)
        {
            this._config = config;
            this._logger = logger;
            this._exchangeRate = exchangeRate;
            this._tokenService = tokenService;
            this._exchangeRate.SetProvider(ProviderType.EuropeanCentralBank);

        }
        [HttpPost]
        [Route("ExchangeRate/{startDate:datetime}/{endDate:datetime}")]
        public async Task<IActionResult> ExchangeRate([FromBody] Dictionary<String, String> currencyCodes, DateTime startDate, DateTime endDate)
        {
            StringBuilder sb = new StringBuilder();
            this._logger.LogInformation("Request new ExchangeRate, startDate={0}, endDate={1}, currencyCodes={2}", startDate, endDate, String.Join(",", currencyCodes));

           

            DateTime currentDate = DateTime.Now;

            if (startDate.CompareTo(currentDate) > 0)
            {
                sb = new StringBuilder();
                sb.Append("Parameter startDate is from the future.");
                sb.Append("startDate = ");
                sb.Append(startDate.ToString());
                this._logger.LogInformation(sb.ToString());
                return StatusCode(StatusCodes.Status404NotFound, sb.ToString());
            }

            if (startDate.CompareTo(endDate) > 0)
            {
                sb = new StringBuilder();
                sb.Append("Parameter startDate is grater than endDate. ");
                sb.Append("startDate = ");
                sb.Append(startDate.ToString());
                sb.Append("endDate = ");
                sb.Append(endDate.ToString());
                this._logger.LogInformation(sb.ToString());
                return StatusCode(StatusCodes.Status400BadRequest, sb.ToString());
            }



            return Ok(await this._exchangeRate.Get(currencyCodes, startDate, endDate));
        }
    }
}
