using CryptoTradingPlatform.Core.Models.Api;
using CryptoTradingPlatfrom.Core.Models.Api;

namespace CryptoTradingPlatform.Core.Contracts
{
    public interface ICryptoApiService
    {
        Task<List<CryptoResponseModel>> GetCryptos(List<string> tickers);

        Task<List<ImageDescriptionResponseModel>> GetImgUrls(List<string> tickers);
    }
}
