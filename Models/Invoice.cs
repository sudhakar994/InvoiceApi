﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceApi.Models
{
    public class Invoice:InvoiceDetails
    {
        public string InvoiceNo { get; set; }
             
    }
}
