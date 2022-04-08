using CryptoTradingPlatform.Core.Models.Articles;
using CryptoTradingPlatform.Data.Models;
using CryptoTradingPlatform.Infrastructure.Data;
using CryptoTradingPlatform.Infrastructure.Data.Models;
using CryptoTradingPlatform.Infrastructure.Data.Repositories;
using CryptoTradingPlatfrom.Core.Contracts;
using CryptoTradingPlatfrom.Core.Models.Articles;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CryptoTradingPlatfrom.Core.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IApplicatioDbRepository repo;
        //private readonly UserManager<ApplicationUser> userManager;

        public ArticleService(IApplicatioDbRepository _repo)
        {
            repo = _repo;
            //userManager = _userManager;
        }
        public async Task<(bool success, string error)> AddArticle(AddArticleFormModel model, string? name)
        {
            bool success = false;

            var user = await repo.All<ApplicationUser>().FirstOrDefaultAsync(c => c.UserName == name);

            //if (!await userManager.IsInRoleAsync(user, "Manager") && !await userManager.IsInRoleAsync(user, "Administrator"))
            //{
            //    return (success, "We don't do that here!");
            //}

            Article article = new Article
            {
                ApplicationUserId = user.Id,
                Content = model.Content,
                DateAdded = DateTime.Now,
                ImageURL = model.ImageURL,
                Title = model.Title
            };


            try
            {
                await repo.AddAsync<Article>(article);
                await repo.SaveChangesAsync();  
                success = true;
            }
            catch (Exception)
            {
                success = false;
            }

            return (success, null);
        }

        public async Task<List<ArticleViewModel>> GetArticles()
        {

            return await repo.All<Article>()
                .Select(c => new ArticleViewModel
                {
                    Likes = c.Likes,
                    AddedBy = c.User.UserName,
                    Content = c.Content,
                    DateAdded = c.DateAdded,
                    ImageURL = c.ImageURL,
                    Id = c.Id,
                    Title = c.Title,
                })
                .ToListAsync();


            /*   return await data
                   .Articles
                   .Select(c => new ArticleViewModel
                   {
                       AddedBy = c.ApplicationUserId,
                       Content = c.Content,
                       DateAdded = c.DateAdded,
                       Id = c.Id,
                       ImageURL = c.ImageURL,
                       Title = c.Title,
                       Likes = c.Likes,                    
                   })
                   .ToListAsync();*/


        }

        public int getTotalLikes(string articleId)
        {
            return repo
                .All<Article>()
                .FirstOrDefault(c => c.Id == articleId)
                .Likes;
        }

        public async Task<bool> LikeArticle(string articleId, string? userName)
        {

            var userId = repo.All<ApplicationUser>().FirstOrDefault(c => c.UserName == userName).Id;
            bool success = false;
            var article = await repo.GetByIdAsync<Article>(articleId);

            if (String.IsNullOrEmpty(userId) || String.IsNullOrEmpty(articleId))
            {
                return success;
            }
            var articleLikeEntry = await repo.All<ArticleLikes>().FirstOrDefaultAsync(c => c.ApplicationUserId == userId && c.ArticleId == articleId);

            try
            {
                if (articleLikeEntry == null)
                {
                    await repo.AddAsync<ArticleLikes>(new ArticleLikes { ApplicationUserId = userId, ArticleId = articleId });
                    article.Likes++;
                }
                else
                {
                    repo.Delete<ArticleLikes>(articleLikeEntry);
                    article.Likes--;
                }
                await repo.SaveChangesAsync();
                success = true;
            }
            catch (Exception)
            {
                return success;
            }

            return success;
        }

        public async Task<bool> RemoveArticle(string articleId)
        {
            bool success = false;
            var article = await repo.GetByIdAsync<Article>(articleId);

            if(article == null)
            {
                return (success);
            }
            try
            {
                repo.Delete<Article>(article);
                await repo.SaveChangesAsync();
                success = true;
            }
            catch (Exception)
            {
                return success;
            }

            return success;
        }
    }
}
