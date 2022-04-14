using MailKit.Net.Smtp;
using MimeKit;
using Services.Contracts;
using System;

namespace Services.EmailSenderService
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailConfiguration _emailConfig;
        public bool isSuccess { get; set; }
        public EmailSender(EmailConfiguration emailConfig)
        {
            _emailConfig = emailConfig;
        }
        public bool SendEmail(Message message)
        {
            var emailMessage = CreateEmailMessage(message);
            return Send(emailMessage);
        }

        private MimeMessage CreateEmailMessage(Message message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_emailConfig.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = message.Content };
            return emailMessage;
        }

        private bool Send(MimeMessage mailMessage)
        {
            bool isSuccess = false;

            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect(_emailConfig.SmtpServer, _emailConfig.Port, true);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate(_emailConfig.UserName, _emailConfig.Password);
                    client.Send(mailMessage);

                    isSuccess = true;
                }
                catch (Exception ex)
                {
                    isSuccess = false;

                    //TODO: log an error message or throw an exception or both. 
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }

                return isSuccess;
            }
        }
    }
}
