using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceApi.Constants
{
    public static class EmailConstant
    {
        public class EmailSubject
        {
            public const string SendVerificationcode = "Welcome to EFormsBuddy";
            public const string WelcomeSubject = "Account Verified";
        }
        public class EmailTemplate
        {
            public const string VerficationCodeTemplate = "\\SendVerificationCode.html";
            public const string WelcomeEmailTemplate = "\\WelcomeEmail.html";
        }
    }
}
