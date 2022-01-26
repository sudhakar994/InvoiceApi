using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InvoiceApi.Constants;
using InvoiceApi.IRepository;
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
    [Produces("application/json")]
    public class UserController : ControllerBase
    {
        #region Variable Declaration

        private readonly IUserService _userService;
        private readonly IJwtService _jwtService;
        private readonly IHtmlReaderService _htmlReaderService;
        private readonly IEmailService _emailService;
        #endregion
        public UserController(IUserService userService, IJwtService jwtService, IHtmlReaderService htmlReaderService, IEmailService emailService)
        {
            _userService = userService;
            _jwtService = jwtService;
            _htmlReaderService = htmlReaderService;
            _emailService = emailService;

        }

        #region Login
        /// <summary>
        /// Authenticate
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("authenticate")]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate(LoginRequest loginRequest)
        {
            
            var response = new LoginResponse();
            if (ModelState.IsValid)
            {
                
                response = await _userService.ValidateUser(loginRequest);
                if (response.Status == StatusType.Success.ToString())
                {
                    //if user name and password is correct then generate jwt token
                    response.Email = loginRequest.Email;
                    response.JwtToken = _userService.GenerateJwtToken(new User { UserId = response.UserId, Email = loginRequest.Email, UserName = response.UserName });
                    return Ok(response);
                }

                else
                {
                    response.Message = "Invalid credentials";
                    return Ok(response);
                }


            }

            else
            {
                return BadRequest(response);
            }

        }

        #endregion

        #region Download Pdf
        [HttpGet]
        [Route("DownloadPdf")]
        public async Task<IActionResult> DownloadPdf()
        {
            string fileName = "testFile.pdf";
            var invoice = new Invoice { InvoiceNo = "009898" };
            var html = await _htmlReaderService.ReadHtmlFileAndConvert("InvoiceTemplates/BlueInvoice.cshtml", invoice);
            var pdfBytes = PdfService.GeneratePdf(html);
            if (pdfBytes != null)
                return File(pdfBytes, "application/pdf", fileName);
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
        [Route("register")]
        public async Task<IActionResult> Register(User user)
        {
            if (ModelState.IsValid)
            {
                var response = await _userService.Register(user);
                if (response != null && !string.IsNullOrEmpty(response.UserId) && !string.IsNullOrEmpty(response.VerificationCode) && response?.Status.ToLower() == "success")
                {
                    //generate Jwt Token
                    user.UserId = response.UserId;
                    user.VerificationCode = response.VerificationCode;
                    response.JwtToken = _userService.GenerateJwtToken(user);
                    response.Email = user.Email;
                    response.UserName = user.UserName;
                    //send email
                    if (!string.IsNullOrEmpty(response.JwtToken))
                    {
                        var emailValues = new EmailValues();
                        emailValues.Email = user.Email;
                        emailValues.UserName = user.UserName;
                        emailValues.VerificationCode = user.VerificationCode;
                        emailValues.Subject = EmailConstant.EmailSubject.SendVerificationcode;
                        emailValues.TemplateName = EmailConstant.EmailTemplate.VerficationCodeTemplate;
                        await _emailService.EmailSend(emailValues);
                        response.VerificationCode = string.Empty;
                    }
                    return Ok(response);
                }

                else if (response != null && response?.Status.ToLower() == "verified")
                {
                    return Ok(response);
                }
                else
                {
                    response.Status = "Failure";
                    return Ok(response);
                }
            }
            else
            {
                return BadRequest();
            }
        }

        #endregion

        #region Validation Verification Code to Email

        [HttpPost]
        [Route("validateverficationcode")]
        public async Task<IActionResult> ValidateVerficationCode(VerificationRequest verificationRequest)
        {
            if (ModelState.IsValid)
            {
                verificationRequest.UserId = _jwtService.GetUserIdFromJwt().ToString();

                if (!string.IsNullOrEmpty(verificationRequest.UserId) && !string.IsNullOrWhiteSpace(verificationRequest.VerificationCode))
                {
                    var response = await _userService.ValidateVerficationCode(verificationRequest);

                    if (response == Messages.Success)
                    {
                        //Send Welcome Email
                        var emailValues = new EmailValues();
                        var sendresponse = await _userService.ResendEmail(verificationRequest.UserId);

                        emailValues.Email = sendresponse.Email;
                        emailValues.UserName = sendresponse.UserName;
                        emailValues.Subject = EmailConstant.EmailSubject.WelcomeSubject;
                        emailValues.TemplateName = EmailConstant.EmailTemplate.WelcomeEmailTemplate;
                        await _emailService.EmailSend(emailValues);
                    }
                    return Ok(response);
                }
                else
                {
                    return BadRequest();
                }
            }
            return BadRequest();

        }
        #endregion

        #region Resend Verification Code to Email
        [HttpPost]
        [Route("resendcode")]
        public async Task<IActionResult> ResendVerificationCode(VerificationRequest verificationRequest)
        {
            var emailValues = new EmailValues();
            if (ModelState.IsValid)
            {
                verificationRequest.UserId = _jwtService.GetUserIdFromJwt().ToString();
                if (verificationRequest.UserId != null)
                {
                    var response = await _userService.ResendCode(verificationRequest);

                    //Fetch Resend Email detail
                    var resendresponse = await _userService.ResendEmail(verificationRequest.UserId);

                    //Email Service
                    emailValues.UserName = resendresponse.UserName;
                    emailValues.Email = resendresponse.Email;
                    emailValues.VerificationCode = resendresponse.VerificationCode;
                    emailValues.Subject = EmailConstant.EmailSubject.SendVerificationcode;
                    emailValues.TemplateName = EmailConstant.EmailTemplate.VerficationCodeTemplate;
                    await _emailService.EmailSend(emailValues);

                    return Ok(response);
                }
            }
            return BadRequest();

        }

        #endregion

        #region  Reset Password
        /// <summary>
        /// Reset Password
        /// </summary>
        /// <param name="resetPasswordRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("resetpassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest resetPasswordRequest)
        {
            var response = new PasswordResetResponse();
            if (ModelState.IsValid)
            {
                response = await _userService.ResetPassword(resetPasswordRequest);

                return Ok(response);
            }

            return BadRequest();
        }
        #endregion

        #region ValidateResetPasswordLink
        /// <summary>
        /// ValidateResetPasswordLink
        /// </summary>
        /// <param name="validateResetPasswordLinkRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("validateResetpasswordlink")]
        public async Task<IActionResult> ValidateResetPasswordLink(ValidateResetPasswordLinkRequest validateResetPasswordLinkRequest)
        {
            if (ModelState.IsValid)
            {
              var  response = await _userService.ValidateResetPasswordLink(validateResetPasswordLinkRequest);

                return Ok(response);
            }

            return BadRequest();
        }
        #endregion

        #region Update Password
        /// <summary>
        /// UpdatePassword
        /// </summary>
        /// <param name="updatePasswordRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("updatepassword")]
        public async Task<IActionResult> UpdatePassword(UpdatePasswordRequest updatePasswordRequest)
        {
            if (ModelState.IsValid)
            {
                var response = await _userService.UpdatePassword(updatePasswordRequest);

                return Ok(response);
            }

            return BadRequest();
        }
        #endregion

    }
}