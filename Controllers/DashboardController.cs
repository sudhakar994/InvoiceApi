using InvoiceApi.Constants;
using InvoiceApi.IServices;
using InvoiceApi.Models;
using InvoiceApi.Models.Dashboard;
using InvoiceApi.Services;
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
        private readonly IHtmlReaderService _htmlReaderService;
        #endregion

        public DashboardController(IDashboardService dashboardService, IJwtService jwtService, IHtmlReaderService htmlReaderService)
        {
            _dashboardService = dashboardService;
            _jwtService = jwtService;
            _htmlReaderService = htmlReaderService;
        }
        [HttpGet]
        [Route("getprofile")]
        public async Task<IActionResult> GetProfileData(string userId, string email)
        {
            var profileDetails = new UserProfile();

            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(email))
            {
                profileDetails = await _dashboardService.GetProfileDetail(Guid.Parse(userId), email);
                return Ok(profileDetails);
            }
            else
            {
                return BadRequest("Error occured");
            }
        }
        [HttpPut]
        [Route("updatepassword")]
        public IActionResult UpdateUserPassword()
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

            if (userId != Guid.Empty)
            {

                response = await _dashboardService.GetBusinessDetails(userId);
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
                if (invoiceDetails != null && invoiceDetails.BusinessDetails != null && invoiceDetails.ClientsDetails != null && invoiceDetails.Itemdetails.Count > 0)
                {
                    invoiceDetails.UserId = userId;
                    invoiceDetails.BusinessDetails.UserId = userId;
                    invoiceDetails.ClientsDetails.UserId = userId;
                    var response = await _dashboardService.SaveInvoiceDetails(invoiceDetails);
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

        #region Delete Invoice
        /// <summary>
        /// DeleteInvoice
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("deleteinvoice")]
        public async Task<IActionResult> DeleteInvoice(DeleteInvoiceRequest deleteInvoiceRequest)
        {
            var response = new Base { Status = StatusType.Failure.ToString() };

            deleteInvoiceRequest.UserId = _jwtService.GetUserIdFromJwt();
            if (deleteInvoiceRequest.UserId != Guid.Empty && deleteInvoiceRequest.InvoiceId != Guid.Empty)
            {
                response = await _dashboardService.DeleteInvoice(deleteInvoiceRequest.UserId, deleteInvoiceRequest.InvoiceId);
                return Ok(response);
            }

            else
            {
                return Ok(response);
            }

        }
        #endregion

        #region Download Invoice
        [HttpGet]
        [Route("downloadinvoice")]
        public async Task<IActionResult> DownloadPdf(Guid invoiceId)
        {
            string fileName = "testFile.pdf";
            var invoice = await _dashboardService.GetInvoiceDetailByInvoiceId(_jwtService.GetUserIdFromJwt(), invoiceId);
            var html = await _htmlReaderService.ReadHtmlFileAndConvert("InvoiceTemplates/BlueInvoice.cshtml", invoice);
            var pdfBytes =  PdfService.GeneratePdf(html);
            if (pdfBytes != null)
                return File(pdfBytes, "application/pdf", fileName);
            else
                return BadRequest("Error occured");
        }
        #endregion
    }
}
