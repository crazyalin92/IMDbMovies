using MimeKit;
using Services.EmailSenderService;
using System;
using System.Collections.Generic;
using Xunit;

namespace Tests
{
    public class EmailSenderTest
    {
        [Fact]
        public void SendEmailTest_ShouldSend()
        {
            //TODO: take from appsettings
            EmailConfiguration config = new EmailConfiguration
            {
                From = "",
                UserName = "",
                Password = "",
                SmtpServer = "smtp.gmail.com",
                Port = 465
            };

            EmailSender emailSender = new EmailSender(config);
            var senderList = new List<MailboxAddress>()
            {
                new MailboxAddress("@gmail.com")
            };

            Message msg = new Message
            {
                Content = "Hello, world!",
                Subject = "Test",
                To = senderList
            };

            bool isSend = emailSender.SendEmail(msg);

            Assert.Equal(true, isSend);
        }
    }
}
