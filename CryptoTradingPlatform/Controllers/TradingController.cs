using CryptoTradingPlatform.Core.Contracts;
using CryptoTradingPlatform.Core.Models.Api;
using CryptoTradingPlatform.Core.Models.Trading;
using CryptoTradingPlatfrom.Core.Contracts;
using CryptoTradingPlatfrom.Core.Models.Assets;
using CryptoTradingPlatfrom.Core.Models.Trading;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CryptoTradingPlatform.Controllers
{
    public class TradingController : Controller
    {
        private readonly IAssetService assetService;
        private readonly ICryptoApiService cryptoService;
        private readonly ITradingService tradingService;
        public TradingController(IAssetService _assetService, ICryptoApiService _cryptoService, ITradingService _tradingService)
        {
            assetService = _assetService;
            cryptoService = _cryptoService;
            tradingService = _tradingService;
        }

        [Authorize]
        public async Task<IActionResult> Swap()
        {
            SwapAssetsListViewModel model = await assetService.GetUserAssets(User.Identity.Name);
            ViewBag.UserMoney = model.UserMoney;
            ViewBag.Assets = model.Assets.ToList();

            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Swap(BuyAssetFormModel model)
        {
            //check asset quantity to be for the user. He has to have this much to convert
            SwapAssetsListViewModel customModel = await assetService.GetUserAssets(User.Identity.Name);
            ViewBag.UserMoney = customModel.UserMoney;
            ViewBag.Assets = customModel.Assets.ToList();

            decimal buyQuantity = await assetService.CalculateTransaction(model);
            model.BuyAssetQuantity = buyQuantity;
            ViewData["BuyQuantity"] = Math.Round(buyQuantity, 8);

            if (model.Calculate == "Calculate")
            {
                return View(model);
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            //needs to be coded
            bool success = await tradingService.SaveSwap(model, User.Identity.Name);

            if (!success)
            {
                return View(model);
            }

            return Redirect("/");
        }

        [Authorize]
        public async Task<IActionResult> Trade()
        {
            List<string> tickers = await assetService.GetAllAssetTickers();
            List<CryptoResponseModel> cryptos = await cryptoService.GetCryptos(tickers);
            SwapAssetsListViewModel customModel = await assetService.GetUserAssets(User.Identity.Name);
            ViewBag.UserMoney = customModel.UserMoney;
            ViewBag.UserAssets = customModel.Assets.ToList();
            ViewBag.Assets = cryptos;

            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Trade(TradingFormModel model)
        {
            //Done! Get price again. Make sure price from form was not changed
            List<CryptoResponseModel> cryptos = await cryptoService.GetCryptos(new List<string> { model.Ticker });
            model.Price = cryptos[0].Price;

            if (!ModelState.IsValid)
            {
                return Redirect("/Trading/Trade");
            }
            bool success = tradingService.SaveTransaction(model, User.Identity.Name);

            if (!success)
            {
                return Redirect("/Trading/Trade");
            }

            return Redirect("/");
        }

        [Authorize]
        public async Task<IActionResult> History()
        {
            List<TransactionHistoryViewModel> transactions = await tradingService.GetUserTradingHistory(User.Identity.Name);
            return View(transactions);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> AddToFavorites(string ticker)
        {
            bool result = await tradingService.SaveToFavorites(ticker, User.Identity.Name);

            if (!ModelState.IsValid || result == false)
            {
                return Json(new { success = false });
            }

            return Json(new { success = true });

        }

        public async Task<IActionResult> Favorites()
        {
            List<string> tickers = await assetService.GetAllFavoritesTickers(User.Identity.Name);
            List<CryptoResponseModel> cryptos = await cryptoService.GetCryptos(tickers);

            return View(cryptos);
        }
    }
}
