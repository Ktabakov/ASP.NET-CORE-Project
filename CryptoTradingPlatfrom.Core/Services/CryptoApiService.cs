using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using CryptoTradingPlatform.Core.Constants;
using CryptoTradingPlatform.Core.Contracts;
using CryptoTradingPlatform.Core.Models.Api;
using CryptoTradingPlatfrom.Core.Cache;
using CryptoTradingPlatfrom.Core.Models.Api;
using Newtonsoft.Json.Linq;

namespace CryptoTradingPlatform.Core.Services
{
    public class CryptoApiService : ICryptoApiService
    {
        private readonly HttpClient client;

        public CryptoApiService(HttpClient _client)
        {
            client = _client;
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
       
       
        public async Task<List<CryptoResponseModel>> GetCryptos(List<string> tickers)
        {
            string secretString = null;
            var cacheResult = CacheModel.GetApiKey("coinmarketcapKey");
            if (cacheResult == null)
            {
                var kvUri = $"https://MyVaultCrypto.vault.azure.net";
                var secretClient = new SecretClient(new Uri(kvUri), new DefaultAzureCredential());
                var secret = await secretClient.GetSecretAsync("coinmarketcapKey");
                secretString = secret.Value.Value;
                CacheModel.AddApiKey("coinmarketcapKey", secretString);
            }
            else
            {
                secretString = cacheResult;
            }

            //var apikey = config.GetValue<string>("coinmarketcapKey");
            if (!client.DefaultRequestHeaders.Contains("Accepts"))
            {
                client.DefaultRequestHeaders.Add("Accepts", "application/json");
                client.DefaultRequestHeaders.Add("X-CMC_PRO_API_KEY", secretString);
            }

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
        public async Task<BuyAssetResponseModel> GetPrices(List<string> tickers)
        {
            BuyAssetResponseModel result = new BuyAssetResponseModel();
            List<CryptoResponseModel> model = await GetCryptos(tickers);

            result.BuyAssetPrice = model.Find(a => a.Ticker == tickers[0]).Price;
            result.BuyAssetTicker = model.Find(a => a.Ticker == tickers[0]).Ticker;
            result.SellAssetPrice = model.Find(a => a.Ticker == tickers[1]).Price;
            result.SellAssetTicker = model.Find(a => a.Ticker == tickers[1]).Ticker;

            return result;
        }
    }
}

