using System.ComponentModel.DataAnnotations;

namespace CryptoTradingPlatform.Models.Articles
{
    public class ArticleViewModel
    {
        public string Content { get; set; }

        [Display(Name = "Image URL")]
        public string ImageURL { get; set; }
    }
}
