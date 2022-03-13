using CryptoTradingPlatform.Core.Models.Api;
using CryptoTradingPlatfrom.Core.Models.Api;
using CryptoTradingPlatfrom.Core.Models.Assets;
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
        public SwapAssetsListViewModel ListForSwap(string name);
        Task<decimal> CalculateTransaction(BuyAssetFormModel model);
        bool SaveSwap(BuyAssetFormModel model);
        bool RemoveAsset(string assetName);
    }
}
