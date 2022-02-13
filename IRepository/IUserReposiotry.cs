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
        Task<LoginResponse> ValidateUser(LoginRequest loginRequest);
        Task<PasswordResetResponse> ResetPassword(ResetPasswordRequest resetPasswordRequest);
        Task<ValidateResetPasswordLinkResponse> ValidateResetPasswordLink(ValidateResetPasswordLinkRequest validateResetPasswordLinkRequest);
        Task<Base> UpdatePassword(UpdatePasswordRequest updatePasswordRequest);
        Task<string> ValidateVerficationCode(VerificationCodeResponse VerificationCodeResponse);
        Task<VerificationCodeResponse> ResendCode(VerificationCodeResponse VerificationCodeResponse);
        Task<VerificationCodeResponse> GetDetailsForResendCode(VerificationCodeResponse VerificationCodeResponse);
    }
}
