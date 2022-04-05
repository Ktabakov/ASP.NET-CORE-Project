using CryptoTradingPlatform.Constants;
using CryptoTradingPlatform.Core.Contracts;
using CryptoTradingPlatform.Core.Models;
using CryptoTradingPlatform.Core.Models.Api;
using CryptoTradingPlatform.Core.Models.Articles;
using CryptoTradingPlatfrom.Core.Cache;
using CryptoTradingPlatfrom.Core.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.Diagnostics;

namespace CryptoTradingPlatform.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> logger;
        private readonly IAssetService assetService;
        private readonly ICryptoApiService cryptoService;
        private readonly INewsService newsService;
        private readonly IDistributedCache cache;

        public HomeController(ILogger<HomeController> _logger,
            IAssetService _assetService,
            ICryptoApiService _cryptoService,
            INewsService _newsService,
            IDistributedCache _cache)
        {
            logger = _logger;
            assetService = _assetService;
            cryptoService = _cryptoService;
            newsService = _newsService;
            cache = _cache;
        }

        //seed these 4 assets when creating DB - problem solved
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            List<NewsViewModel> news = await newsService.GetNews();
            ViewBag.News = news;

            if (User.Identity.IsAuthenticated)
            {

                List<string> tickers = await assetService.GetAllAssetTickers();
                List<CryptoResponseModel> cryptos = await cryptoService.GetCryptos(tickers);
                return View(cryptos);

            }
            else
            {
                //use the seed
                List<string> tickers = new List<string> { "BTC", "ETH", "BNB", "ADA" };
                var result = CacheModel.Get("cryptos");
                if (result == null)
                {
                    List<CryptoResponseModel> cryptos = await cryptoService.GetCryptos(tickers);
                    CacheModel.Add("cryptos", cryptos);
                    return View(cryptos);
                }
                else
                {
                    return View(result);
                }
            }
        }

        [AllowAnonymous]
        public async Task<IActionResult> Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}