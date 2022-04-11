using CryptoTradingPlatform.Data;
using CryptoTradingPlatform.Data.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CryptoTradingPlatform.Infrastructure.Data.Models
{
    public class ManagerApplication
    {
        public ManagerApplication()
        {
            Id = Guid.NewGuid().ToString();
        }

        [Key]
        [MaxLength(DataConstants.Idlength)]
        public string Id { get; set; }

        [Required]
        [MaxLength(DataConstants.ExperienceMaxLength)]
        public string Experience { get; set; }

        [MaxLength(DataConstants.ApplicationStatus)]
        public string? Status { get; set; }

        [Required]
        public DateTime DateApplied { get; set; }

        [Required]
        [MaxLength(DataConstants.QuestionMaxLength)]
        public string Question { get; set; }

        [ForeignKey(nameof(ApplicationUser))]
        public string ApplicationUserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}
