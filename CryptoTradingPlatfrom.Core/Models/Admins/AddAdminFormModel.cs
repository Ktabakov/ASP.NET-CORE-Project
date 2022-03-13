using System.ComponentModel.DataAnnotations;

namespace CryptoTradingPlatform.Core.Models.Admins
{
    public class AddAdminFormModel
    {
        [Required]
        public string Experience { get; set; }

        [Required]
        [MaxLength(ModelsConstants.QuestionMaxLength)]
        public string Question { get; set; }
    }
}
