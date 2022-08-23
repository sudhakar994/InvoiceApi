﻿using InvoiceApi.IRepository;
using InvoiceApi.IServices;
using InvoiceApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceApi.Services
{
    public class DashboardService : IDashboardService
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

        public async Task<User> GetProfileDetail(Guid userId, string email)
        {
            return await _dashboardRepository.GetProfileDetail(userId, email);
        }

    }
}
