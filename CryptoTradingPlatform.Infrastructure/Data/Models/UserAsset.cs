using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CryptoTradingPlatform.Data.Models
{
    public class UserAsset
    {
        [ForeignKey(nameof(Asset))]
        [MaxLength(DataConstants.Idlength)]
        public string AssetId { get; set; }

        public Asset Asset { get; set; }

        [Required]
        public double Quantity { get; set; }

        [ForeignKey(nameof(ApplicationUser))]
        public string ApplicationUserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}
