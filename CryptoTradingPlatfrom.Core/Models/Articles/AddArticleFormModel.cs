using System.ComponentModel.DataAnnotations;

namespace CryptoTradingPlatform.Core.Models.Articles
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
