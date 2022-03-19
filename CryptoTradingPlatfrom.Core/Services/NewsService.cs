using CryptoTradingPlatform.Core.Models.Articles;
using CryptoTradingPlatfrom.Core.Contracts;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var rnd = new Random();
            var apiKey = config.GetValue<string>("newsKey");

            HttpResponseMessage response = await client.GetAsync($"https://cryptopanic.com/api/v1/posts/?auth_token={apiKey}&public=true");
            List<NewsViewModel> list = new List<NewsViewModel>();
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var result = await response.Content.ReadAsStringAsync();
            JObject json = JObject.Parse(result);
            for (int i = 0; i < 5; i++)
            {
                int arNum = rnd.Next(0, json["results"].Count());
                int pictureId = rnd.Next(1, 20);
                

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
