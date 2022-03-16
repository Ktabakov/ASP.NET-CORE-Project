using CryptoTradingPlatform.Core.Models.Api;
using CryptoTradingPlatfrom.Core.Models.Api;
using CryptoTradingPlatfrom.Core.Models.Assets;
using CryptoTradingPlatfrom.Core.Models.Trading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoTradingPlatfrom.Core.Contracts
{
    public interface IAssetService
    {
        (bool, string) AddAsset(CryptoResponseModel model);
        Task<List<string>> GetTickers();
        List<string> GetIds();
        AssetDetailsViewModel GetDetails(string assetName);
        Task<SwapAssetsListViewModel> GetUserAssets(string name);
        Task<decimal> CalculateTransaction(BuyAssetFormModel model);
        bool RemoveAsset(string assetName);
        Task<List<string>> GetAllAssetTickers();
        decimal GetUserMoney(string? name);
        Task<List<CryptoResponseModel>> CheckIfFavorites(List<CryptoResponseModel> cryptos, string userName);
        Task<List<string>> GetAllFavoritesTickers(string? name);
    }
}
