using Microsoft.Extensions.Logging;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public class NotificationService
    {
        private readonly IUserWatchListService userWatchListService;

        private readonly ILogger<NotificationService> logger;
        public NotificationService(IUserWatchListService userWatchListService, ILogger<NotificationService> logger)
        {
            this.userWatchListService = userWatchListService;
            this.logger = logger;
        }

        public void SendEmail()
        {

        }
    }
}
