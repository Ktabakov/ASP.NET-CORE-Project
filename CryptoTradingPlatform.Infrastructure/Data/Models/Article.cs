using System.ComponentModel.DataAnnotations;

namespace CryptoTradingPlatform.Data.Models
{
    public class Article
    {
        public Article()
        {
            Id = Guid.NewGuid().ToString();
        }

        [Key]
        public string Id { get; set; }

        [Required]
        [MaxLength(DataConstants.ArticleMaxLength)]
        public string Content { get; set; }

        [Required]
        public string ImageURL { get; set; }
    }
}
