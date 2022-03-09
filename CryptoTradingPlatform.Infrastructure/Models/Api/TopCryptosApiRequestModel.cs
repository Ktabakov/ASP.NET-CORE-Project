namespace CryptoTradingPlatform.Models.Api
{
    using System.Collections.Generic;
    public class TopCryptosApiRequestModel
    {
        public IEnumerable<CryptoResponseModel> Cryptos { get; init; }
    }
}
