
using InvoiceApi.Models;
using InvoiceApi.Models.Dashboard;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InvoiceApi.IServices
{
   public interface IDashboardService
    {
        Task<User> GetProfileDetail(Guid userId ,string email);
        Task<List<Business>> GetBusinessDetails(Guid userId);
        Task<List<Clients>> GetClientDetails(Guid userId);
        Task<InvoiceDetails> SaveInvoiceDetails(InvoiceDetails invoiceDetails);
        Task<List<UserInvoiceDetails>> GetInvoiceDetails(Guid userId);
        Task<InvoiceDetails> GetInvoiceDetailByInvoiceId(Guid userId,Guid invoiceId);
        Task<Base> DeleteInvoice(Guid userId, Guid invoiceId);
    }
}
