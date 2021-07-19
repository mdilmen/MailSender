using FluentEmail.Core;
using FluentEmail.Smtp;
//using MailKit.Net.Smtp;
using MailSender.Data;
using MailSender.Http;
using MailSender.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
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
                //await serviceProvider.GetService<IMailService>().Send();
                //await serviceProvider.GetService<IMailService>().SendToNonComing();
                //await serviceProvider.GetService<IMailService>().SendToNotCompleted();
                //await serviceProvider.GetService<ISmsService>().Send();
                //await serviceProvider.GetService<ISmsService>().SendToNonComing();
                //await serviceProvider.GetService<ISmsService>().SendToNotCompleted();
                //var userList = await serviceProvider.GetService<IUserFinderService>().Find();
                //await serviceProvider.GetService<IMailService>().SendToSurveyNotCompleted(userList);
                //await serviceProvider.GetService<ISmsService>().SendToSurveyNotCompleted(userList);

                await serviceProvider.GetService<IMailService>().SendCongratMail();

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
            serviceCollection.AddDbContext<RahatTSSContext>(cfg =>
            {
                //cfg.UseSqlServer(_config.GetConnectionString("MailSenderConnectionString"));
                cfg.UseSqlServer("server=10.65.100.42;Database=OgsRahatTSS;User Id=sa;password=Sh99m5ayneS2003;Trusted_Connection=False;MultipleActiveResultSets=true;");

            });

            serviceCollection.AddHttpClient<SmsClient>()
                             .AddTransientHttpErrorPolicy(x => x.WaitAndRetryAsync(3, _ => TimeSpan.FromMilliseconds(300)));
            serviceCollection.AddScoped<IMailService, MailService>();
            serviceCollection.AddScoped<ISmsService, SmsService>();
            serviceCollection.AddScoped<IUserFinderService, UserFinderService>();
            serviceCollection.AddScoped<IMailSenderRepository, MailSenderRepository>();
            serviceCollection.AddScoped<IRahatTSSRepository, RahatTSSRepository>();
            serviceCollection.AddHttpContextAccessor();
            serviceCollection.AddTransient<ISeriLogService, SeriLogService>();


        }
    }
}
