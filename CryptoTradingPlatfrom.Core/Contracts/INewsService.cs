using CryptoTradingPlatform.Core.Models.Articles;

namespace CryptoTradingPlatfrom.Core.Contracts
{
    public interface INewsService
    {
        Task<List<NewsViewModel>> GetNews();
    }
}
