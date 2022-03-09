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


        [ForeignKey(nameof(User))]
        [MaxLength(DataConstants.Idlength)]
        public string UserId { get; set; }

        public User User { get; set; }
    }
}
