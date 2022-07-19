using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceApi.Models
{
    public class TransactionDetails
    {
        public long TransactionId { get; set; }
        public Guid InvoiceId { get; set; }

        public string Description { get; set; }
        public decimal Quantity { get; set; }

        public decimal Rate { get; set; }

        public decimal Amount { get; set; }
    }
}
