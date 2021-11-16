using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace InvoiceApi.IServices
{
   public interface IJwtService
    {
        Guid GetUserIdFromJwt();
    }
}
