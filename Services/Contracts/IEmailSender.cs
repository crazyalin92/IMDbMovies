using Services.EmailSenderService;

namespace Services.Contracts
{
    public interface IEmailSender
    {
        bool SendEmail(Message message);
    }
}
