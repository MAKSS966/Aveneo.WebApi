using Aveneo.WebApi.Model.Dto.Response;
using Aveneo.WebApi.Services.Token.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aveneo.WebApi.Controllers.Auth
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _config;
        private readonly ILogger<AuthController> _logger;
        public AuthController(IConfiguration config, ITokenService tokenService, ILogger<AuthController> logger)
        {
            this._config = config;
            this._tokenService = tokenService;
            this._logger = logger;
            this._logger.LogDebug("Nlog injected into AuthController");
        }

        [HttpGet]
        [Route("GenerateToken")]
        public async Task<IActionResult> GetToken()
        {
            _logger.LogInformation("Hello, this is the GetToken!");
            var token = this._tokenService.GetToken(this._config["JwtConfig:Secret"], this._config["JwtConfig:Issuer"]);
            return Ok(new AuthResultResponse()
            {
                Success = true,
                Token = token
            });
        }
       
    }
}
