using CryptoTradingPlatfrom.Core.Contracts;
using CryptoTradingPlatfrom.Core.Models.Assets;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CryptoTradingPlatform.Controllers
{
    public class TradingController : Controller
    {
        private readonly IAssetService assetService;
        public TradingController(IAssetService _assetService)
        {
            assetService = _assetService;
        }

        [Authorize]
        public IActionResult Swap()
        {
            SwapAssetsListViewModel model = assetService.ListForSwap(User.Identity.Name);
            ViewBag.UserMoney = model.UserMoney;
            ViewBag.Assets = model.Assets.ToList();

            return View();
        }

        [Authorize]
        [HttpPost]
        //Swap only user assets
        //Add another page for buying and selling all
        public async Task<IActionResult> Swap(BuyAssetFormModel model)
        {
            //check asset quantity to be for the user. He has to have this much to convert
            //rewrite to swap only user crypros
            // add a new page to buy any crypto with usd
            SwapAssetsListViewModel customModel = assetService.ListForSwap(User.Identity.Name);
            ViewBag.UserMoney = customModel.UserMoney;
            ViewBag.Assets = customModel.Assets.ToList();

            decimal buyQuantity = Math.Round(await assetService.CalculateTransaction(model), 2);
            model.BuyAssetQuantity = buyQuantity;
            ViewData["BuyQuantity"] = buyQuantity;

            if (model.Calculate == "Calculate")
            {
                return View(model);
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            //needs to be coded
            bool success = assetService.SaveSwap(model);

            return Redirect("/");
        }
        public IActionResult Trade() => View();
    }
}
