using InvoiceApi.IServices;
using InvoiceApi.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using InvoiceApi.Constants;
using InvoiceApi.Repository;
using InvoiceApi.IRepository;
using System.Globalization;

namespace InvoiceApi.Services
{
    public class UserService : IUserService
    {
        private readonly IUserReposiotry _userReposiotry;
        private readonly IEmailService _emailService;
        public UserService(IUserReposiotry userRepository, IEmailService emailService)
        {
            _userReposiotry = userRepository;
            _emailService = emailService;
        }

        public string GenerateJwtToken(User user)
        {
            // generate token that is valid for 10 minutes only
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Utility.GetAppSettings(UserConstants.SecretKey));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim("UserId", user.UserId.ToString()),
                }),
                Expires = DateTime.Now.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<User> Register(User user)
        {
            return await _userReposiotry.Register(user);

        }

        public async Task<LoginResponse> ValidateUser(LoginRequest loginRequest)
        {
            return await _userReposiotry.ValidateUser(loginRequest);
        }

        public async Task<string> ValidateVerficationCode(VerificationRequest verificationRequest)
        {
            return await _userReposiotry.ValidateVerficationCode(verificationRequest);
        }

        public async Task<string> ResendCode(VerificationRequest verificationRequest)
        {
            return await _userReposiotry.ResendCode(verificationRequest);
        }

        public async Task<PasswordResetResponse> ResetPassword(ResetPasswordRequest resetPasswordRequest)
        {

            var response = await _userReposiotry.ResetPassword(resetPasswordRequest);
            if (response != null && response.Status == StatusType.Success.ToString() && !string.IsNullOrEmpty(response.UserId))
            {
                var url = Utility.GetAppSettings("WebAppUrl") + "resetpassword?id=" + Utility.EnryptString(response.UserId + "," + DateTime.Now.AddMinutes(10));
                var user = new User();
                user.Email = resetPasswordRequest.Email;
                await _emailService.ForgotPasswordEmail(user, url);
                response.Messages = "We've sent a password reset link to email";
            }
            return response;
        }

        public async  Task<ValidateResetPasswordLinkResponse> ValidateResetPasswordLink(ValidateResetPasswordLinkRequest validateResetPasswordLinkRequest)
        {
           
            var decryptedUrl = Utility.DecryptString(validateResetPasswordLinkRequest.Url);
            var userId= decryptedUrl?.Split(new Char[] { ',' })[0];
            validateResetPasswordLinkRequest.UserId = !string.IsNullOrEmpty(userId) ? userId:string.Empty ;
            var response = await _userReposiotry.ValidateResetPasswordLink(validateResetPasswordLinkRequest);
            if (response.Status == StatusType.Success.ToString())
            {
                var issuedTime = decryptedUrl.Substring(decryptedUrl.LastIndexOf(',') + 1).ToString();
                var passwordGeneratedDate = DateTime.ParseExact(issuedTime, "M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);
                var currentTime = DateTime.Now;
                if (!string.IsNullOrWhiteSpace(issuedTime))
                {
                    var value = DateTime.Compare(currentTime, passwordGeneratedDate);
                    if (value <= 0)
                    {
                        response.Status = StatusType.Success.ToString();
                        response.IsExpired = false;
                    }

                    else
                    {
                        response.Status = StatusType.Failure.ToString();
                        response.Messages = "Link expired!";
                        response.IsExpired = true;
                    }

                  

                }

            }
        
            return response;
        }

       public async Task<Base> UpdatePassword(UpdatePasswordRequest updatePasswordRequest)
        {
            return await _userReposiotry.UpdatePassword(updatePasswordRequest);
        }

        public Task<ResendEmail> ResendEmail(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
