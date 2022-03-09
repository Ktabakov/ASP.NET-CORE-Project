namespace CryptoTradingPlatform.Models.Api
{
    public class CryptoResponseModel
    {
        public string Ticker { get; init; }

        public string Name { get; init; }

        public decimal Price { get; init; }

        public decimal MarketCap { get; init; }

        public long CirculatingSupply { get; init; }

        public decimal PercentChange { get; init; }

        public string Logo { get; set; }
    }
}
