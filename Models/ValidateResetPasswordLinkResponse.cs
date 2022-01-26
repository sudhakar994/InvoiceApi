using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceApi.Models
{
    public class ValidateResetPasswordLinkResponse:Base
    {
        public bool IsExpired { get; set; }
    }
}
