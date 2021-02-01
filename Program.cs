using FluentEmail.Core;
using FluentEmail.Smtp;
//using MailKit.Net.Smtp;
using MailSender.Data;
using MailSender.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;
using System;
using System.Globalization;
using System.IO;
using System.Net.Mail;
using System.Threading.Tasks;

namespace MailSender
{
    class Program
    {
        private static IConfiguration _config;

        public Program(IConfiguration config)
        {
            _config = config;
        }
        static async Task Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .MinimumLevel.Override("Microsoft.NetCore", LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .MinimumLevel.Override("System.Net.Http.HttpClient", LogEventLevel.Warning)
            .WriteTo.File(new RenderedCompactJsonFormatter(), @"c:/temp/logs/MailSender.json")            
            .WriteTo.Seq("http://localhost:5341")
            .CreateLogger();

            try
            {
                var serviceCollection = new ServiceCollection();
                ConfigureServices(serviceCollection);
                Log.Information("Starting up");
                var serviceProvider = serviceCollection.BuildServiceProvider();
                await serviceProvider.GetService<IMailService>().Send();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application start-up failed");
            }
            finally
            {
                Log.CloseAndFlush();
            }

            Console.ReadKey();

        }
        private static void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddLogging(builder => builder.AddSerilog());

            _config = new ConfigurationBuilder()
                
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("config.json", true, true)
                            .Build();

            serviceCollection.AddDbContext<MailSenderContext>(cfg =>
            {
                //cfg.UseSqlServer(_config.GetConnectionString("MailSenderConnectionString"));
                cfg.UseSqlServer("server=10.65.100.42;Database=Survey;User Id=sa;password=Sh99m5ayneS2003;Trusted_Connection=False;MultipleActiveResultSets=true;");
                
            });

       
            serviceCollection.AddScoped<IMailService, MailService>();
            serviceCollection.AddScoped<IMailSenderRepository, MailSenderRepository>();


        }
    }
}
