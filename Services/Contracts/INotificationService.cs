using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface INotificationService
    {
        Task<bool> SendNotifications();
    }
}
