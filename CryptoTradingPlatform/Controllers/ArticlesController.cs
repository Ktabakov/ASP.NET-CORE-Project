using CryptoTradingPlatform.Constants;
using CryptoTradingPlatform.Core.Models.Articles;
using CryptoTradingPlatfrom.Core.Contracts;
using CryptoTradingPlatfrom.Core.Models.Articles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CryptoTradingPlatform.Controllers
{
    public class ArticlesController : BaseController
    {
        private readonly IArticleService articleService;

        public ArticlesController(IArticleService _articleService)
        {
            articleService = _articleService;
        }

        [Authorize(Roles = "Administrator, Manager")]
        public IActionResult Add() => View();


        [HttpPost]
        [Authorize(Roles = "Administrator, Manager")]
        public async Task<IActionResult> Add(AddArticleFormModel article)
        {
            (bool success, string error) = await articleService.AddArticle(article, User.Identity.Name);
            if (!success)
            {
                ViewData[MessageConstants.UnexpectedError] = error;
                return View(article);
            }
            if (!ModelState.IsValid)
            {
                ViewData[MessageConstants.UnexpectedError] = MessageConstants.UnexpectedError;
                return View(article);
            }

            TempData[MessageConstants.Success] = "Article Added!";
            return Redirect("/Articles/All");
        }

        [HttpPost]
        public async Task<ActionResult> Like(string articleId)
        {
            bool result = await articleService.LikeArticle(articleId, User.Identity.Name);

            if (!ModelState.IsValid || result == false)
            {
                return Json(new { success = false });
            }

            return Json(new { success = true });
        }

        public async Task<IActionResult> All()
        {
            List<ArticleViewModel> model = await articleService.GetArticles();
            return View(model);
        }

        public async Task<ActionResult> GetTotalLikes(string articleId)
        {
            int totalLikes = articleService.getTotalLikes(articleId);
            return Json(totalLikes);
        }
    }
}
