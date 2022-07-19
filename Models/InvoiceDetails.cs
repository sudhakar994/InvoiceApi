using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceApi.Models
{
    public class InvoiceDetails:Base
    {
        public string InvoiceNumber { get; set; }
        public string InvoiceDate { get; set; }
        public string DueDate { get; set; }
        public Guid BusinessId { get; set; }
        public Guid ClientId { get; set; }
        public string Notes { get; set; }
        public string Terms { get; set; }
        public decimal Tax { get; set; }
        public decimal Discount { get; set; }
        public decimal Total { get; set; }
        public string TemplateName { get; set; }
        public List<TransactionDetails> TransactionDetails { get; set; }
        public Clients Clients { get; set; }
        public Business Business { get; set; }
    }

    
}
