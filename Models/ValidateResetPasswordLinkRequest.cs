using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace InvoiceApi.Models
{
    public class ValidateResetPasswordLinkRequest
    {
        [DataMember, Required]
        public string Url { get; set; }

        public string UserId { get; set; }

    }
}
