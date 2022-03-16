using CryptoTradingPlatform.Constants;
using CryptoTradingPlatform.Core.Contracts;
using CryptoTradingPlatform.Core.Models;
using CryptoTradingPlatform.Core.Models.Api;
using CryptoTradingPlatfrom.Core.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CryptoTradingPlatform.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAssetService assetService;
        private readonly ICryptoApiService cryptoService;
        public HomeController(ILogger<HomeController> logger, IAssetService _assetService, ICryptoApiService _cryptoService)
        {
            _logger = logger;
            assetService = _assetService;
            cryptoService = _cryptoService;
        }

        //seed these 4 assets when creating DB - problem solved
        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                List<string> tickers = await assetService.GetAllAssetTickers();
                List<CryptoResponseModel> cryptos = await cryptoService.GetCryptos(tickers);
                cryptos = await assetService.CheckIfFavorites(cryptos, User.Identity.Name);

                return View(cryptos);
            }
            else
            {
                List<string> tickers = new List<string> { "BTC", "ETH", "BNB", "ADA" };
                List<CryptoResponseModel> cryptos = await cryptoService.GetCryptos(tickers);

                return View(cryptos);
            }
        }

        public IActionResult Privacy()
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