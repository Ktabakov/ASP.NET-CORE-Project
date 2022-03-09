using CryptoTradingPlatform.Infrastructure.Contracts;
using CryptoTradingPlatform.Models.Api;
using CryptoTradingPlatform.Infrastructure.Constants;
using System.Net;
using System.Web;
using Newtonsoft.Json.Linq;

namespace CryptoTradingPlatform.Infrastructure.Services
{
    public class CryptoApiService : ICryptoApiService
    {
        static HttpClient client = new HttpClient();

        public async Task<CryptoResponseModel> GetFirst()
        {

            client.DefaultRequestHeaders.Add("Accepts", "application/json");
            client.DefaultRequestHeaders.Add("X-CMC_PRO_API_KEY", ApiConstants.ApiKey);

            HttpResponseMessage response = await client.GetAsync(ApiConstants.BasePath + "?symbol=btc");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            } 

            var result = await response.Content.ReadAsStringAsync();

            JObject json = JObject.Parse(result);
            CryptoResponseModel model = new CryptoResponseModel()
            {
                CirculatingSupply = json["data"]["BTC"][0]["circulating_supply"].ToString(),
                Name = json["data"]["BTC"][0]["name"].ToString(),
                Price = json["data"]["BTC"][0]["quote"]["USD"]["price"].ToString(),
                MarketCap = json["data"]["BTC"][0]["quote"]["USD"]["market_cap"].ToString(),
                Ticker = json["data"]["BTC"][0]["symbol"].ToString(),
                Volume = json["data"]["BTC"][0]["quote"]["USD"]["volume_24h"].ToString()
            };
            return model;

        }

        public TopCryptosApiRequestModel GetTopFive()
        {
            throw new NotImplementedException();
        }
      
    }
}

