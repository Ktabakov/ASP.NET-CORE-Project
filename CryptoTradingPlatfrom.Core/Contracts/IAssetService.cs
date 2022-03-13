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
        List<string> GetTickers();
        List<string> GetIds();
        AssetDetailsViewModel GetDetails(string assetName);
        public SwapAssetsListViewModel GetUserAssets(string name);
        Task<decimal> CalculateTransaction(BuyAssetFormModel model);
        bool RemoveAsset(string assetName);
        List<string> GetAllAssetTickers();
        decimal GetUserMoney(string? name);
    }
}
