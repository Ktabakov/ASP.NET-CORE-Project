using CryptoTradingPlatform.Core.Models.Articles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CryptoTradingPlatform.Controllers
{
    public class ArticlesController : BaseController
    {
        [Authorize(Roles = "Administrator, Manager")]
        public IActionResult Add() => View();


        [HttpPost]
        [Authorize(Roles = "Administrator, Manager")]
        public IActionResult Add(AddArticleFormModel article)
        {
            return View();
        }

        public async Task<IActionResult> All()
        {
            return View();
        }
    }
}
