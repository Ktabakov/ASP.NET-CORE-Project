using CryptoTradingPlatform.Core.Constants;
using CryptoTradingPlatform.Core.Contracts;
using CryptoTradingPlatform.Core.Models.Api;
using CryptoTradingPlatfrom.Core.Models.Api;
using Newtonsoft.Json.Linq;

namespace CryptoTradingPlatform.Core.Services
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

        public async Task<List<ImageDescriptionResponseModel>> GetImgUrls(List<string> tickers)
        {
            HttpResponseMessage response = await client.GetAsync(ApiConstants.InfoPath + "?symbol=" + string.Join(',', tickers));
            List<ImageDescriptionResponseModel> list = new List<ImageDescriptionResponseModel>();
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var result = await response.Content.ReadAsStringAsync();
            JObject json = JObject.Parse(result);
            foreach (var item in json["data"])
            {
                ImageDescriptionResponseModel model = new ImageDescriptionResponseModel()
                {
                    ImageUrl = item.First["logo"].ToString(),
                    Description = item.First["description"].ToString()
                };
                list.Add(model);
            }
            return list;

        }
        public async Task<IEnumerable<CryptoResponseModel>> GetTopFive(List<string> tickers)
        {
            InitRequest();
            //change the request later for all cryptos in the database
            //take the tickers and add in request
            //foreach them on the page

            HttpResponseMessage response = await client.GetAsync(ApiConstants.LatestPath + "?symbol=" + string.Join(',',tickers));

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var result = await response.Content.ReadAsStringAsync();

            List<CryptoResponseModel> cryptos = new List<CryptoResponseModel>();
            List<string> list = new List<string>();
            JObject json = JObject.Parse(result);

            foreach (var crypto in json["data"])
            {
                CryptoResponseModel model = new CryptoResponseModel()
                {
                    CirculatingSupply = (long)crypto.First[0]["circulating_supply"],
                    Name = crypto.First[0]["name"].ToString(),
                    Price = (decimal)crypto.First[0]["quote"]["USD"]["price"],
                    MarketCap = (decimal)crypto.First[0]["quote"]["USD"]["market_cap"],
                    Ticker = crypto.First[0]["symbol"].ToString(),
                    PercentChange = (decimal)crypto.First[0]["quote"]["USD"]["percent_change_24h"]
                };
                cryptos.Add(model);
                list.Add(model.Ticker);
            }
            List<ImageDescriptionResponseModel> urls = await GetImgUrls(list);
            for (int i = 0; i < cryptos.Count; i++)
            {
                cryptos[i].Logo = urls[i].ImageUrl;
                cryptos[i].Description = urls[i].Description;
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

