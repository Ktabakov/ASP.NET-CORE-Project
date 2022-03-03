using System.ComponentModel.DataAnnotations;

namespace CryptoTradingPlatform.Models.Articles
{
    public class AddArticleFormModel
    {
        public string Content { get; set; }

        [Display(Name = "Image URL")]
        public string ImageURL { get; set; }
    }
}
