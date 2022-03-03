using CryptoTradingPlatform.Data;
using System.ComponentModel.DataAnnotations;

namespace CryptoTradingPlatform.Models.Assets
{
    public class AddAssetFormModel
    {
        public string Name { get; init; }

        public string Ticker { get; init; }

        public string ImageURL { get; init; }
    }
}
