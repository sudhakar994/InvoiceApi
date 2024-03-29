﻿using InvoiceApi.IServices;
using InvoiceApi.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceApi.Services
{
    public class EmailService : IEmailService
    {
        #region  Variable Declaration
        private const string templatePath = @"EmailTemplate/{0}";
        private readonly SMTPConfig _smtpConfig;
        #endregion

        #region Constructor
        public EmailService(IOptions<SMTPConfig> smtpConfig)
        {
            _smtpConfig = smtpConfig.Value;
        }
        #endregion       


        #region  Send Email Verification Code 
        /// <summary>
        /// Send Email Verification Code 
        /// </summary>
        /// <param name="emailValues"></param>
        /// <returns></returns>
        public async Task EmailSend(EmailValues emailValues)
        {
            EmailAttributes userEmail = new EmailAttributes();
            userEmail = PlaceHolderEmailContent(emailValues);
            userEmail.ToEmail = emailValues.Email;
            userEmail.Subject = emailValues.Subject;
            userEmail.Body = UpdatePlaceHolders(GetEmailBody(emailValues.TemplateName), userEmail.PlaceHolders);

            await SenEmailProcess(userEmail);
            //return msg; 
        }
        #endregion      

        #region Send Verfication Code to Email
        /// <summary>
        ///  SendEmail to Verification
        /// </summary>
        /// <param name="objEmail"></param>
        /// <returns></returns>
        private async Task SenEmailProcess(EmailAttributes objEmail)
        {

            MailMessage mail = new MailMessage()
            {
                Subject = objEmail.Subject,
                Body = objEmail.Body,
                From = new MailAddress(_smtpConfig.SenderAddress, _smtpConfig.SenderDisplayName),
                IsBodyHtml = _smtpConfig.IsBodyHTML,
            };

            mail.To.Add(new MailAddress(objEmail.ToEmail));

            SmtpClient smtpClient = new SmtpClient
            {
                Host = _smtpConfig.Host,
                Port = _smtpConfig.Port,
                EnableSsl = _smtpConfig.EnableSSL,
                UseDefaultCredentials = _smtpConfig.UseDefaultCredentials,
                Credentials = new NetworkCredential(_smtpConfig.UserName, _smtpConfig.Password),
                DeliveryMethod = SmtpDeliveryMethod.Network,
            };
            mail.BodyEncoding = Encoding.Default;
            await smtpClient.SendMailAsync(mail);
        }
        #endregion

        #region Mail Template and PlaceHolder Dynamic value
        /// <summary>
        /// UpdatePlaceHolders
        /// </summary>
        /// <param name="text"></param>
        /// <param name="keyValuePairs"></param>
        /// <returns></returns>
        private string UpdatePlaceHolders(string text, List<KeyValuePair<string, string>> keyValuePairs)
        {
            if (!string.IsNullOrEmpty(text) && keyValuePairs != null)
            {
                foreach (var placeholder in keyValuePairs)
                {
                    if (text.Contains(placeholder.Key))
                    {
                        text = text.Replace(placeholder.Key, placeholder.Value);
                    }
                }
            }
            return text;
        }

        /// <summary>
        /// GetEmailBody
        /// </summary>
        /// <param name="templateName"></param>
        /// <returns></returns>
        private string GetEmailBody(string templateName)
        {
            var body = File.ReadAllText(string.Format(templatePath, templateName));
            return body;
        }
        /// <summary>
        /// PlaceHolderEmailContent
        /// </summary>
        /// <param name="emailValues"></param>
        /// <returns></returns>
        private EmailAttributes PlaceHolderEmailContent(EmailValues emailValues)
        {
            EmailAttributes options = new EmailAttributes
            {
                PlaceHolders = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("{{UserName}}", emailValues.UserName),
                    new KeyValuePair<string, string>("{{VerificationCode}}",emailValues.VerificationCode),
                    new KeyValuePair<string, string>("{{PasswordResetUrl}}", emailValues.Url),
                     new KeyValuePair<string, string>("{{SupportEmail}}", emailValues.SupportEmail)
                }
            };
            return options;
        }
        #endregion
    }
}
