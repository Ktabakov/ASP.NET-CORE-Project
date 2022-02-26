using System.ComponentModel.DataAnnotations;

namespace CryptoTradingPlatform.Data.Models
{
    public class Asset
    {
        public Asset()
        {
            Id = Guid.NewGuid().ToString();
        }

        [Key]
        [MaxLength(DataConstants.Idlength)]
        public string Id { get; set; }

        [Required]
        [MaxLength(DataConstants.AssetMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(DataConstants.TickerMaxLength)]
        public string Ticker { get; set; }

        public string ImageURL { get; set; }
    }
}
