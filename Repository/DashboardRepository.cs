﻿using InvoiceApi.Constants;
using InvoiceApi.IRepository;
using InvoiceApi.IServices;
using InvoiceApi.Models;
using InvoiceApi.Models.Dashboard;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceApi.Repository
{
    public class DashboardRepository : IDashboardRepository
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
            var response = new List<Business>();

            response = await _sqlService.GetListExecuteQueryasync<Business>(SqlQuery.GetBusinessDetails, new { UserId = userId });

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
            if(invoiceDetails.InvoiceId == Guid.Empty)
            {
                #region Save Invoice
                //Save business
                var businessId = await SaveBusiness(invoiceDetails.BusinessDetails);
                //Save clients
                var clientId = await SaveClients(invoiceDetails.ClientsDetails);
                if (businessId != Guid.Empty && clientId != Guid.Empty)
                {
                    invoiceDetails.BusinessId = businessId;
                    invoiceDetails.ClientId = clientId;
                    if (invoiceDetails.IsCustomDate)
                    {
                        invoiceDetails.InvoiceDate = null;
                    }
                    else
                    {
                        invoiceDetails.InvoiceDate = invoiceDetails.InvoiceDate?.AddDays(1);
                        if (invoiceDetails.InvoiceDueDate != null)
                        {
                            invoiceDetails.InvoiceDueDate = invoiceDetails.InvoiceDueDate?.AddDays(1);
                        }
                    }
                    
                    //save invoice details
                    Guid invoiceId = await _sqlService.GetSingleExecuteQueryasync<Guid>(SqlQuery.SaveInvoiceDetails, invoiceDetails);
                    if (invoiceId != Guid.Empty)
                    {
                        try
                        {
                            //save item details against invoice id
                            foreach (var item in invoiceDetails.Itemdetails)
                            {
                                item.InvoiceId = invoiceId;
                                await _sqlService.GetSingleExecuteQueryasync<long>(SqlQuery.SaveTransactionDetails, item);
                            }

                            //Save Logo
                          
                            long logoId = await _sqlService.GetSingleExecuteQueryasync<long>(SqlQuery.SelectLogoId, new { InvoiceId = invoiceId, UserId = invoiceDetails.UserId });
                            if (logoId > 0)
                            {
                                if (string.IsNullOrWhiteSpace(invoiceDetails.ImageBase64String))
                                {
                                   
                                    invoiceDetails.ImageBase64String = string.Empty;
                                }
                                if (string.IsNullOrWhiteSpace(invoiceDetails.LogoName))
                                {
                                    invoiceDetails.LogoName = string.Empty;
                                }
                                await _sqlService.GetSingleExecuteQueryasync<long>(SqlQuery.UpdateLogoDetails, new
                                {
                                    InvoiceId = invoiceId,
                                    LogoName = invoiceDetails.LogoName,
                                    UserId = invoiceDetails.UserId,
                                    LogoId = logoId,
                                    ImageBase64String = invoiceDetails.ImageBase64String
                                });
                            }
                            else
                            {
                                if (!string.IsNullOrWhiteSpace(invoiceDetails.ImageBase64String))
                                {
                                    await _sqlService.GetSingleExecuteQueryasync<long>(SqlQuery.SaveLogoDetails, new { InvoiceId = invoiceId, UserId = invoiceDetails.UserId, LogoName = invoiceDetails.LogoName, ImageBase64String = invoiceDetails.ImageBase64String });
                                }
                            }



                            #region //Save Signature 
                            long signatureId = await _sqlService.GetSingleExecuteQueryasync<long>(SqlQuery.SelectSignatureId, new { InvoiceId = invoiceId, UserId = invoiceDetails.UserId });
                            if (signatureId > 0)
                            {
                                if (string.IsNullOrWhiteSpace(invoiceDetails.SignatureBase64String))
                                {

                                    invoiceDetails.SignatureBase64String = string.Empty;
                                }

                                await _sqlService.GetSingleExecuteQueryasync<long>(SqlQuery.UpdateSignatureDetails, new
                                {
                                    InvoiceId = invoiceId,
                                    UserId = invoiceDetails.UserId,
                                    SignatureId = signatureId,
                                    SignatureBase64String = invoiceDetails.SignatureBase64String
                                });
                            }
                            else
                            {
                                if (!string.IsNullOrWhiteSpace(invoiceDetails.SignatureBase64String))
                                {
                                    await _sqlService.GetSingleExecuteQueryasync<long>(SqlQuery.SaveSignatureDetails, new { InvoiceId = invoiceId, UserId = invoiceDetails.UserId, LogoName = invoiceDetails.LogoName, SignatureBase64String = invoiceDetails.SignatureBase64String });
                                }
                            }

                            #endregion
                            response.Status = StatusType.Success.ToString();
                        }
                        catch (Exception ex)
                        {

                            response.Status = ex.Message;
                        }

                    }
                    else
                    {
                        response.Status = StatusType.Failure.ToString();
                    }


                }

                #endregion
            }

            else
            {
                #region  Update Invoice
                //Save business
                var businessId = await SaveBusiness(invoiceDetails.BusinessDetails);
                //Save clients
                var clientId = await SaveClients(invoiceDetails.ClientsDetails);
                if (invoiceDetails.IsCustomDate)
                {
                    invoiceDetails.InvoiceDate = null;
                }
                else
                {
                    invoiceDetails.InvoiceDate = invoiceDetails.InvoiceDate?.AddDays(1);
                }
                if (invoiceDetails.IsCustomDueDate)
                {
                    invoiceDetails.InvoiceDueDate = null;
                }
                else
                {
                    if (invoiceDetails.InvoiceDueDate != null)
                    {
                        invoiceDetails.InvoiceDueDate = invoiceDetails.InvoiceDueDate?.AddDays(1);
                    }
                }
               
                if (businessId != Guid.Empty && clientId != Guid.Empty)
                {
                    invoiceDetails.BusinessId = businessId;
                    invoiceDetails.ClientId = clientId;
                    //save invoice details
                   
                    Guid invoiceId = await _sqlService.GetSingleExecuteQueryasync<Guid>(SqlQuery.UpdateInvoiceDetails, invoiceDetails);
                    if (invoiceId != Guid.Empty)
                    {
                        try
                        {
                            //delete transaction details and save new one
                            await _sqlService.GetSingleExecuteQueryasync<long>(SqlQuery.DeleteTransactionDetails, new {InvoiceId= invoiceId });
                            //save item details against invoice id
                            foreach (var item in invoiceDetails.Itemdetails)
                            {
                                item.InvoiceId = invoiceId;
                                await _sqlService.GetSingleExecuteQueryasync<long>(SqlQuery.SaveTransactionDetails, item);
                            }
                            long logoId= await _sqlService.GetSingleExecuteQueryasync<long>(SqlQuery.SelectLogoId, new { InvoiceId = invoiceId, UserId = invoiceDetails.UserId });
                            if(logoId > 0)
                            {
                                if (string.IsNullOrWhiteSpace(invoiceDetails.ImageBase64String))
                                {
                                    
                                    invoiceDetails.ImageBase64String = string.Empty;
                                }
                                if (string.IsNullOrWhiteSpace(invoiceDetails.LogoName))
                                {
                                    invoiceDetails.LogoName = string.Empty;
                                }
                                await _sqlService.GetSingleExecuteQueryasync<long>(SqlQuery.UpdateLogoDetails, new
                                {
                                    InvoiceId = invoiceId,
                                    LogoName = invoiceDetails.LogoName,
                                    UserId = invoiceDetails.UserId,
                                    LogoId = logoId,
                                    ImageBase64String = invoiceDetails.ImageBase64String
                                });
                            }
                            else
                            {
                                if (!string.IsNullOrWhiteSpace(invoiceDetails.ImageBase64String))
                                {
                                    await _sqlService.GetSingleExecuteQueryasync<long>(SqlQuery.SaveLogoDetails, new { InvoiceId = invoiceId, UserId = invoiceDetails.UserId,LogoName= invoiceDetails.LogoName, ImageBase64String = invoiceDetails.ImageBase64String });
                                }
                            }
                            #region //Save Signature 
                            long signatureId = await _sqlService.GetSingleExecuteQueryasync<long>(SqlQuery.SelectSignatureId, new { InvoiceId = invoiceId, UserId = invoiceDetails.UserId });
                            if (signatureId > 0)
                            {
                                if (string.IsNullOrWhiteSpace(invoiceDetails.SignatureBase64String))
                                {

                                    invoiceDetails.SignatureBase64String = string.Empty;
                                }

                                await _sqlService.GetSingleExecuteQueryasync<long>(SqlQuery.UpdateSignatureDetails, new
                                {
                                    InvoiceId = invoiceId,
                                    UserId = invoiceDetails.UserId,
                                    SignatureId = signatureId,
                                    SignatureBase64String = invoiceDetails.SignatureBase64String
                                });
                            }
                            else
                            {
                                if (!string.IsNullOrWhiteSpace(invoiceDetails.SignatureBase64String))
                                {
                                    await _sqlService.GetSingleExecuteQueryasync<long>(SqlQuery.SaveSignatureDetails, new { InvoiceId = invoiceId, UserId = invoiceDetails.UserId,  SignatureBase64String = invoiceDetails.SignatureBase64String });
                                }
                            }
                            #endregion
                            response.Status = StatusType.Success.ToString();
                        }
                        catch (Exception ex)
                        {

                            response.Status = ex.Message;
                        }

                    }
                    else
                    {
                        response.Status = StatusType.Failure.ToString();
                    }


                }
                #endregion
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
            Guid businessId = Guid.Empty;
            //update business
            if (business.BusinessId != Guid.Empty)
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

        #region GetInvoiceDetails
        /// <summary>
        /// GetInvoiceDetails
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<UserInvoiceDetails>> GetInvoiceDetails(Guid userId)
        {
            var response = new List<UserInvoiceDetails>();

            response = await _sqlService.GetListExecuteQueryasync<UserInvoiceDetails>(SqlQuery.GetInvoiceDetails, new { UserId = userId });

            return response;
        }



        #endregion

        #region GetInvoiceDetailByInvoiceId
        /// <summary>
        /// GetInvoiceDetailByInvoiceId
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="invoiceId"></param>
        /// <returns></returns>
        public async Task<InvoiceDetails> GetInvoiceDetailByInvoiceId(Guid userId ,Guid invoiceId)
         {
            var response = new InvoiceDetails { Itemdetails =new List<TransactionDetails>()};
            response = await _sqlService.GetSingleExecuteQueryasync<InvoiceDetails>(SqlQuery.GetInvoiceDetailByInvoiceId, new { UserId = userId,InvoiceId=invoiceId });
            if(response != null && !string.IsNullOrWhiteSpace(response.InvoiceNumber)){

                if(response.InvoiceDate != null)
                {
                 response.FormattedInvoiceDate=   response.InvoiceDate?.ToString("MM-dd-yyyy");
                }
                if(response.InvoiceDueDate != null)
                {
                    response.FormattedInvoiceDueDate = response.InvoiceDueDate?.ToString("MM-dd-yyyy");
                }

                if (response.IsCustomDate)
                {
                    response.FormattedInvoiceDate = response.CustomDate;
                }
                if (response.IsCustomDueDate)
                {
                    response.FormattedInvoiceDueDate = !string.IsNullOrWhiteSpace(response.CustomDueDate) ? response.CustomDueDate :null;
                }
                response.Itemdetails = await _sqlService.GetListExecuteQueryasync<TransactionDetails>(SqlQuery.GetTransactionDetails, new { InvoiceId = invoiceId });
                response.ImageBase64String = await _sqlService.GetSingleExecuteQueryasync<string>(SqlQuery.GetImageSrcByInvoiceId, new { InvoiceId = invoiceId,UserId= userId });
                if (string.IsNullOrWhiteSpace(response.ImageBase64String))
                {
                    response.ImageBase64String = null;
                }
                response.SignatureBase64String = await _sqlService.GetSingleExecuteQueryasync<string>(SqlQuery.GetSignatureImageSrcByInvoiceId, new { InvoiceId = invoiceId, UserId = userId });
                if (string.IsNullOrWhiteSpace(response.SignatureBase64String))
                {
                    response.SignatureBase64String = null;
                }
                if (response.BusinessId != Guid.Empty)
                {
                    response.BusinessDetails = new Business();
                    response.BusinessDetails = await _sqlService.GetSingleExecuteQueryasync<Business>(SqlQuery.GetBusinessDetailsByBusinessId, new { BusinessId = response.BusinessId, UserId = userId });
                    if(response.BusinessDetails != null)
                    {
                       
                        if (!string.IsNullOrWhiteSpace(response.BusinessDetails.City))
                        {
                            response.BusinessDetails.FormattedAddress = response.BusinessDetails.City;
                        }
                        if (!string.IsNullOrWhiteSpace(response.BusinessDetails.State))
                        {
                            if (!string.IsNullOrWhiteSpace(response.BusinessDetails.FormattedAddress)){
                                response.BusinessDetails.FormattedAddress += "," + response.BusinessDetails.State;
                            }
                            else
                            {
                                response.BusinessDetails.FormattedAddress += response.BusinessDetails.State;
                            }
                           
                        }
                        if (!string.IsNullOrWhiteSpace(response.BusinessDetails.CountryId))
                        {
                            if(!string.IsNullOrWhiteSpace(response.BusinessDetails.FormattedAddress))
                            {
                                response.BusinessDetails.FormattedAddress += "," + response.BusinessDetails.CountryId;
                            }
                            else
                            {
                                response.BusinessDetails.FormattedAddress +=   response.BusinessDetails.CountryId;
                            }
                           
                        }
                    }
                }
                if (response.ClientId != Guid.Empty)
                {
                    response.ClientsDetails = new Clients();
                    response.ClientsDetails = await _sqlService.GetSingleExecuteQueryasync<Clients>(SqlQuery.GetClientDetailsByClientId, new { ClientId = response.ClientId, UserId = userId });
                    if (response.BusinessDetails != null)
                    {
                        var formattedAddress = string.Empty;
                        if (!string.IsNullOrWhiteSpace(response.ClientsDetails.City))
                        {
                            response.ClientsDetails.FormattedAddress = response.ClientsDetails.City;
                        }
                        if (!string.IsNullOrWhiteSpace(response.ClientsDetails.State))
                        {
                            if (!string.IsNullOrWhiteSpace(response.ClientsDetails.FormattedAddress))
                            {
                                response.ClientsDetails.FormattedAddress += "," + response.ClientsDetails.State;
                            }
                            else
                            {
                                response.ClientsDetails.FormattedAddress +=  response.ClientsDetails.State;
                            }
                            
                        }
                        if (!string.IsNullOrWhiteSpace(response.ClientsDetails.CountryId))
                        {
                            if (!string.IsNullOrWhiteSpace(response.ClientsDetails.FormattedAddress))
                            {
                                response.ClientsDetails.FormattedAddress += "," + response.ClientsDetails.CountryId;
                            }
                            else
                            {
                                response.ClientsDetails.FormattedAddress +=  response.ClientsDetails.CountryId;
                            }
                           
                        }
                    }
                }
            }
            return response;
        }
        #endregion

        #region DeleteInvoice
        /// <summary>
        /// DeleteInvoice
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="invoiceId"></param>
        /// <returns></returns>
        public async Task<Base> DeleteInvoice(Guid userId, Guid invoiceId)
        {
            var response = new Base { Status = StatusType.Failure.ToString() };
            if(userId != Guid.Empty && invoiceId != Guid.Empty)
            {
                await _sqlService.GetSingleExecuteQueryasync<long>(SqlQuery.DeleteLogoDetails, new { InvoiceId = invoiceId, UserId = userId });
                await _sqlService.GetSingleExecuteQueryasync<long>(SqlQuery.DeleteSignatureDetails, new { InvoiceId = invoiceId, UserId = userId });

                await _sqlService.GetSingleExecuteQueryasync<long>(SqlQuery.DeleteTransactionDetails, new { InvoiceId = invoiceId });
                await _sqlService.GetSingleExecuteQueryasync<long>(SqlQuery.DeleteInvoiceDetails, new { InvoiceId = invoiceId ,UserId= userId });
                response.Status = StatusType.Success.ToString();
            }
            return response;
        }
        #endregion

        #region

      


        #endregion
    }
}
