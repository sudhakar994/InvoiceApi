using InvoiceApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceApi.IRepository
{
    public interface IDashboardRepository
    {
        Task<User> GetProfileDetail(Guid userId, string email);
        Task<List<Business>> GetBusinessDetails(Guid userId);
        Task<List<Clients>> GetClientDetails(Guid userId);
        Task<InvoiceDetails> SaveInvoiceDetails(InvoiceDetails invoiceDetails);
    }
}
