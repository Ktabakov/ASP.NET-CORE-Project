using System.ComponentModel.DataAnnotations;

namespace CryptoTradingPlatform.Data.Models
{
    public class User
    {

        public User()
        {
            Id = Guid.NewGuid().ToString();
            Money = 300000;
            Assets = new List<Asset>();
            Transactions = new List<Transaction>();
        }
        [Key]
        [MaxLength(DataConstants.Idlength)]
        public string Id { get; set; }

        [Required]
        [MaxLength(DataConstants.UsernameMaxLength)]
        public string Username { get; set; }

        [EmailAddress]
        [MaxLength(DataConstants.EmailMaxLength)]
        public string Email { get; set; }

        [Required]
        [MaxLength(DataConstants.PasswordMaxLength)]
        public string Password { get; set; }
        //client validation 20 char max

        [Required]
        [MaxLength(DataConstants.CreditCardMaxLength)]
        public string CreditCard { get; set; }
        //do Regex client validation

        [Required]
        public bool IsAdmin { get; set; }

        public decimal Money { get; set; }

        public List<Asset> Assets { get; set; }

        public List<Transaction> Transactions { get; set; }
    }
}
