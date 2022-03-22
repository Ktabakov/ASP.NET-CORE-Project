using CryptoTradingPlatform.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CryptoTradingPlatform.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            Money = 300000;
            Assets = new List<Asset>();
            Transactions = new List<Transaction>();
        }

        [Required]
        [Column(TypeName = "money")]
        public decimal Money { get; set; }

        public List<Asset> Assets { get; set; }

        public List<Transaction> Transactions { get; set; }

        public List<Article> Articles { get; set; }

    }
}
