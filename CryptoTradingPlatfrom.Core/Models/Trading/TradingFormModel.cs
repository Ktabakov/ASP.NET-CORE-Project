namespace CryptoTradingPlatform.Core.Models.Trading
{
    public class TradingFormModel
    {
        public string Name { get; init; }

        public decimal Quantity { get; set; }

        public decimal Price { get; set; }

        public string Type { get; set; }

        public string Ticker { get; set; }
    }
}
