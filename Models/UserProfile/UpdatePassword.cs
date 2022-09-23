using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvoiceApi.Models.UserProfile
{
    public class UpdatePassword : ProfileDetails
    {
        [Required]
        public string CurrentPassword { get; set; }
        [Required]
        [Column("Password")]
        public string NewPassword { get; set; }
        public string PasswordSalt { get; set; }

    }
}
