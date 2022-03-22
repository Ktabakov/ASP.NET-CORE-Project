using CryptoTradingPlatform.Data;
using System.ComponentModel.DataAnnotations;

namespace CryptoTradingPlatform.Core.Models.Articles
{
    public class AddArticleFormModel
    {
        [Required]
        [Display(Name = "Article Name")]
        [StringLength(DataConstants.ArticleTitleMaxLength, MinimumLength = DataConstants.ArticleTitleMinLength)]
        public string Title { get; set; }

        [Required]
        [StringLength(DataConstants.ArticleMaxLength, MinimumLength = DataConstants.ArticleMinLength)]
        public string Content { get; set; }

        [Required]
        [Display(Name = "Image URL")]
        [MaxLength(DataConstants.ImageUrlMaxLength)]
        public string ImageURL { get; set; }
    }
}
