using InvoiceApi.IServices;
using InvoiceApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceApi.Services
{
    public class EmailService : IEmailService
    {
        public Task SendVerificationCode(User user)
        {
            throw new NotImplementedException();
        }
    }
}
