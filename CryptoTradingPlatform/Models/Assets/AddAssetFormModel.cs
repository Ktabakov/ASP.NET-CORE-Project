using CryptoTradingPlatform.Data;
using System.ComponentModel.DataAnnotations;

namespace CryptoTradingPlatform.Models.Assets
{
    public class AddAssetFormModel
    {
        [Required]
        public string Name { get; init; }

        [Required]
        public string Ticker { get; init; }

        [Required]
        public string ImageURL { get; init; }
    }
}
