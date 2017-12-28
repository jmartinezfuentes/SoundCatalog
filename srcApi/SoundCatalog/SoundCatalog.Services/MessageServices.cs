using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using SoundCatalog.Configuration;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace SoundCatalog.Services
{
    public class MessageServices :IMessageServices
    {
        private readonly EmailOptions _emailConfiguration;

        public MessageServices(IOptions<EmailOptions> emailOptions)
        {
            this._emailConfiguration = emailOptions.Value;
        }

        public Task SendEmailAsync(string email, string subject, string message)
        {
            var client = new SendGridClient(this._emailConfiguration.SendGridApiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress(this._emailConfiguration.CompanyEmail),
                Subject = subject, 
                HtmlContent = message
            };
            msg.AddTo(new EmailAddress(email));
            return client.SendEmailAsync(msg);
            //SmtpClient client = new SmtpClient(_emailConfiguration.Server, _emailConfiguration.Port)
            //{
            //    UseDefaultCredentials = false,
            //    Credentials = new NetworkCredential(_emailConfiguration.User, _emailConfiguration.Password)
            //};

            //MailMessage mailMessage = new MailMessage
            //{
            //    From = new MailAddress(_emailConfiguration.FromEmail)
            //};
            //mailMessage.To.Add(email);
            //mailMessage.Body = message;
            //mailMessage.Subject = subject;
            //return client.SendMailAsync(mailMessage);
        }
    }
}
