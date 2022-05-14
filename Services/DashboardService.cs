using InvoiceApi.IRepository;
using InvoiceApi.IServices;
using InvoiceApi.Models;
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
    }
}
