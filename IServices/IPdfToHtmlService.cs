using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceApi.IServices
{
   public interface IHtmlReaderService
    {
        string ReadHtmlFileAndConvert(string viewName, object model);
    }
}
