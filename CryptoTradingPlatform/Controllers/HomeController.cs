using CryptoTradingPlatform.Core.Contracts;
using CryptoTradingPlatform.Core.Models;
using CryptoTradingPlatform.Core.Models.Api;
using CryptoTradingPlatfrom.Core.Contracts;
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

        public async Task<IActionResult> Index()
        {
            List<string> tickers = assetService.GetAllAssetTickers();
            List<CryptoResponseModel> cryptos = await cryptoService.GetCryptos(tickers);

            ViewBag.Assets = cryptos;
            return View();
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