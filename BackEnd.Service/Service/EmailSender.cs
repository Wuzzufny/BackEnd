using BackEnd.Service.IService;
using System;
using System.Collections.Generic;
using System.Text;
using NETCore.MailKit;
using BackEnd.Service.Models;
using MimeKit;
using System.Net.Mail;
using MailKit.Net.Smtp;
using System.Threading.Tasks;

namespace BackEnd.Service.Service
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailConfiguration _emailConfig;
        public EmailSender(EmailConfiguration emailConfig)
        {
            _emailConfig = emailConfig;
        }
        public async Task SendEmail(Message message)
        {
            var emailMessage = CreateEmailMessage(message);
            await  Send(emailMessage);
        }

        private MimeMessage CreateEmailMessage(Message message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_emailConfig.Sender));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = "<div style='background-color:cyan;color:white;padding:20px; height :100%; font-family:Arial, Helvetica;color:#555555;font-size:16px;'>"+message.Content +"</div>"};
            return emailMessage;
        }
        private async Task Send(MimeMessage mailMessage)
        {
            using var client = new MailKit.Net.Smtp.SmtpClient();
            try
            {
                await client.ConnectAsync(_emailConfig.SmtpServer, _emailConfig.Port, true);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                await client.AuthenticateAsync(_emailConfig.UserName, _emailConfig.Password);
                await client.SendAsync(mailMessage);
            }
            catch
            {
                //log an error message or throw an exception or both.
                throw;
            }
            finally
            {
                await client.DisconnectAsync(true);
                client.Dispose();
            }
        }

    }
}
