﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceApi.Models
{
    public class PasswordResetResponse:Base
    {
        public string Url { get; set; }
        public string UserId { get; set; }
    }
}
