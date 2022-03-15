using CryptoTradingPlatform.Core.Models.Trading;
using CryptoTradingPlatfrom.Core.Models.Assets;

namespace CryptoTradingPlatfrom.Core.Contracts
{
    public interface ITradingService
    {
        bool SaveTransaction(TradingFormModel model, string userName);
        Task<bool> SaveSwap (BuyAssetFormModel model, string name);
        Task<bool> SaveToFavorites(string ticker, string? name);
    }
}
