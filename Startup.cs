using System;
using Hangfire;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Emailer.Startup))]

namespace Emailer
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            // Hangfire Configuration
            GlobalConfiguration.Configuration
                .UseSqlServerStorage("DefaultConnection");
            app.UseHangfireDashboard();
            // Fire-and-forget jobs are executed only once and almost immediately after creation.
            //BackgroundJob.Enqueue(() => Console.WriteLine("Fire-and-forget!"));

            // Recurring jobs fire many times on the specified CRON schedule.
            RecurringJob.AddOrUpdate(
                () => Console.WriteLine("Recurring!"),Cron.Minutely);
            app.UseHangfireServer();
        }
    }
}
