using CryptoTradingPlatform.Infrastructure.Data.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [MaxLength(DataConstants.ArticleTitleMaxLength)]
        public string Title { get; set; }

        [Required]
        [MaxLength(DataConstants.ArticleMaxLength)]
        public string Content { get; set; }

        [Required]
        [MaxLength(DataConstants.ImageUrlMaxLength)]
        public string ImageURL { get; set; }

        [ForeignKey(nameof(ApplicationUser))]
        public string ApplicationUserId { get; set; }

        public ApplicationUser User { get; set; }

        [Required]
        public DateTime DateAdded { get; set; }
    }
}
