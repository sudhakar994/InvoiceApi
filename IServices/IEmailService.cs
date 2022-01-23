using InvoiceApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceApi.IServices
{
    public interface IEmailService
    {
        Task SendVerificationCode(User user);
         Task SendEmailVerificationCode(User userModel);
        Task ForgotPasswordEmail(User userModel, string url);
        Task WelcomeEmail(User user);
    }
}
