using CryptoTradingPlatform.Core.Models.Articles;

namespace CryptoTradingPlatfrom.Core.Contracts
{
    public interface IArticlesService
    {
        Task<List<ArticleViewModel>> GetArticles();
    }
}
