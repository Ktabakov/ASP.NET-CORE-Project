using CryptoTradingPlatfrom.Core.CustomAttributes;
using System.ComponentModel.DataAnnotations;

namespace CryptoTradingPlatfrom.Core.Models.Assets
{
    public class BuyAssetFormModel
    {
        [Required]
        [Unlike("BuyAssetId")]
        [Display(Name = "Selling")]
        public string SellAssetId { get; set; }

        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Quantity must be more than 0")]
        public decimal SellAssetQyantity { get; set; }

        [Required]
        [Display(Name = "Buying")]
        public string BuyAssetId { get; set; }

        [Required]
        public decimal BuyAssetQuantity { get; set; }

    }
}
