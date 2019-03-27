using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Http;
using MimeKit;
using System;
using System.Threading.Tasks;
using VendingMachine.BLL.Interfaces;
using VendingMachine.Core.Models;

namespace VendingMachine.BLL.Services
{
    public class EmailService: IEmailService
    {
        private const string ConfirmUrl = "https://{0}/api/auth/confirmEmail?userId={1}&code={2}";
        private EmailOptions _options;
        private string _baseUrl;

        public EmailService(IHttpContextAccessor httpContext, EmailOptions options)
        {
            _options = options;
            _baseUrl = httpContext.HttpContext.Request.Host.Value;
        }

        public async Task SendConfirmEmailLinkAsync(Guid userId, string email, string code)
        {
            var callbackUrl = string.Format(ConfirmUrl, _baseUrl, userId, System.Net.WebUtility.UrlEncode(code));

            await SendAsync(email, "Invite to Vending Machine Platform",
                $"Please confirm your invite by <a href='{callbackUrl}'>clicking here</a>.");
        }

        public async Task SendAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress(_options.Name, _options.Adress));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(_options.Smtp, _options.Port, _options.UseSsl);
                await client.AuthenticateAsync(_options.UserName, _options.Password);
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
        }
    }
}
