using System.ComponentModel.DataAnnotations;

namespace CryptoTradingPlatform.Models.Articles
{
    public class AddArticleFormModel
    {
        [Required]
        public string Content { get; set; }

        [Required]
        [Display(Name = "Image URL")]
        public string ImageURL { get; set; }
    }
}
