using CryptoTradingPlatform.Models;
using CryptoTradingPlatfrom.Core.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CryptoTradingPlatform.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAssetService assetService;
        public HomeController(ILogger<HomeController> logger, IAssetService _assetService)
        {
            _logger = logger;
            assetService = _assetService;
        }

        public IActionResult Index()
        {
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