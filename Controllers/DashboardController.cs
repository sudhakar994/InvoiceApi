using InvoiceApi.IServices;
using InvoiceApi.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<IActionResult> GetProfileData(string userId, string token)
        {
            return Ok();
        }
        [HttpPut]
        [Route("updatepassword")]
        public async Task<IActionResult> UpdateUserPassword()
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
                response = await _dashboardService.GetBusinessDetails(userId);
                return Ok(response);
            }

            else
            {
                return BadRequest("Error occured");
            }
                
        }

        #endregion

    }
}
