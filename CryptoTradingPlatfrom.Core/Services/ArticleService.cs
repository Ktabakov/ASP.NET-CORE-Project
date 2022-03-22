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
                return (success, "You can't do that!");
            }

            Article article = new Article
            {
                ApplicationUserId = user.Id,
                Content = model.Content,
                DateAdded = DateTime.Now,
                ImageURL = model.ImageURL,
                Title = model.Title
            };

            ArticleLikes like = new ArticleLikes()
            {
                ApplicationUserId = user.Id,
                ArticleId = article.Id
            };

            try
            {
                await data.Articles.AddAsync(article);
                await data.ArticleLikes.AddAsync(like);
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
                .ArticleLikes
                .Include(c => c.Article)
                .Select(x => new ArticleViewModel
                {
                    Likes = x.Article.Likes,
                    AddedBy = x.Article.User.UserName,
                    Content = x.Article.Content,
                    DateAdded = x.Article.DateAdded,
                    ImageURL = x.Article.ImageURL,
                    Id = x.Article.Id,
                    Title = x.Article.Title,
                    IsLikedByUser = x.IsLiked
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
    }
}
