using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceApi.Models
{
    public class DeleteInvoiceRequest
    {
        public Guid InvoiceId { get; set; }
        public Guid UserId { get; set; }
    }
}
