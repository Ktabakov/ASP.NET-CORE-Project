using CryptoTradingPlatform.Constants;
using CryptoTradingPlatform.Core.Contracts;
using CryptoTradingPlatform.Core.Models;
using CryptoTradingPlatform.Core.Models.Api;
using CryptoTradingPlatform.Core.Models.Assets;
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
        [Authorize]
        public IActionResult Add(AddAssetFormModel asset) 
        {
            var isNumeric = int.TryParse(asset.Ticker, out int value);

            //Maybe toaster cant be number - and refresh page
            if (isNumeric)
            {
                ViewData[MessageConstants.UnexpectedError] = "Ticker cannot but a number";
                return View();
            }
            //Maybe toaster random mistake - and refresh page
            if (!ModelState.IsValid)
            {
                ViewData[MessageConstants.UnexpectedError] = MessageConstants.UnexpectedError;
                return View();
            }

            List<string> assets = new List<string>();
            assets.Add(asset.Ticker);

            Task<List<CryptoResponseModel>> models = cryptoApiService.GetCryptos(assets);

            if (models.Result == null)
            {
                ViewData[MessageConstants.UnexpectedError] = "Ticker is Invalid";
                return View();
            }

            //Call assetservice and save model into DB
            //Aater that redirect to home and list all assets
            CryptoResponseModel model = models.Result.FirstOrDefault();
            (bool success, string error) = assetService.AddAsset(model);

            if (!success)
            {
                ViewData[MessageConstants.UnexpectedError] = MessageConstants.UnexpectedError;
                return View();
            }

            TempData[MessageConstants.Success] = "Crypto Added!";
            return Redirect("/");
        }


        public IActionResult Details(string assetName)
        {
            AssetDetailsViewModel model = assetService.GetDetails(assetName);
            if (model == null)
            {
                ViewData[MessageConstants.UnexpectedError] = "Asset doesn't exist on the platform";
                return View();
            }
            return View(model);
        }

        public IActionResult Remove(string assetName)
        {
            if (string.IsNullOrEmpty(assetName))
            {
                return View();
            }

            bool success = assetService.RemoveAsset(assetName);

            if (!success)
            {
                ViewData[MessageConstants.UnexpectedError] = MessageConstants.UnexpectedError;
                return View();
            }

            TempData[MessageConstants.Success] = "Assst Removed!";
            return Redirect("/");

        }
    }
}
