/*using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CryptoTradingPlatform.Data.Models
{
    public class UserArticle
    {
        [ForeignKey(nameof(ApplicationUser))]
        [MaxLength(DataConstants.Idlength)]
        public string ApplicationUserId { get; set; }

        public ApplicationUser User { get; set; }

        [ForeignKey(nameof(Article))]

        [MaxLength(DataConstants.Idlength)]
        public string ArticleId { get; set; }

        public Article Article { get; set; }
    }
}
*/