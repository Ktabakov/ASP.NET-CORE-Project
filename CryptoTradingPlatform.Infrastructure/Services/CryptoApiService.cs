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
            InitRequest();
            HttpResponseMessage response = await client.GetAsync(ApiConstants.LatestPath + "?symbol=btc");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var result = await response.Content.ReadAsStringAsync();

            JObject json = JObject.Parse(result);
            CryptoResponseModel model = new CryptoResponseModel()
            {
                CirculatingSupply = (long)json["data"]["BTC"][0]["circulating_supply"],
                Name = json["data"]["BTC"][0]["name"].ToString(),
                Price = (decimal)json["data"]["BTC"][0]["quote"]["USD"]["price"],
                MarketCap = (decimal)json["data"]["BTC"][0]["quote"]["USD"]["market_cap"],
                Ticker = json["data"]["BTC"][0]["symbol"].ToString(),
                PercentChange = (decimal)json["data"]["BTC"][0]["quote"]["USD"]["percent_change_24h"]
            };
            return model;

        }

        public async Task<string> GetImgUrl(string ticker)
        {
            HttpResponseMessage response = await client.GetAsync(ApiConstants.InfoPath + "?symbol=" + ticker);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var result = await response.Content.ReadAsStringAsync();
            JObject json = JObject.Parse(result);
            string url = json["data"][ticker]["logo"].ToString();
            return url;

        }
        public async Task<IEnumerable<CryptoResponseModel>> GetTopFive()
        {
            InitRequest();
            //change the request later for all cryptos in the database
            //take the tickers and add in request
            //foreach them on the page

            HttpResponseMessage response = await client.GetAsync(ApiConstants.LatestPath + "?symbol=btc,eth,bnb,ada,dot");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var result = await response.Content.ReadAsStringAsync();

            List<CryptoResponseModel> cryptos = new List<CryptoResponseModel>();
            JObject json = JObject.Parse(result);

            foreach (var crypto in json["data"])
            {
                var ticker = crypto.First[0]["symbol"].ToString();
                CryptoResponseModel model = new CryptoResponseModel()
                {
                    CirculatingSupply = (long)crypto.First[0]["circulating_supply"],
                    Name = crypto.First[0]["name"].ToString(),
                    Price = (decimal)crypto.First[0]["quote"]["USD"]["price"],
                    MarketCap = (decimal)crypto.First[0]["quote"]["USD"]["market_cap"],
                    Ticker = ticker,
                    PercentChange = (decimal)crypto.First[0]["quote"]["USD"]["volume_change_24h"]
                };
                var url = await GetImgUrl(ticker);
                model.Logo = url;
                cryptos.Add(model);
            }

            return cryptos;
        }
        private static void InitRequest()
        {
            if (!client.DefaultRequestHeaders.Contains("Accepts"))
            {
                client.DefaultRequestHeaders.Add("Accepts", "application/json");
                client.DefaultRequestHeaders.Add("X-CMC_PRO_API_KEY", ApiConstants.ApiKey);
            }
        }
    }
}

