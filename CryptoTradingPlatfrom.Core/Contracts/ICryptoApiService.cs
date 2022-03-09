using CryptoTradingPlatform.Core.Models.Api;

namespace CryptoTradingPlatform.Core.Contracts
{
    public interface ICryptoApiService
    {
        Task<CryptoResponseModel> GetFirst();

        Task<IEnumerable<CryptoResponseModel>> GetTopFive(List<string> tickers);
    }
}
