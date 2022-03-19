using System.ComponentModel.DataAnnotations;

namespace CryptoTradingPlatform.Core.Models.Users
{
    public class AddManagerFormModel
    {
        [Required]
        public string Experience { get; set; }

        [Required]
        [StringLength(ModelsConstants.QuestionMaxLength, MinimumLength = ModelsConstants.QuestionMinLength)]
        public string Question { get; set; }
    }
}
