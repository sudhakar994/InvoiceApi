using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceApi
{
    public static class Utility
    {
        
        public static IConfiguration _config;
        public static string EnryptString(string strEncrypted)
        {
            byte[] b = System.Text.ASCIIEncoding.ASCII.GetBytes(strEncrypted);
            string encryptedPassword = Convert.ToBase64String(b);
            return encryptedPassword;
        }



        public static string DecryptString(string encrString)
        {
            byte[] b = Convert.FromBase64String(encrString);
            string decryptedPassword = System.Text.ASCIIEncoding.ASCII.GetString(b);
            return decryptedPassword;
        }

        public static string GetAppSettings(string key)
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            return config["AppSettings:" + key] != null ? config["AppSettings:" + key] : string.Empty;
        }

        public static string GetConnectionString(string key)
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            return config["ConnectionString:" + key] != null ? config["ConnectionString:" + key] : string.Empty;
        }


    }
}
