using CryptoTradingPlatform.Data;
using CryptoTradingPlatform.Data.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CryptoTradingPlatform.Infrastructure.Data
{
    public class UserFovorites
    {
        [ForeignKey(nameof(ApplicationUser))]
        public string ApplicationUserId { get; set; }

        public ApplicationUser User { get; set; }

        [ForeignKey(nameof(Asset))]
        [MaxLength(DataConstants.Idlength)]
        public string AssetId { get; set; }

        public Asset Asset { get; set; }
    }
}
