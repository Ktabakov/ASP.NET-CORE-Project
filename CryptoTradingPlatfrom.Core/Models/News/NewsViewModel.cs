using System.ComponentModel.DataAnnotations;

namespace CryptoTradingPlatform.Core.Models.Articles
{
    public class NewsViewModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Published At")]
        public string DatePublished { get; set; }

        [Required]
        public string Url { get; set; }

        public int PictureId { get; set; }

        public string Source { get; set; }
    }
}
