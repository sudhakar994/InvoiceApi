using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
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

        public static string CreateSalt(int saltSize)
        {
            var buff = new byte[saltSize];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(buff);
            }
            return Convert.ToBase64String(buff);
        }
        public static string EncryptPassword(string pPassword, string pSalt)
        {
            var saltAndPwd = String.Concat(pPassword, pSalt);
            var hashedPwd = GetSwcSha1(saltAndPwd);
            return hashedPwd;
        }


        static string GetSwcSha1(string value)
        {
            var algorithm = SHA1.Create();
            var data = algorithm.ComputeHash(Encoding.UTF8.GetBytes(value));
            var sh1 = new StringBuilder();
            foreach (byte t in data)
            {
                sh1.Append(t.ToString("x2").ToUpperInvariant());
            }
            return sh1.ToString();
        }
      public static string GenerateVerificationCode()
        {
            Random generator = new Random();
            return generator.Next(0, 1000000).ToString("D6");
        }
       
    }
}
