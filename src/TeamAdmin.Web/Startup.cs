using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TeamAdmin.Core.Caching;
using TeamAdmin.Core.Repositories;
using TeamAdmin.Lib.Caching;
using TeamAdmin.Lib.Repositories;
using TeamAdmin.Lib.Util;
using TeamAdmin.Web.Data;
using TeamAdmin.Web.Models;
using TeamAdmin.Web.Services;

namespace TeamAdmin.Web
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("settings.json", optional: true, reloadOnChange: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets();

                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(developerMode: true);
            }
            
            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
            Settings.Config = Configuration;
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddApplicationInsightsTelemetry(Configuration);

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Settings.DefaultConnectionString));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddAutoMapper();
            services.AddMemoryCache();

            services.AddRouting(options => options.LowercaseUrls = true);
            services.AddMvc();

            // Add application services.
            services.AddTransient<IEmailSender, MessageSender>();
            services.AddTransient<ISmsSender, MessageSender>();
            services.AddTransient<IClubRepository, ClubRepository>();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<ITeamRepository, TeamRepository>();
            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<IPlayerRepository, PlayerRepository>();
            services.AddScoped<IProgramRepository, ProgramRepository>();
            services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddSingleton<ICacheService, CacheService>();
            services.AddSingleton<IOpponentRepository, OpponentRepository>();
            services.AddScoped<ICompetitionsRepository, CompetitionsRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();


            //if (env.IsDevelopment())
            //{
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            //}
            //else
            //{
            //    app.UseExceptionHandler("/Home/Error");
            //}


            app.UseStaticFiles();
            
            app.UseIdentity();

            // Add external authentication middleware below. To configure them please see http://go.microsoft.com/fwlink/?LinkID=532715
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
