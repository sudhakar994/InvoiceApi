
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace InvoiceApi.Services
{
    public static class PdfService
    {
        public  static Byte[] GeneratePdf(string html)
        {
            Byte[] res = null;
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            using (MemoryStream ms = new MemoryStream())
            {
                SelectPdf.HtmlToPdf converter = new SelectPdf.HtmlToPdf(800,1032);
                SelectPdf.PdfDocument doc = converter.ConvertHtmlString(html, "https://media.istockphoto.com/id/1286680331/vector/adoption-and-community-care.webp?s=612x612&w=is&k=20&c=gZK-vVVN1PtIRE_m8DGxiI4RQl0nHQ0U-YXXAX7woGs=");
                doc.Save(ms);
                res = ms.ToArray();
            }
            return res;
        }



        
    }
}
