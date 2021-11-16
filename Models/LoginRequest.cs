using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace InvoiceApi.Models
{
    [DataContract]
    public class LoginRequest
    {
        [DataMember,Required]
        public string Email { get; set; }
        [DataMember,Required]
        public string Password { get; set; }
    }
}
