using CryptoTradingPlatform.Core.Models.Api;
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
    }
}
