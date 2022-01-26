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
            public const string SendVerificationcodeSubject = "Welcome to EFormsBuddy";
            public const string WelcomeSubject = "Account Verified";
            public const string ResetPasswordSubject = "Reset Your Password !";
        }
        public class EmailTemplate
        {
            public const string VerficationCodeTemplate = "\\SendVerificationCode.html";
            public const string WelcomeEmailTemplate = "\\WelcomeEmail.html";
            public const string ResetPasswordTemplate = "\\ForgotPassword.html";
        }
    }
}
