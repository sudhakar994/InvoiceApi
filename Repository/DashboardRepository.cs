using InvoiceApi.Constants;
using InvoiceApi.IRepository;
using InvoiceApi.IServices;
using InvoiceApi.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
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
            var response = new InvoiceDetails {Status = StatusType.Failure.ToString() };

            //Save business
            var businessId = await SaveBusiness(invoiceDetails.BusinessDetails);
            //Save clients
            var clientId = await SaveClients(invoiceDetails.ClientsDetails);
            if(businessId != Guid.Empty && clientId != Guid.Empty)
            {
                invoiceDetails.BusinessId = businessId;
                invoiceDetails.ClientId = clientId;
                //save invoice details
                if (invoiceDetails.InvoiceDate == null)
                    invoiceDetails.InvoiceDate = DateTime.Now;
               Guid invoiceId= await _sqlService.GetSingleExecuteQueryasync<Guid>(SqlQuery.SaveInvoiceDetails, invoiceDetails);
                if(invoiceId != Guid.Empty)
                {
                    try
                    {
                        //save item details against invoice id
                        foreach (var item in invoiceDetails.Itemdetails)
                        {
                            item.InvoiceId = invoiceId;
                            await _sqlService.GetSingleExecuteQueryasync<long>(SqlQuery.SaveTransactionDetails, item);
                        }
                        response.Status = StatusType.Success.ToString();
                    }
                    catch(Exception ex)
                    {
                        
                        response.Status = ex.Message;
                    }
                   
                }
                else
                {
                    response.Status = StatusType.Failure.ToString();
                }


            }
            return response;
        }
        #endregion

        #region
        /// <summary>
        /// 
        /// </summary>
        /// <param name="business"></param>
        /// <returns></returns>
        public async Task<Guid> SaveBusiness(Business business)
        {
            Guid businessId =Guid.Empty;
            //update business
            if(business.BusinessId != Guid.Empty)
            {
                businessId = business.BusinessId;
                await _sqlService.GetSingleExecuteQueryasync<Guid>(SqlQuery.UpdateBusinessByBusinessId, business);
            }
            //save business
            else
            {
                businessId = await _sqlService.GetSingleExecuteQueryasync<Guid>(SqlQuery.SaveBusiness, business);
            }


            return businessId;
        }


        #endregion


        #region Save Client
        /// <summary>
        /// SaveClients
        /// </summary>
        /// <param name="clients"></param>
        /// <returns></returns>
        public async Task<Guid> SaveClients(Clients clients)
        {
            Guid clientId = Guid.Empty;
            //update client
            if (clients.ClientId != Guid.Empty)
            {
                clientId = clients.ClientId;
                await _sqlService.GetSingleExecuteQueryasync<Guid>(SqlQuery.UpdateClientsByClientId, clients);
            }
            //save client
            else
            {
                clientId = await _sqlService.GetSingleExecuteQueryasync<Guid>(SqlQuery.SaveClient, clients);
            }
            return clientId;

        }

        #endregion
    }
}
