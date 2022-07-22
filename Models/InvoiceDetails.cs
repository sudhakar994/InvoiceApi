using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceApi.Models
{
    public class InvoiceDetails:Base
    {
        public Guid UserId { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime InvoiceDueDate { get; set; }

        public Guid BusinessId { get; set; }
        public Guid ClientId { get; set; }
        public string Notes { get; set; }
        public string Terms { get; set; }
        public decimal Tax { get; set; }
        public decimal Discount { get; set; }
        public decimal Total { get; set; }
        public decimal SubTotal { get; set; }
        public string Taxtype { get; set; }
        public  string DiscountType { get; set; }
        public string TemplateName { get; set; }
        public List<TransactionDetails> Itemdetails { get; set; }
        public Clients ClientsDetails { get; set; }
        public Business BusinessDetails { get; set; }
    }

    
}
