using CryptoTradingPlatform.Core.Models.Articles;
using CryptoTradingPlatfrom.Core.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace CryptoTradingPlatform.Controllers
{
    public class NewsController : BaseController
    {
        private readonly INewsService newsService;
        public NewsController(INewsService _anewsService)
        {
            newsService = _anewsService;
        }

        public async Task<IActionResult> All()
        {
            List<NewsViewModel> model = await newsService.GetNews();
            ViewBag.News = model;
            return View();
        }
    }
}
