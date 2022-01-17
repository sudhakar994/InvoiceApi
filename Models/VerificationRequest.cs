using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceApi.Models
{
    public class VerificationRequest
    {
        public string VerificationCode { get; set; }
        public string UserId { get; set; }
        public string Status { get; set; }
    }
}
