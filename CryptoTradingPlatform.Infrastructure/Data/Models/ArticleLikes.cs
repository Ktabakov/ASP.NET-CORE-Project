using CryptoTradingPlatform.Data;
using CryptoTradingPlatform.Data.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CryptoTradingPlatform.Infrastructure.Data.Models
{
    public class ArticleLikes
    {
        [ForeignKey(nameof(Article))]
        public string ArticleId { get; set; }
        public Article Article { get; set; }

        [ForeignKey(nameof(ApplicationUser))]
        public string ApplicationUserId { get; set; }

        public ApplicationUser User { get; set; }

        [Required]
        public bool IsLiked { get; set; }
    }
}
