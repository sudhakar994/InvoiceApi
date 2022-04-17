using InvoiceApi.Constants;
using InvoiceApi.IRepository;
using InvoiceApi.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceApi.Controllers
{
    [Route("api/mastervalue")]
    [ApiController]
    [Produces("application/json")]
    public class MasterValueController : Controller
    {
        #region Variable Declaration

        private readonly IMasterValueRepository _masterValueRepository;

        #endregion

        #region Constructor 
        public MasterValueController(IMasterValueRepository masterValueRepository)
        {
            _masterValueRepository = masterValueRepository;
        }
        #endregion
        [HttpGet]
        [Route("getdropdown")]
        public async Task<IActionResult> GetDropDownValue(string key, string condition)
        {
            List<DropDownValue> response = new List<DropDownValue>();
            if (!string.IsNullOrWhiteSpace(key))
            {
                response = await _masterValueRepository.GetDropDownValue(key, condition);
                return Ok(response);
            }
            else
            {
                return BadRequest(Messages.BadRequestMsg);
            }
        }
    }
}
