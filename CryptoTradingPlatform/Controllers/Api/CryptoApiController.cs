using CryptoTradingPlatform.Core.Contracts;
using CryptoTradingPlatform.Core.Models.Api;
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

        /*[HttpGet]
        [Route("api/crypto/first")]
        public Task<CryptoResponseModel> First()
        {
            return cryptoApiService.GetFirst();
        }*/

        [HttpGet]
        public Task<IEnumerable<CryptoResponseModel>> TopFive()
        {
            List<string> tickers = new List<string>{ "btc", "eth", "bnb", "ada", "dot","usdt","usdc","luna","trx","xrp"};
            //get cryptos from db and make api call
            //test with list<string>
            return cryptoApiService.GetTopFive(tickers);
        }

    }
}
