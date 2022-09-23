using InvoiceApi.Models;
using InvoiceApi.Models.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceApi.IRepository
{
    public interface IDashboardRepository
    {
        Task<List<Business>> GetBusinessDetails(Guid userId);
        Task<List<Clients>> GetClientDetails(Guid userId);
        Task<InvoiceDetails> SaveInvoiceDetails(InvoiceDetails invoiceDetails);
        Task<List<UserInvoiceDetails>> GetInvoiceDetails(Guid userId);
        Task<InvoiceDetails> GetInvoiceDetailByInvoiceId(Guid userId,Guid invoiceId);
        Task<Base> DeleteInvoice(Guid userId, Guid invoiceId);
    }
}
