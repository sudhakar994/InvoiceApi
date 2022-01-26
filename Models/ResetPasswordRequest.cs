﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace InvoiceApi.Models
{
    public class ResetPasswordRequest
    {
        [DataMember, Required, RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Invalid Email")]
        [StringLength(120, ErrorMessage = "Maximum 120 characters for email is allowed")]
        public string Email { get; set; }


    }
}
