using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using CryptoTradingPlatform.Core.Models.Articles;
using CryptoTradingPlatfrom.Core.Cache;
using CryptoTradingPlatfrom.Core.Contracts;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;

namespace CryptoTradingPlatfrom.Core.Services
{
    public class NewsService : INewsService
    {
        private readonly HttpClient client;
        private readonly IConfiguration config;

        public NewsService(HttpClient _httpClient, IConfiguration _config)
        {
            client = _httpClient;
            config = _config;
        }

        public async Task<List<NewsViewModel>> GetNews()
        {
            string secretString = null;
            var cacheResult = CacheModel.GetApiKey("newsKey");
            if (cacheResult == null)
            {
                var kvUri = $"https://MyVaultCrypto.vault.azure.net";
                var secretClient = new SecretClient(new Uri(kvUri), new DefaultAzureCredential());
                var secret = await secretClient.GetSecretAsync("newsKey");
                secretString = secret.Value.Value;
                CacheModel.AddApiKey("newsKey", secretString);
            }
            else
            {
                secretString = cacheResult;
            }

            var rnd = new Random();
            //var apiKey = config.GetValue<string>("newsKey");

            HttpResponseMessage response = await client.GetAsync($"https://cryptopanic.com/api/v1/posts/?auth_token={secretString}&public=true");
            List<NewsViewModel> list = new List<NewsViewModel>();
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var result = await response.Content.ReadAsStringAsync();
            JObject json = JObject.Parse(result);
            for (int i = 0; i < 6; i++)
            {
                int arNum = rnd.Next(0, json["results"].Count());
                int pictureId = rnd.Next(1, 25);
                

                if (list.Any(c => c.Title == json["results"][arNum]["title"].ToString()))
                {
                    i--;
                    continue;
                }
                
                NewsViewModel model = new NewsViewModel()
                {
                    DatePublished = json["results"][arNum]["published_at"].ToString(),
                    Title = json["results"][arNum]["title"].ToString(),
                    Url = json["results"][arNum]["url"].ToString(),
                    PictureId = pictureId,
                    Source = json["results"][arNum]["source"]["title"].ToString(),
                };

                list.Add(model);
            }

           
            return list;
        }

    }
}
