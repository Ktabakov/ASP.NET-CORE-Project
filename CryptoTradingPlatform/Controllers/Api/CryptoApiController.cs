using CryptoTradingPlatform.Core.Contracts;
using CryptoTradingPlatform.Core.Models.Api;
using CryptoTradingPlatfrom.Core.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace CryptoTradingPlatform.Controllers.Api
{
    [ApiController]
    [Route("api/crypto")]
    public class CryptoApiController : ControllerBase
    {
        private readonly ICryptoApiService cryptoApiService;
        private readonly IAssetService assetService;
        public CryptoApiController(ICryptoApiService _cryptoApiService, IAssetService _assetService)
        {
            cryptoApiService = _cryptoApiService;
            assetService = _assetService;
        }


        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<List<CryptoResponseModel>>> Top()
        {
            List<string> tickers = await assetService.GetAllAssetTickers();
            return await cryptoApiService.GetCryptos(tickers);
        }

    }
}
