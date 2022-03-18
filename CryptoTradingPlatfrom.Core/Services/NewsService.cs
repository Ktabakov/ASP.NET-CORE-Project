﻿using CryptoTradingPlatform.Core.Models.Articles;
using CryptoTradingPlatfrom.Core.Contracts;
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
        public NewsService(HttpClient _httpClient)
        {
            client = _httpClient;
        }

        public async Task<List<NewsViewModel>> GetNews()
        {
            var rnd = new Random();

            HttpResponseMessage response = await client.GetAsync("https://cryptopanic.com/api/v1/posts/?auth_token=1580eb35061a2d6c12b22fe766bbc5c3cb3bfe8f&public=true");
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