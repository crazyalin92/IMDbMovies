using IMDbMovies;
using Microsoft.Extensions.DependencyInjection;
using Services;
using Services.Contracts;
using System;
using Xunit;

namespace Tests
{
    public class NotificationServiceTest
    {
        readonly IServiceProvider _services =
        Program.CreateHostBuilder(new string[] { }).Build().Services;

        [Fact]
        public async void NotificationServiceTest_ShouldSendMessages()
        {
            var notificationService = _services.GetRequiredService<INotificationService>();

            var isSuccess = await notificationService.SendNotifications();

            Assert.True(isSuccess);
        }
    }
}
