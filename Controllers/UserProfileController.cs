using InvoiceApi.IServices;
using InvoiceApi.Models.UserProfile;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace InvoiceApi.Controllers
{
    [Route("api/userprofile")]
    [ApiController]
    [Produces("application/json")]
    public class UserProfileController : ControllerBase
    {
        #region Variable declaration

        private readonly IUserProfileService _userProfileService;

        #endregion

        public UserProfileController(IUserProfileService userProfileService)
        {
            _userProfileService = userProfileService;
        }
        [HttpGet]
        [Route("getprofile")]
        public async Task<IActionResult> GetProfileData(Guid userId)
        {
            var profileDetails = new ProfileDetails();

            if (userId != null && userId != Guid.Empty)
            {
                profileDetails = await _userProfileService.GetProfileDetail(userId);
                return Ok(profileDetails);
            }
            else
            {
                return BadRequest("Error occured");
            }
        }

        [HttpPut]
        [Route("changeProfilePassword")]
        public async Task<IActionResult> ChangeProfilePassword(UpdatePassword updatePassword)
        {
            if (ModelState.IsValid)
            {
                var response = await _userProfileService.ChangeProfilePassword(updatePassword);
                return Ok(response);
            }
            return BadRequest();
        }

    }
}
