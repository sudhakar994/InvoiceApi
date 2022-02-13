using InvoiceApi.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceApi.IServices
{
    public interface IUserService
    {
        string GenerateJwtToken(User user);
        Task<User> Register(User user);        
        Task<LoginResponse> ValidateUser(LoginRequest loginRequest);
        Task<PasswordResetResponse> ResetPassword(ResetPasswordRequest resetPasswordRequest);
        Task<ValidateResetPasswordLinkResponse> ValidateResetPasswordLink(ValidateResetPasswordLinkRequest validateResetPasswordLinkRequest);
        Task<Base> UpdatePassword(UpdatePasswordRequest updatePasswordRequest);
        Task<string> ValidateVerficationCode(VerificationCodeResponse user);
        Task<VerificationCodeResponse> ResendCode(VerificationCodeResponse VerificationCodeResponse);
        Task<VerificationCodeResponse> GetDetailsForResendCode(VerificationCodeResponse VerificationCodeResponse);
    }
}

