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

        #endregion

        public DashboardController()
        {

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

    }
}
