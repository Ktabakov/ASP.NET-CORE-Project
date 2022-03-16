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
        public DateTime Date { get; set; }

        [Required]
        [MaxLength(DataConstants.TransactionType)]
        public string Type { get; set; }

        public string AssetName { get; set; }

    }
}
