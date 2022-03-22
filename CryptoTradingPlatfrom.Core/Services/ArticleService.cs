using CryptoTradingPlatform.Core.Models.Articles;
using CryptoTradingPlatform.Data.Models;
using CryptoTradingPlatform.Infrastructure.Data;
using CryptoTradingPlatfrom.Core.Contracts;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
     /*   public async Task<(bool success, string error)> AddArticle(AddArticleFormModel model, string? name)
        {
            bool success = false;

            var user = data.Users.FirstOrDefault(c => c.UserName == name);

            if (!await userManager.IsInRoleAsync(user, "Manager") || !await userManager.IsInRoleAsync(user, "Administrator"))
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
        }*/
    }
}
