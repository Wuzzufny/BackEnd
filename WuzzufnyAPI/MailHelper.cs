using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace TeacherWebDesign
{
    public class MailHelper
    {
        private static SmtpClient _smtp;

        static void Util()
        {
            if (_smtp == null)
            {
                _smtp = new SmtpClient
                {
                    Host = ConfigurationManager.AppSettings["MailServer"],
                    Port = Convert.ToInt32(ConfigurationManager.AppSettings["SmtpPort"]),
                    EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSsl"]),
                    UseDefaultCredentials = true,
                    Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["EmailSender"], ConfigurationManager.AppSettings["EmailPassword"]),
                };
            }
        }

        public static bool MailSender(string emailTemplate, string mailFromEmail, string mailFromName, string mailToEmail, string mailToName, string mailBody, string subject, bool highPriority = false, bool IsHtml = true, AttachmentCollection AttachmentList = null)
        {
            try
            {
                MailAddress mailFrom = new MailAddress(mailFromEmail, mailFromName);

                MailAddress mailTo = new MailAddress(mailToEmail, mailToName);
                MailMessage mailMsg = new MailMessage(mailFrom, mailTo);

                if (mailFromEmail != mailToName)
                { mailMsg.Bcc.Add(mailFrom); }

                string emailTemplatesPath, templateBody;

                try
                {
                    emailTemplatesPath = ConfigurationManager.AppSettings["AppUrl"] + ConfigurationManager.AppSettings["Path_EmailTemplates"] + "/" + emailTemplate + ".html";
                    var webRequest = System.Net.WebRequest.Create(emailTemplatesPath);

                    using (var response = webRequest.GetResponse())
                    using (var content = response.GetResponseStream())
                    using (var reader = new StreamReader(content))
                    {
                        templateBody = reader.ReadToEnd();
                    }

                    if (emailTemplate != null && !string.IsNullOrEmpty(templateBody))
                    {
                        mailMsg.Body = templateBody
                            .Replace("{ServerUrl}", ConfigurationManager.AppSettings["AppUrl"])
                            .Replace("{Name}", mailToName)
                            .Replace("{Password}", mailBody)
                            .Replace("{Body}", mailBody)
                            .Replace("{ActivationCode}", mailBody)
                            .Replace("{AccountNameReported}", !string.IsNullOrEmpty(mailBody) && mailBody.Split('-').Length > 0 ? mailBody.Split('-')[0] : "")
                            .Replace("{JobTitle}", !string.IsNullOrEmpty(mailBody) && mailBody.Split('-').Length > 1 ? mailBody.Split('-')[1] : "");
                    }
                    else
                    {
                        mailMsg.Body = mailBody;
                    }
                    mailMsg.BodyEncoding = System.Text.Encoding.UTF8;
                    mailMsg.Priority = highPriority ? MailPriority.High : MailPriority.Normal;
                    mailMsg.Subject = subject;
                    mailMsg.ReplyToList.Add(mailFrom);
                    mailMsg.IsBodyHtml = IsHtml;
                    if (AttachmentList != null)
                    {
                        foreach (var item in AttachmentList)
                        { mailMsg.Attachments.Add(item); }
                    }
                }
                catch (Exception e) { }
                Util();
                if (_smtp != null)
                { _smtp.Send(mailMsg); }
                return true;
            }
            catch //(Exception e)
            { return false; }
        }
    }
}