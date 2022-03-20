using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoTradingPlatfrom.Core.Models.Users
{
    public class ManagerApplicationViewModel
    {

        [Required]
        public string Experience { get; set; }

        [Required]
        [Display(Name = "Application Date")]
        public DateTime DateApplied { get; set; }

        [Required]
        public string Question { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string UserId { get; set; }
    }
}
