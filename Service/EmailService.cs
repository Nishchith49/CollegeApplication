using CollegeApplication.IService;
using Microsoft.Extensions.Configuration;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace CollegeApplication.Service
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Email address cannot be null or empty.", nameof(email));
            if (string.IsNullOrWhiteSpace(subject)) throw new ArgumentException("Subject cannot be null or empty.", nameof(subject));
            if (string.IsNullOrWhiteSpace(message)) throw new ArgumentException("Message cannot be null or empty.", nameof(message));

            var smtpServer = _configuration["EmailSettings:SmtpServer"] ?? throw new InvalidOperationException("SMTP server is not configured.");
            var port = int.Parse(_configuration["EmailSettings:Port"] ?? throw new InvalidOperationException("SMTP port is not configured."));
            var username = _configuration["EmailSettings:UserName"] ?? throw new InvalidOperationException("SMTP username is not configured.");
            var password = _configuration["EmailSettings:Password"] ?? throw new InvalidOperationException("SMTP password is not configured.");
            var enableSsl = bool.Parse(_configuration["EmailSettings:EnableSsl"] ?? "false");
            var senderEmail = _configuration["EmailSettings:SenderEmail"] ?? throw new InvalidOperationException("Sender email is not configured.");

            var smtpClient = new SmtpClient(smtpServer)
            {
                Port = port,
                Credentials = new NetworkCredential(username, password),
                EnableSsl = enableSsl
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(senderEmail),
                Subject = subject,
                Body = message,
                IsBodyHtml = true
            };

            mailMessage.To.Add(email);

            try
            {
                await smtpClient.SendMailAsync(mailMessage);
            }
            catch (SmtpException ex)
            {
                // Log or handle the exception as needed
                Console.WriteLine($"SMTP Error: {ex.Message}");
            }
        }
    }
}