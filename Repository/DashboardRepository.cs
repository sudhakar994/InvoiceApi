using InvoiceApi.Constants;
using InvoiceApi.IRepository;
using InvoiceApi.IServices;
using InvoiceApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceApi.Repository
{
    public class DashboardRepository:IDashboardRepository
    {
       private readonly ISqlService _sqlService;

        public DashboardRepository(ISqlService sqlService)
        {
            _sqlService = sqlService;
        }

        #region GetBusinessDetails
        /// <summary>
        /// GetBusinessDetails
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<Business>> GetBusinessDetails(Guid userId)
        {
            var response = new List <Business>();

            response = await _sqlService.GetListExecuteQueryasync<Business>(SqlQuery.GetBusinessDetails, new {UserId= userId });

            return response;
        }

        #endregion

        #region Get Client Details
        /// <summary>
        /// GetClientDetails
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<Clients>> GetClientDetails(Guid userId)
        {
            var response = new List<Clients>();

            response = await _sqlService.GetListExecuteQueryasync<Clients>(SqlQuery.GetClientDetails, new { UserId = userId });

            return response;
        }

        #endregion

        #region SaveInvoiceDetails
        /// <summary>
        /// SaveInvoiceDetails
        /// </summary>
        /// <param name="invoiceDetails"></param>
        /// <returns></returns>
        public async Task<InvoiceDetails> SaveInvoiceDetails(InvoiceDetails invoiceDetails)
        {
            var response = new InvoiceDetails();

            response = await _sqlService.GetSingleExecuteQueryasync<InvoiceDetails>(SqlQuery.GetClientDetails, invoiceDetails );

            return response;
        }
        #endregion
    }
}
