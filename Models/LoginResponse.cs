using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceApi.Models
{
    public class LoginResponse:User
    {
       public string Message { get; set; }

    }
}
