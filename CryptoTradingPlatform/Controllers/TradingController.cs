using CryptoTradingPlatform.Constants;
using CryptoTradingPlatform.Core.Contracts;
using CryptoTradingPlatform.Core.Models.Api;
using CryptoTradingPlatform.Core.Models.Trading;
using CryptoTradingPlatfrom.Core.Contracts;
using CryptoTradingPlatfrom.Core.Models.Assets;
using CryptoTradingPlatfrom.Core.Models.Trading;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CryptoTradingPlatform.Controllers
{
    public class TradingController : BaseController
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

        public async Task<IActionResult> Swap()
        {
            SwapAssetsListViewModel model = await assetService.GetUserAssets(User.Identity.Name);
            ViewBag.UserMoney = model.UserMoney;
            ViewBag.Assets = model.Assets.ToList();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Swap(BuyAssetFormModel model)
        {
            //check asset quantity to be for the user. He has to have this much to convert
            SwapAssetsListViewModel customModel = await assetService.GetUserAssets(User.Identity.Name);
            ViewBag.UserMoney = customModel.UserMoney;
            ViewBag.Assets = customModel.Assets.ToList();

            decimal buyQuantity = await tradingService.CalculateTransaction(model);
            model.BuyAssetQuantity = buyQuantity;
            ViewData["BuyQuantity"] = Math.Round(buyQuantity, 8);

            if (model.Calculate == "Calculate")
            {
                return View(model);
            }

            if (!ModelState.IsValid)
            {
                ViewData[MessageConstants.UnexpectedError] = MessageConstants.UnexpectedError;
                return View(model);
            }

            bool success = await tradingService.SaveSwap(model, User.Identity.Name);

            if (!success)
            {
                ViewData[MessageConstants.UnexpectedError] = MessageConstants.UnexpectedError;
                return View(model);
            }

            TempData[MessageConstants.Success] = "Swap Successful!";
            return Redirect("/");
        }

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
        public async Task<IActionResult> Trade(TradingFormModel model)
        {
            //Done! Get price again. Make sure price from form was not changed
            List<CryptoResponseModel> cryptos = await cryptoService.GetCryptos(new List<string> { model.Ticker });
            model.Price = cryptos[0].Price;

            if (!ModelState.IsValid)
            {
                ViewData[MessageConstants.UnexpectedError] = MessageConstants.UnexpectedError;
                return Redirect("/Trading/Trade");
            }
            bool success = tradingService.SaveTransaction(model, User.Identity.Name);

            if (!success)
            {
                ViewData[MessageConstants.UnexpectedError] = MessageConstants.UnexpectedError;
                return Redirect("/Trading/Trade");
            }

            TempData[MessageConstants.Success] = "Transaction Successful";
            return Redirect("/");
        }

        public async Task<IActionResult> History(string sortOrder)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["TypeSortParm"] = sortOrder == "Type" ? "type_desc" : "Type";
            ViewData["QuantitySortParm"] = sortOrder == "Quantity" ? "quantity_desc" : "Quantity";
            ViewData["PriceSortParm"] = sortOrder == "Price" ? "price_desc" : "Price";

            List<TransactionHistoryViewModel> transactions = await tradingService.GetUserTradingHistory(User.Identity.Name);

            transactions = tradingService.SortTransactions(sortOrder, transactions);
            
            return View(transactions);
        }

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
