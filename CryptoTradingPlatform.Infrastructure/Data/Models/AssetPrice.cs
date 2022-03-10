/*using CryptoTradingPlatform.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoTradingPlatform.Infrastructure.Data.Models
{
    public class AssetPrice
    {
        public AssetPrice()
        {
            Id = Guid.NewGuid().ToString();
        }

        [Key]
        public string Id { get; set; }

        [ForeignKey(nameof(Asset))]
        public string AssetId { get; set; }

        public Asset Asset { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [Column(TypeName ="money")]
        public decimal Price { get; set; }
    }
}
*/