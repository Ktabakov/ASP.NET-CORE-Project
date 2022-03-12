using CryptoTradingPlatform.Core.Contracts;
using CryptoTradingPlatform.Core.Models.Api;
using CryptoTradingPlatform.Models;
using CryptoTradingPlatform.Models.Assets;
using CryptoTradingPlatfrom.Core.Contracts;
using CryptoTradingPlatfrom.Core.Models.Assets;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CryptoTradingPlatform.Controllers
{
    public class AssetsController : Controller
    {
        private readonly ICryptoApiService cryptoApiService;
        private readonly IAssetService assetService;

        public AssetsController(ICryptoApiService _cryptoApiService, IAssetService _assetService)
        {
            cryptoApiService = _cryptoApiService;
            assetService = _assetService;
        }
        public IActionResult Add() => View();

        //when add asset maybe add only name and ticker, make the call to the api, get the rest of the infos
        //maybe get the full crypto model. When buy is clicked, get the infos again and save them in the buy model.
        //Save the price infos in Transaction table - Save in Asset table only name, ticker
        [HttpPost]
        public IActionResult Add(AddAssetFormModel asset) 
        {
            var isNumeric = int.TryParse(asset.Ticker, out int value);

            //Maybe toaster cant be number - and refresh page
            if (isNumeric)
            {
                return View("Error", new ErrorViewModel { Message = "Ticker cannot but a number" });
            }
            //Maybe toaster random mistake - and refresh page
            if (!ModelState.IsValid)
            {
                return View("Error", new ErrorViewModel { Message = "An unexpected error has occurred" });
            }

            List<string> assets = new List<string>();
            assets.Add(asset.Ticker);

            Task<List<CryptoResponseModel>> models = cryptoApiService.GetCryptos(assets);

            //Maybe toaster invalid asset - and refresh page
            if (models.Result == null)
            {
                return View("Error", new ErrorViewModel { Message = "Ticker is Invalid"});
            }

            //Call assetservice and save model into DB
            //Aater that redirect to home and list all assets
            CryptoResponseModel model = models.Result.FirstOrDefault();
            (bool success, string error) = assetService.AddAsset(model);

            if (!success)
            {
                return View("Error", new ErrorViewModel { Message = "An unexpected error has occurred" });
            }

            return Redirect("/");
        }

        public IActionResult Trade() => View();


        [Authorize]
        public IActionResult Swap()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Swap(SwapAssetViewModel)
        {
            return View();
        }

        public IActionResult Details(string assetName)
        {
            AssetDetailsViewModel model = assetService.GetDetails(assetName);
            if (model == null)
            {
                return View("Error", new ErrorViewModel { Message = "Asset doesn't exist on the platform" });
            }
            ViewBag.Model = model;
            return View(model);
        }
    }
}
