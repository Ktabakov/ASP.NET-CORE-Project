using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CryptoTradingPlatform.Data.Models
{
    public class Transaction
    {
        public Transaction()
        {
            Id = Guid.NewGuid().ToString();
        }

        [Key]
        [MaxLength(DataConstants.Idlength)]
        public string Id { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal PriceBoughtFor { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal PriceSoldFor { get; set; }

        [Required]
        public DateTime DateBoughtOn { get; set; }

        [Required]
        public DateTime DateSoldOn { get; set; }

        [Required]
        [ForeignKey(nameof(Asset))]
        [MaxLength(DataConstants.Idlength)]
        public string AssetId { get; set; }

        public Asset Asset { get; set; }

        [Required]
        [ForeignKey(nameof(User))]
        [MaxLength(DataConstants.Idlength)]
        public string UserId { get; set; }

        public User User { get; set; }
    }
}
