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
            //.MinimumLevel.Override("Microsoft.NetCore", LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .MinimumLevel.Override("System.Net.Http.HttpClient", LogEventLevel.Warning)
            .WriteTo.File(new RenderedCompactJsonFormatter(), @"c:/temp/logs/MailSender.json")            
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
            //CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-EN");
            //var serviceCollection = new ServiceCollection();
            //ConfigureServices(serviceCollection);
            //var serviceProvider = serviceCollection.BuildServiceProvider();

            //try
            //{
            //    // this execution shall take 4-5 steps
            //    // token store is an issue
            //    // --- 
            //    // Get Token 
            //    // Get Invoice List
            //    // Loop every invoice in List 
            //    // For every item get individual invoice 
            //    // Send to repository (Save Invoices) 
            //    // Update IsNew for recorded invoices 

            //    //await serviceProvider.GetService<ITurkcellService>().Run();
            //}
            //catch (Exception generalException)
            //{
            ////    var logger = serviceProvider.GetService<ILogger<Program>>();
            // //   logger.LogError(generalException, "An Exception occured while running the Integration service");
            //}
            Console.ReadKey();

        }
        private static void ConfigureServices(IServiceCollection serviceCollection)
        {

            // config file
            _config = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("config.json", true, true)
                            .Build();

            serviceCollection.AddDbContext<MailSenderContext>(cfg =>
            {
                //cfg.UseSqlServer(_config.GetConnectionString("MailSenderConnectionString"));
                cfg.UseSqlServer("server=.\\SQLExpress;Database=Survey;Integrated Security=true;MultipleActiveResultSets=true;");
            });

            //serviceCollection.AddFluentEmail("tssbilgilendirme@oyakgrupsigorta.com")

            //                 .AddSmtpSender("localhost", 25);

            // serviceCollection.AddScoped<SmtpClient>();
            serviceCollection.AddScoped<IMailService, MailService>();
            serviceCollection.AddScoped<IMailSenderRepository, MailSenderRepository>();



            //// DbContext With Factory   
            ////Console.WriteLine(Directory.GetCurrentDirectory().ToString());
            ////Console.ReadLine();
            //serviceCollection.AddDbContextFactory<InvoiceContext>(cfg =>
            //{
            //    cfg.UseSqlServer(_config.GetConnectionString("InvoiceConnectionString"));
            //});
            //// EntegrationLog DbConnection            
            //serviceCollection.AddDbContext<EntegrationLogContext>(cfg =>
            //{
            //    cfg.UseSqlServer(_config.GetConnectionString("EntegrationLogConnectionString"));
            //});
            //// Adding loggers             
            //serviceCollection.AddSingleton(new LoggerFactory());
            //serviceCollection.AddLogging(opt => { opt.AddConsole(); opt.AddDebug(); });

            //// For Mapping Invoice Service 
            //serviceCollection.AddScoped<IInvoiceService, InvoiceService>();

            //// For CRUD operations
            //serviceCollection.AddScoped<ITurkcellService, TurkcellService>();

            //// HttpClientFactory
            //serviceCollection.AddHttpClient<AuthClient>();
            //serviceCollection.AddHttpClient<InvoiceClient>();

            //// Repository Invoice
            //serviceCollection.AddScoped<IInvoiceRepository, InvoiceRepositoryWithFactory>();
            //// Repository EntegrationLog
            //serviceCollection.AddScoped<IEntegrationLogRepository, EntegrationLogRepository>();
        }
    }
}
