﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InvoiceApi.IServices;
using InvoiceApi.Models;
using InvoiceApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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
        private readonly IHtmlReaderService _htmlReaderService;
        public UserController(IUserService userService, IJwtService jwtService, IHtmlReaderService htmlReaderService)
        {
            _userService = userService;
            _jwtService = jwtService;
            _htmlReaderService = htmlReaderService;
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

        #region Download Pdf
        [HttpGet] 
        [Route("DownloadPdf")]
        public async Task<IActionResult>  DownloadPdf()
        {
            string fileName = "testFile.pdf";
            var invoice = new Invoice { InvoiceNo = "009898" };
            var html = await _htmlReaderService.ReadHtmlFileAndConvert("InvoiceTemplates/BlueInvoice.cshtml", invoice);
            var pdfBytes = PdfService.GeneratePdf(html);
            if (pdfBytes != null)
                return  File(pdfBytes, "application/pdf", fileName);
            else
                return BadRequest("Error occured");
        }

        #endregion

        #region User Register
        /// <summary>
        /// Register
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult>  Register(User user)
        {
            if (ModelState.IsValid)
            {
              var response =    await _userService.Register(user);
                if(response != null && !string.IsNullOrEmpty(response.JwtToken))
                {
                    return Ok(response);
                }
                else
                {
                    return BadRequest("Registration Failed Please Try Again");
                }
               
                
            }
            else
            {
                return BadRequest();
            }
          
        }

        #endregion
    }
}