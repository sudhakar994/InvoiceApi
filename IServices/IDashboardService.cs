
using InvoiceApi.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InvoiceApi.IServices
{
   public interface IDashboardService
    {
        Task<List<Business>> GetBusinessDetails(Guid userId);
        Task<List<Clients>> GetClientDetails(Guid userId);
        Task<InvoiceDetails> SaveInvoiceDetails(InvoiceDetails invoiceDetails);
    }
}
