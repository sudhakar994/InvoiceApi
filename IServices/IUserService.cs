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
        Task<string> ValidateVerficationCode(VerificationRequest user);
        Task<string> ResendCode(VerificationRequest verificationRequest);
        Task<ResendEmail> ResendEmail(string userId);
        Task<LoginResponse> ValidateUser(LoginRequest loginRequest);
        Task<PasswordResetResponse> ResetPassword(ResetPasswordRequest resetPasswordRequest);
        Task<ValidateResetPasswordLinkResponse> ValidateResetPasswordLink(ValidateResetPasswordLinkRequest validateResetPasswordLinkRequest);
    }
}
