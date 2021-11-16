using InvoiceApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceApi.IServices
{
     public interface IUserService
    {
        void Test();
        string GenerateJwtToken(User user);
    }
}
