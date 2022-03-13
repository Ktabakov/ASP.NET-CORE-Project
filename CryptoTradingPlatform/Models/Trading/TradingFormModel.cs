namespace CryptoTradingPlatform.Models.Trading
{
    public class TradingFormModel
    {
        public string Name { get; init; }

        public double Quantity { get; set; }

        public decimal Price { get; set; }

        public string Type { get; set; }
    }
}
