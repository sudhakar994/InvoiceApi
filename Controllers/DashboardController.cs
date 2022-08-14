using InvoiceApi.Constants;
using InvoiceApi.IServices;
using InvoiceApi.Models;
using InvoiceApi.Models.Dashboard;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InvoiceApi.Controllers
{

    [Route("api/dashboard")]
    [ApiController]
    [Produces("application/json")]
    public class DashboardController : Controller
    {
        #region Variable Declaration
        private readonly IDashboardService _dashboardService;
        private readonly IJwtService _jwtService;
        #endregion

        public DashboardController(IDashboardService dashboardService, IJwtService jwtService)
        {
            _dashboardService = dashboardService;
            _jwtService = jwtService;
        }
        [HttpGet]
        [Route("getprofile")]
        public  IActionResult GetProfileData(string userId, string token)
        {
            return Ok();
        }
        [HttpPut]
        [Route("updatepassword")]
        public  IActionResult UpdateUserPassword()
        {
            return Ok();
        }

        #region GetBusinessDetails
        /// <summary>
        /// GetBusinessDetails
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getbusinessdetails")]
        public async Task<IActionResult> GetBusinessDetails()
        {

            var response = new List<Business>();
            Guid userId = _jwtService.GetUserIdFromJwt();

            if(userId != Guid.Empty)
            {

                response =  await _dashboardService.GetBusinessDetails(userId);
                return Ok(response);
            }

            else
            {
                return BadRequest("Error occured");
            }
                
        }

        #endregion

        #region Get Client Details
        /// <summary>
        /// GetClientDetails
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getclientdetails")]
        public async Task<IActionResult> GetClientDetails()
        {

            var response = new List<Clients>();
            Guid userId = _jwtService.GetUserIdFromJwt();

            if (userId != Guid.Empty)
            {
                response = await _dashboardService.GetClientDetails(userId);
                return Ok(response);
            }

            else
            {
                return BadRequest("Error occured");
            }

        }
        #endregion

        #region SaveInvoiceDetails
        /// <summary>
        /// SaveInvoiceDetails
        /// </summary>
        /// <returns></returns>
         [HttpPost]
        [Route("saveinvoicedetail")]
        public async Task<IActionResult> SaveInvoiceDetails(InvoiceDetails invoiceDetails)
        {
           
            Guid userId = _jwtService.GetUserIdFromJwt();

            if (userId != Guid.Empty)
            {
                if(invoiceDetails != null && invoiceDetails.BusinessDetails != null && invoiceDetails.ClientsDetails !=null && invoiceDetails.Itemdetails.Count > 0)
                {
                    invoiceDetails.UserId = userId;
                    invoiceDetails.BusinessDetails.UserId = userId;
                    invoiceDetails.ClientsDetails.UserId = userId;
                    var   response = await _dashboardService.SaveInvoiceDetails(invoiceDetails);
                    return Ok(response);
                }

                else
                {
                    return BadRequest("Error occured");
                }
                
            }

            else
            {
                return BadRequest("Error occured");
            }
        }
        #endregion

        #region GetInvoiceDetails
        /// <summary>
        /// GetInvoiceDetails
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getuserinvoicedetail")]
        public async Task<IActionResult> GetInvoiceDetails()
        {
            var response = new List<UserInvoiceDetails>();
            Guid userId = _jwtService.GetUserIdFromJwt();
            if (userId != Guid.Empty)
            {
                     response = await _dashboardService.GetInvoiceDetails(userId);
                    return Ok(response);
            }

            else
            {
                return BadRequest("Error occured");
            }

        }
        #endregion

        #region Get Invoice DetailBy InvoiceId
        /// <summary>
        /// GetInvoiceDetailByInvoiceId
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getinvoicedetail")]
        public async Task<IActionResult> GetInvoiceDetailByInvoiceId(Guid invoiceId)
       {
            var response = new InvoiceDetails();

            Guid userId = _jwtService.GetUserIdFromJwt();
            if (userId != Guid.Empty && invoiceId != Guid.Empty)
            {
                response = await _dashboardService.GetInvoiceDetailByInvoiceId(userId,invoiceId);
                return Ok(response);
            }

            else
            {
                return Ok(response);
            }
        }
        #endregion
    }
}
