namespace CryptoTradingPlatform.Models.Api
{
    public class CryptoResponseModel
    {
        public string Ticker { get; init; }

        public string Name { get; init; }

        public string Price { get; init; }

        public string MarketCap { get; init; }

        public string CirculatingSupply { get; init; }

        public string Volume { get; init; }
    }
}
