using CryptoTradingPlatform.Models.Assets;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CryptoTradingPlatform.Controllers
{
    public class AssetsController : Controller
    {
        public IActionResult Add() => View();

        //when add asset maybe add only name and ticker, make the call to the api, get the rest of the infos
        //maybe get the full crypto model. When buy is clicked, get the infos again and save them in the buy model.
        //Save the price infos in Transaction table - Save in Asset table only name, ticker
        [HttpPost]
        public IActionResult Add(AddAssetFormModel asset) 
        {
            return View();
        }
    }
}
