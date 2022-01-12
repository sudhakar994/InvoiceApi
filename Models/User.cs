using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace InvoiceApi.Models
{
    public class User
    {
        [DataMember,Required, RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Invalid Email")]
        [StringLength(120, ErrorMessage = "Maximum 120 characters for email")]
        public string Email { get; set; }
        [DataMember, Required]
        public string UserName { get; set; }
        [DataMember]
        public string UserId { get; set; }
        [DataMember, Required]
        [StringLength(20, MinimumLength = 8 ,  ErrorMessage ="Minimum 8 characters")]
        public string Password { get; set; }
        [DataMember]
        public string PasswordSalt { get; set; }
        [DataMember]
        public string JwtToken { get; set; }
        [DataMember]
        public string VerificationCode { get; set; }
        [DataMember]
        public string Status { get; set; }
        [DataMember]
        public int RoleId { get; set; }
    }
}
