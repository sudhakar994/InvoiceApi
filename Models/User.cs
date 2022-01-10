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
        public string Email { get; set; }
        [DataMember, Required]
        public string UserName { get; set; }
        [DataMember]
        public string UserId { get; set; }
        [DataMember, Required]
        public string Password { get; set; }
        [DataMember]
        public string PasswordSalt { get; set; }
        [DataMember]
        public string JwtToken { get; set; }

    }
}
