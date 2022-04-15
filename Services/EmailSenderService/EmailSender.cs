using MailKit.Net.Smtp;
using MimeKit;
using Services.Contracts;
using System;
using System.Threading.Tasks;

namespace Services.EmailSenderService
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailConfiguration _emailConfig;

        public EmailSender(EmailConfiguration emailConfig)
        {
            _emailConfig = emailConfig;
        }

        public async Task<bool> SendEmailAsync(Message message)
        {
            var emailMessage = CreateEmailMessage(message);
            return await SendAsync(emailMessage);
        }

        private MimeMessage CreateEmailMessage(Message message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_emailConfig.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = message.Content };
            return emailMessage;
        }

        private async Task<bool> SendAsync(MimeMessage mailMessage)
        {
            bool isSuccess = false;

            using (var client = new SmtpClient())
            {
                try
                {
                    await client.ConnectAsync(_emailConfig.SmtpServer, _emailConfig.Port, true);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    await client.AuthenticateAsync(_emailConfig.UserName, _emailConfig.Password);
                    await client.SendAsync(mailMessage);

                    isSuccess = true;
                }
                catch (Exception ex)
                {
                    isSuccess = false;

                    throw;
                }
                finally
                {
                    await client.DisconnectAsync(true);
                    client.Dispose();
                }

                return isSuccess;
            }
        }
    }
}
