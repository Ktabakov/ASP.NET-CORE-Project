using CryptoTradingPlatform.Core.Models.Articles;
using Microsoft.AspNetCore.Mvc;

namespace CryptoTradingPlatform.Controllers
{
    public class ArticlesController : Controller
    {
        public IActionResult Add() => View();


        [HttpPost]
        public IActionResult Add(AddArticleFormModel article)
        {
            return View();
        }

        public IActionResult All() => View();
    }
}
