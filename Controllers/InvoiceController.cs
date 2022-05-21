using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvoiceApi.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceApi.Controllers
{
   
    [Route("api/invoice")]
    [ApiController]
    [Produces("application/json")]
    public class InvoiceController : ControllerBase
    {
        #region Variable Declaration
        private readonly IInvoiceService _invoiceService;
        private readonly IJwtService _jwtService;
        #endregion

        public InvoiceController(IInvoiceService invoiceService, IJwtService jwtService)
        {
            _invoiceService = invoiceService;
            _jwtService = jwtService;
        }

        
    }
}