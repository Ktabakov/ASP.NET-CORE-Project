using CryptoTradingPlatform.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CryptoTradingPlatform.Infrastructure.Data.Models
{
    public class Treasury
    {
        public Treasury()
        {
            Id = Guid.NewGuid().ToString();
        }

        [Key]
        [MaxLength(DataConstants.Idlength)]
        public string Id { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        [Column(TypeName = "money")]
        public decimal Total { get; set; }
    }
}
