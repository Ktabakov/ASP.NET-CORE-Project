using CryptoTradingPlatform.Models.Assets;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CryptoTradingPlatform.Controllers
{
    public class AssetsController : Controller
    {
        public IActionResult Add() => View();

        [HttpPost]
        public IActionResult Add(AddAssetFormModel asset)
        {
            return View();
        }
    }
}
