using Services.EmailSenderService;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IEmailSender
    {
        Task<bool> SendEmailAsync(Message message);
    }
}
