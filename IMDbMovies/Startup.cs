using DataAccess.DataBase;
using DataAccess.Repositories;
using Hangfire;
using Hangfire.Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Services;
using Services.Contracts;
using Services.EmailSenderService;
using Services.IMDBService;
using System;

namespace IMDbMovies
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            string connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddMvc();

            var emailConfig = Configuration
                 .GetSection("EmailConfiguration")
                 .Get<EmailConfiguration>();

            services.AddSingleton(emailConfig);
            services.AddSwaggerGen();

            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection, builder => builder.MigrationsAssembly("IMDbMovies")));

            services.Configure<ImdSettings>(Configuration.GetSection("ImdbSettings"));

            services.AddScoped<IMovieService, MovieService>();
            services.AddScoped<IUserWatchListService, UserWatchListService>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IIMDbService, IMDbService>();
            services.AddScoped<IEmailSender, EmailSender>();
            services.AddScoped<INotificationService, NotificationService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
            IRecurringJobManager jobManager, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();

                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "IMDbMovies"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //Email sender scheduler
            //sends emails every sunday at 19.30
            var schedulerService = serviceProvider.GetService<INotificationService>();
            string cron = Cron.Weekly(DayOfWeek.Sunday, 19, 30);
            jobManager.AddOrUpdate("email_notifications", Job.FromExpression(() => schedulerService.SendNotifications()), cron, TimeZoneInfo.Utc);
        }
    }
}
