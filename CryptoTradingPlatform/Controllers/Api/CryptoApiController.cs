using CryptoTradingPlatform.Infrastructure.Contracts;
using CryptoTradingPlatform.Models.Api;
using Microsoft.AspNetCore.Mvc;

namespace CryptoTradingPlatform.Controllers.Api
{
    [ApiController]
    [Route("api/crypto")]
    public class CryptoApiController : ControllerBase
    {
        private readonly ICryptoApiService cryptoApiService;
        public CryptoApiController(ICryptoApiService _cryptoApiService)
        {
            cryptoApiService = _cryptoApiService;
        }
        
        [HttpGet]
        public Task<CryptoResponseModel> First()
        {
            var result = cryptoApiService.GetFirst();
            return result;
        }

    }
}
