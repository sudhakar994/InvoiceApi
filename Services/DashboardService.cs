using InvoiceApi.IRepository;
using InvoiceApi.IServices;
using InvoiceApi.Models;
using InvoiceApi.Models.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceApi.Services
{
    public class DashboardService:IDashboardService
    {
        private readonly IDashboardRepository _dashboardRepository;
        public DashboardService(IDashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
        } 

        public async Task<List<Business>> GetBusinessDetails(Guid userId)
        {
            return await _dashboardRepository.GetBusinessDetails(userId);
        }

        public async Task<List<Clients>> GetClientDetails(Guid userId)
        {
            return await _dashboardRepository.GetClientDetails(userId);
        }

        public async Task<InvoiceDetails> SaveInvoiceDetails(InvoiceDetails invoiceDetails)
        {
            return await _dashboardRepository.SaveInvoiceDetails(invoiceDetails);
        }

        public async Task<List<UserInvoiceDetails>> GetInvoiceDetails(Guid userId)
        {
            return await _dashboardRepository.GetInvoiceDetails(userId);
        }
        public async Task<InvoiceDetails> GetInvoiceDetailByInvoiceId(Guid userId,Guid invoiceId)
        {
            return await _dashboardRepository.GetInvoiceDetailByInvoiceId(userId, invoiceId);
        }

        public async Task<Base> DeleteInvoice(Guid userId, Guid invoiceId)
        {
            return await _dashboardRepository.DeleteInvoice(userId, invoiceId);
        }
    }
}
