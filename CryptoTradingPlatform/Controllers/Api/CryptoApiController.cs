using CryptoTradingPlatform.Infrastructure.Contracts;
using CryptoTradingPlatform.Models.Api;
using Microsoft.AspNetCore.Mvc;

namespace CryptoTradingPlatform.Controllers.Api
{
    [ApiController]
    public class CryptoApiController : ControllerBase
    {
        private readonly ICryptoApiService cryptoApiService;
        public CryptoApiController(ICryptoApiService _cryptoApiService)
        {
            cryptoApiService = _cryptoApiService;
        }

        [HttpGet]
        [Route("api/crypto/first")]
        public Task<CryptoResponseModel> First()
        {
            return cryptoApiService.GetFirst();
        }

        [HttpGet]
        [Route("api/crypto/top")]
        public Task<IEnumerable<CryptoResponseModel>> TopFive()
        {
            return cryptoApiService.GetTopFive();
        }

    }
}
