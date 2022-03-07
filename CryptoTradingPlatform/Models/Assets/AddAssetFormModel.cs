using CryptoTradingPlatform.Data;
using System.ComponentModel.DataAnnotations;

namespace CryptoTradingPlatform.Models.Assets
{
    public class AddAssetFormModel
    {

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
        [Display(Name = "Creator Name")]
        public string CreatorName { get; set; }

        [Required]
        [Range(1, long.MaxValue)]
        [Display(Name = "Total Sypply")]
        public long TotalSypply { get; set; }

        [Required]
        [MaxLength(DataConstants.StoryMaxLength)]
        public string Story { get; set; }

    }
}
