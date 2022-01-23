using InvoiceApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceApi.IRepository
{
    public interface IUserReposiotry
    {
        Task<User> Register(User user);
        Task<string> ValidateVerficationCode(VerificationRequest verificationRequest);
        Task<string> ResendCode(VerificationRequest verificationRequest);
        Task<ResendEmail> ResendEmail(string userId);
        Task<LoginResponse> ValidateUser(LoginRequest loginRequest);
        Task<PasswordResetResponse> ResetPassword(ResetPasswordRequest resetPasswordRequest);
        Task<ValidateResetPasswordLinkResponse> ValidateResetPasswordLink(ValidateResetPasswordLinkRequest validateResetPasswordLinkRequest);
    }
}
