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

        /*[HttpGet]
        [Route("api/crypto/first")]
        public Task<CryptoResponseModel> First()
        {
            return cryptoApiService.GetFirst();
        }*/

        [HttpGet]
        public Task<List<CryptoResponseModel>> Top()
        {
            /* List<string> tickers = new List<string>{ "btc", "eth", "bnb", "ada", "dot","usdt","usdc","luna","trx","xrp"};*/

            List<string> tickers = assetService.GetTickers();
            //get cryptos from db and make api call
            //test with list<string>
            return cryptoApiService.GetCryptos(tickers);
        }

    }
}
