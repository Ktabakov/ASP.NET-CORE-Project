using CryptoTradingPlatform.Core.Models.Trading;
using CryptoTradingPlatfrom.Core.Models.Assets;
using CryptoTradingPlatfrom.Core.Models.Trading;

namespace CryptoTradingPlatfrom.Core.Contracts
{
    public interface ITradingService
    {
        bool SaveTransaction(TradingFormModel model, string userName);
        Task<bool> SaveSwap (BuyAssetFormModel model, string name);
        Task<bool> SaveToFavorites(string ticker, string? name);
        Task<List<TransactionHistoryViewModel>> GetUserTradingHistory(string? name);
        Task<decimal> CalculateTransaction(BuyAssetFormModel model);
        List<TransactionHistoryViewModel> SortTransactions(string sortOrder, List<TransactionHistoryViewModel> transactions);
    }
}
