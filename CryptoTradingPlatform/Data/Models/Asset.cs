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

        [Required]
        public string ImageURL { get; set; }

        [Required]
        [MaxLength(DataConstants.CreatorNameMaxLength)]
        public string CreatorName { get; set; }

        [Required]
        [Range(1, long.MaxValue)]
        public long TotalSypply { get; set; }

        [Required]
        [MaxLength(DataConstants.StoryMaxLength)]
        public string Story { get; set; }
    }
}
