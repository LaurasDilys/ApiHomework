using MailKit.Net.Smtp;
using MimeKit;
using MailKit.Security;
using Business.Dto;
using System.Text.Json;
using Business.Interfaces;
using System;
using Api.Options;

namespace Api.LogLocations
{
    public class LogToEmail : ILogStoreLocation
    {
        private readonly MailOptions mailOptions;

        private string ToEmail { get { return "smtplabogaget@gmail.com"; } }

        private string Subject { get { return $"Recently added logs ({DateTime.Now})"; } }

        public LogToEmail(MailOptions mailOptions)
        {
            this.mailOptions = mailOptions;
        }

        public void Create(LogDtoArray request)
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(mailOptions.Mail);
            email.To.Add(MailboxAddress.Parse(ToEmail));
            email.Subject = Subject;
            var builder = new BodyBuilder();

            builder.HtmlBody = JsonSerializer.Serialize(request);

            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.Connect(mailOptions.Host, mailOptions.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(mailOptions.Mail, mailOptions.Password);
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}
