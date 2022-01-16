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
    }
}
