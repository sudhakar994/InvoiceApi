using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace InvoiceApi.Models
{
    public class UpdatePasswordRequest
    {
        [DataMember,Required]
        public string Url { get; set; }
        [DataMember, Required]
        public string Password { get; set; }
        [DataMember]
        public string PasswordSalt { get; set; }
        [DataMember]
        public string UserId { get; set; }
    }
}
