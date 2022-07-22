using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceApi.Models
{
    public class TransactionDetails
    {
        public Guid InvoiceId { get; set; }
        public string ItemDescription { get; set; }
        public decimal Quantity { get; set; }

        public decimal Rate { get; set; }

        public decimal Amount { get; set; }
    }
}
