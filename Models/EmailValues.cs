using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceApi.Models
{
    public class EmailValues
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string VerificationCode { get; set; }
        public string Subject { get; set; }
        public string TemplateName { get; set; }
        public string Url { get; set; }
    }
}
