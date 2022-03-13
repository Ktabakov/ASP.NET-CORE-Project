using System.ComponentModel.DataAnnotations;

namespace CryptoTradingPlatfrom.Core.Models.Assets
{
    public class SwapAssetViewModel
    {
        [Required]
        public string AssetId { get; set; }

        [Required]
        public string AssetName { get; set; }

        [Required]
        public decimal AssetQuantity { get; set; }

        public string ImageUrl { get; set; }

    }
}
