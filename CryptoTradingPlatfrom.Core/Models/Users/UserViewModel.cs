using System.ComponentModel.DataAnnotations;

namespace CryptoTradingPlatfrom.Core.Models.Users
{
    public class UserViewModel
    {

        [Required]
        public string Username { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public string Role { get; set; }

    }
}
