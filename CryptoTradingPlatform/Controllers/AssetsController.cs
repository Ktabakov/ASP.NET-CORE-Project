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
    public class AssetsController : BaseController
    {
        private readonly ICryptoApiService cryptoApiService;
        private readonly IAssetService assetService;

        public AssetsController(ICryptoApiService _cryptoApiService, IAssetService _assetService)
        {
            cryptoApiService = _cryptoApiService;
            assetService = _assetService;
        }

        public IActionResult Add() => View();

        [HttpPost]
        public IActionResult Add(AddAssetFormModel asset) 
        {
            var isNumeric = int.TryParse(asset.Ticker, out int value);

            if (isNumeric)
            {
                ViewData[MessageConstants.UnexpectedError] = "Ticker cannot but a number";
                return View();
            }
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

            CryptoResponseModel model = models.Result.FirstOrDefault();
            (bool success, string error) = assetService.AddAsset(model);

            if (!success)
            {
                ViewData[MessageConstants.UnexpectedError] = error;
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
