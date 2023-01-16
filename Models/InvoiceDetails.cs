using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceApi.Models
{
    public class InvoiceDetails:Base
    {
        public Guid InvoiceId { get; set; }
        public Guid UserId { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public DateTime? InvoiceDueDate { get; set; }
        public  string FormattedInvoiceDate { get; set; }
        public string FormattedInvoiceDueDate { get; set; }
        public bool IsCustomDate { get; set; }
        public bool IsCustomDueDate { get; set; }
        public string CustomDate { get; set; }
        public string CustomDueDate { get; set; }
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
        public decimal TaxPercentage { get; set; }
        public decimal DiscountPercentage { get; set; }
        public string ImageBase64String { get; set; }
        public string LogoName { get; set; }
        public long LogoId { get; set; }
        public string CurrencyCode { get; set; }
        public string SignatureId { get; set; }
        public string SignatureBase64String { get; set; }
        public string TaxLabel { get; set; }
        public string DiscountLabel { get; set; }
        public string TotalLabel { get; set; }
        public string SubTotalLabel { get; set; }
        public string SignatureLabel { get; set; }
        public string TermsLabel { get; set; }
    }

    
}
