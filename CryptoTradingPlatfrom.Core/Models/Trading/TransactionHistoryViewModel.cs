using CryptoTradingPlatform.Data;
using System.ComponentModel.DataAnnotations;

namespace CryptoTradingPlatfrom.Core.Models.Trading
{
    public class TransactionHistoryViewModel
    {

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public double Quantity { get; set; }

        [Required]
        [Display(Name = "Trade Date")]
        public DateTime Date { get; set; }

        [Required]
        [MaxLength(DataConstants.TransactionType)]
        [Display(Name = "Transaction Type")]
        public string Type { get; set; }

        [Display(Name = "Asset Name")]
        public string AssetName { get; set; }

        [Display(Name = "Transaction Fee")]
        public decimal Fee { get; set; }

    }
}
