using CryptoTradingPlatform.Core.Models.Articles;
using CryptoTradingPlatfrom.Core.Contracts;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoTradingPlatfrom.Core.Services
{
    public class ArticlesService : IArticlesService
    {
        static HttpClient client = new HttpClient();

        public async Task<List<ArticleViewModel>> GetArticles()
        {

            HttpResponseMessage response = await client.GetAsync("https://cryptopanic.com/api/v1/posts/?auth_token=1580eb35061a2d6c12b22fe766bbc5c3cb3bfe8f&public=true");
            List<ArticleViewModel> list = new List<ArticleViewModel>();
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var result = await response.Content.ReadAsStringAsync();
            JObject json = JObject.Parse(result);
            foreach (var item in json["results"])
            {
                ArticleViewModel model = new ArticleViewModel()
                {
                    DatePublished = item["published_at"].ToString(),
                    Title = item["title"].ToString(),
                    Url = item["url"].ToString(),
                };
                list.Add(model);
            }
            return list;
        }

    }
}
