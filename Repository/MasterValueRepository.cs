using InvoiceApi.IRepository;
using InvoiceApi.IServices;
using InvoiceApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceApi.Repository
{
    public class MasterValueRepository : IMasterValueRepository
    {
        #region Variable 
        private readonly ISqlService _sqlService;

        #endregion
        public MasterValueRepository(ISqlService sqlService)
        {
            _sqlService = sqlService;
        }

        public async Task<List<DropDownValue>> GetDropDownValue(string key, string condition)
        {
            List<DropDownValue> reponse = new List<DropDownValue>();

            if (!string.IsNullOrWhiteSpace(key))
            {
                reponse = await _sqlService.SPGetListExecuteQueryasync<DropDownValue>("SP_Common_DropDown", new { Key = key, Condition = condition });
            }
            return reponse;
        }
    }
}
