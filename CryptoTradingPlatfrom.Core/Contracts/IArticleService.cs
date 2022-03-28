using CryptoTradingPlatform.Core.Models.Articles;
using CryptoTradingPlatfrom.Core.Models.Articles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoTradingPlatfrom.Core.Contracts
{
    public interface IArticleService
    {
        Task<(bool success, string error)> AddArticle(AddArticleFormModel article, string? name);
        Task<List<ArticleViewModel>> GetArticles();
        Task<bool> LikeArticle(string articleId, string? name);
        int getTotalLikes(string articleId);
    }
}
