using GuestBook.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuestBook
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IHostEnvironment env = null;
            var host = CreateHostBuilder(args)
            .ConfigureAppConfiguration((hostingContext, config) =>
             {
                 env = hostingContext.HostingEnvironment;
                 config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                       .AddJsonFile($"appsettings.{env.EnvironmentName}.json",
                       optional: true, reloadOnChange: true);
                 config.AddEnvironmentVariables();

             }).ConfigureLogging((hostingContext, logging) => {
                 // Requires `using Microsoft.Extensions.Logging;`
                 logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                 logging.AddConsole();
                 logging.AddDebug();
                 logging.AddEventSourceLogger();

                 //logging.AddFile();
             })
           .Build();
            using(var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<AppDbContext>();
                    DbInitializer.Initialize(context);

                }catch (Exception ex)
                {
                    var e = ex;
                }
            }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
