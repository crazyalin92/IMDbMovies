using DataAccess.Repositories;
using IMDbMovies.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MimeKit;
using Services.Contracts;
using Services.EmailSenderService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class NotificationService : INotificationService
    {
        private readonly IRepository<WatchItem> userWatchlistRepo;
        private readonly IEmailSender emailSender;
        private readonly ILogger<NotificationService> logger;
        public NotificationService(IRepository<WatchItem> userWatchlistRepo,
            ILogger<NotificationService> logger, IEmailSender emailSender)
        {
            this.userWatchlistRepo = userWatchlistRepo;
            this.logger = logger;
            this.emailSender = emailSender;
        }

        public async Task<bool> SendNotifications()
        {
            bool isSuccess = true;
            try
            {
                var userIdList = (await userWatchlistRepo.GetAllAsync())
                    .Select(x => x.UserId).Distinct();

                foreach (var userId in userIdList)
                {
                    var notWatchedList = await userWatchlistRepo
                        .Get(w => w.UserId == userId && !w.IsWatched)
                        .Include(w => w.User)
                        .Include(w => w.Movie)
                        .ToListAsync();

                    if (notWatchedList.Count >= 3)
                    {
                        var content = GetMessageContent(notWatchedList);

                        var senderList = new List<MailboxAddress>()
                         {
                            new MailboxAddress(notWatchedList.First().User.Email)
                         };

                        var message = new Message
                        {
                            Content = content,
                            Subject = "Watchlist Notification",
                            To = senderList
                        };

                        await emailSender.SendEmailAsync(message);
                    }
                }
            }
            catch (Exception ex)
            {
                isSuccess = false;
                logger.LogError("Error during sending notification email", ex);
            }

            return isSuccess;
        }

        private string GetMessageContent(List<WatchItem> watchlist)
        {
            StringBuilder htmlBodyBuilder = new StringBuilder();

            foreach (var movie in watchlist)
            {
                htmlBodyBuilder.Append(
                    string.Format("<html><body><p align=\"center\">{0}</p><p align=\"center\">IMDB Rating - {1}</p><p align=\"center\"><img src =\"{2}\" width=\"500\" height=\"750\"></p>{3}",
                 movie.Movie.Title, movie.Movie.ImdbId, movie.Movie.PosterUrl, movie.Movie.WikiDescription));
            }

            return htmlBodyBuilder.ToString();
        }
    }
}
