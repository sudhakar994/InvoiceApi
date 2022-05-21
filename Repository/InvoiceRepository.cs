using InvoiceApi.IRepository;
using InvoiceApi.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceApi.Repository
{
    public class InvoiceRepository:IInvoiceRepository
    {
        private readonly ISqlService _sqlService;

        public InvoiceRepository(ISqlService sqlService)
        {
            _sqlService = sqlService;
        }
    }
}
