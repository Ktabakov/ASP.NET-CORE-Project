using CryptoTradingPlatform.Core.Models.Articles;
using CryptoTradingPlatfrom.Core.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace CryptoTradingPlatform.Controllers
{
    public class ArticlesController : Controller
    {
        private readonly IArticlesService articlesService;
        public ArticlesController(IArticlesService _articlesService)
        {
            articlesService = _articlesService;
        }
        public IActionResult Add() => View();


        [HttpPost]
        public IActionResult Add(AddArticleFormModel article)
        {
            return View();
        }

        public async Task<IActionResult> All()
        {
            List<ArticleViewModel> model = await articlesService.GetArticles();
            ViewBag.Articles = model;
            return View();
        }
    }
}
