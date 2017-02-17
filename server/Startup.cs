using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Buffers;
using System.Globalization;
using TAI.Utils;
using TAI.Utils.Auth;
using TAIServer.Entities.DataAccess;
using TAIServer.Services;
using TAIServer.Services.Interfaces;

namespace TAI
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            #region service registration

            services.AddTransient<IMembersService, MembersService>();
            services.AddTransient<ILoginService, LoginService>();
            services.AddTransient<ITaskService, TaskService>();
            services.AddTransient<ITaskGroupService, TaskGroupService>();
            services.AddTransient<IProjectService, ProjectService>();
            services.AddTransient<IReportService, ReportService>();

            #endregion service registration

            services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("TAIDatabase")));

            services.AddSession();
            // Add framework services.
            services.AddMvc(options =>
            {
                options.Filters.Add(new StatusCodeExceptionAttribute());
                options.Filters.Add(new WebServiceExceptionAttribute());
                options.OutputFormatters.Clear();

                var formatterSettings = JsonSerializerSettingsProvider.CreateSerializerSettings();
                formatterSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;

                var formatter = new JsonOutputFormatter(formatterSettings, ArrayPool<Char>.Create());
                options.OutputFormatters.Add(formatter);
            });
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins", builder =>
                {
                    builder.AllowAnyOrigin();
                    builder.AllowAnyMethod();
                    builder.AllowAnyHeader();
                });
            });
            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]
                {
                    new CultureInfo("en-US"),
                    new CultureInfo("pl-PL")
                };

                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMiddleware<AuthenticateJWTMiddleware>(new AuthenticateJWTOptions());
            app.UseCors("AllowAllOrigins");
            app.UseSession();
            app.UseDeveloperExceptionPage();
            app.UseDefaultFiles();
            app.UseMiddleware<CacheControlMiddleware>();
            app.UseMvc();
            app.UseStaticFiles();

            // Uncomment if needed
            //app.ApplicationServices.GetRequiredService<DataContext>().Seed();
        }
    }
}