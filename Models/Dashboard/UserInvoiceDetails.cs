using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceApi.Models.Dashboard
{
    public class UserInvoiceDetails
    {
        public Guid InvoiceId { get; set; }
        public string InvoiceNo { get; set; }
        public string BusinessName { get; set; }
        public string ClientName { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime InvoiceDueDate { get; set; }
        public decimal InvoiceAmount { get; set; }
        public bool IsCustomDate { get; set; }
        public string CustomDate { get; set; }
        public bool IsCustomDueDate { get; set; }
        public string CustomDueDate { get; set; }
        public string CurrencyCode { get; set; }
    }
}
