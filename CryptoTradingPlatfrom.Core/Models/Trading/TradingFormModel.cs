using System.ComponentModel.DataAnnotations;

namespace CryptoTradingPlatform.Core.Models.Trading
{
    public class TradingFormModel
    {
        public string Name { get; init; }

        [Required]
        [Range(0.001, double.MaxValue, ErrorMessage = "Quantity must be more than 0")]
        public decimal Quantity { get; set; }

        public decimal Price { get; set; }

        public string Type { get; set; }

        public string Ticker { get; set; }
    }
}
