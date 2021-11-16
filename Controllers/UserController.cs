using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvoiceApi.IServices;
using InvoiceApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceApi.Controllers
{
    [Route("api/user")]
    [ApiController]
    
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IJwtService _jwtService;
        public UserController(IUserService userService, IJwtService jwtService)
        {
            _userService = userService;
            _jwtService = jwtService;
        }

        [HttpGet]
        [Route("getuser")]
        public IActionResult GetUser(string name)
        {
            Guid userId = _jwtService.GetUserIdFromJwt();
            string obj = "Hi" + name;
            return Ok(obj);
        }


        #region Login
        /// <summary>
        /// Authenticate
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("authenticate")]
        [AllowAnonymous]
        public IActionResult Authenticate(LoginRequest loginRequest)
        {
            if (ModelState.IsValid)
            {
                var response = _userService.GenerateJwtToken(new User { UserId= "a9d1c6db-5cb1-4f95-93fd-aecb2d9e955f",UserName="Sudhakaran",Email=loginRequest.Email });
                return Ok(response);
            }

            else
            {
                return BadRequest();
            }
           
        }

        #endregion

        #region

        #endregion
    }
}