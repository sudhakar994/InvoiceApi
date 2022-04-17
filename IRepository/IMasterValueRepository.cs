using InvoiceApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceApi.IRepository
{
    public interface IMasterValueRepository
    {
        Task<List<DropDownValue>> GetDropDownValue(string key, string condition);
    }
}
