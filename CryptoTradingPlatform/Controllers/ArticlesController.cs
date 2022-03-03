using CryptoTradingPlatform.Models.Articles;
using Microsoft.AspNetCore.Authorization;
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
    }
}
