using CryptoTradingPlatform.Core.Models.Articles;
using CryptoTradingPlatform.Data.Models;
using CryptoTradingPlatform.Infrastructure.Data;
using CryptoTradingPlatform.Infrastructure.Data.Models;
using CryptoTradingPlatfrom.Core.Contracts;
using CryptoTradingPlatfrom.Core.Models.Articles;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CryptoTradingPlatfrom.Core.Services
{
    public class ArticleService : IArticleService
    {
        private readonly ApplicationDbContext data;
        private readonly UserManager<ApplicationUser> userManager;

        public ArticleService(ApplicationDbContext _data, UserManager<ApplicationUser> _userManager)
        {
            data = _data;
            userManager = _userManager;
        }
        public async Task<(bool success, string error)> AddArticle(AddArticleFormModel model, string? name)
        {
            bool success = false;

            var user = data.Users.FirstOrDefault(c => c.UserName == name);

            if (!await userManager.IsInRoleAsync(user, "Manager") && !await userManager.IsInRoleAsync(user, "Administrator"))
            {
                return (success, "We don't do that here!");
            }

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
                await data.Articles.AddAsync(article);
                await data.SaveChangesAsync();  
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

            return await data
                .Articles
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
            return data
                .Articles
                .FirstOrDefault(c => c.Id == articleId)
                .Likes;
        }

        public async Task<bool> LikeArticle(string articleId, string? userName)
        {

            var userId = data.Users.FirstOrDefault(c => c.UserName == userName).Id;
            bool success = false;

            if (String.IsNullOrEmpty(userId) || String.IsNullOrEmpty(articleId))
            {
                return success;
            }
            var articleLikeEntry = data.ArticleLikes.FirstOrDefault(c => c.ApplicationUserId == userId && c.ArticleId == articleId);

            try
            {
                if (articleLikeEntry == null)
                {
                    data.ArticleLikes.Add(new ArticleLikes { ApplicationUserId = userId, ArticleId = articleId });
                    data.Articles.Where(c => c.Id == articleId).FirstOrDefault().Likes++;
                }
                else
                {
                    data.ArticleLikes.Remove(articleLikeEntry);
                    data.Articles.Where(c => c.Id == articleId).FirstOrDefault().Likes--;
                }
                data.SaveChanges();
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
            var article = await data.Articles.FirstOrDefaultAsync(c => c.Id == articleId);

            if(article == null)
            {
                return (success);
            }
            try
            {
                data
               .Articles.Remove(article);
                data.SaveChanges();
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
