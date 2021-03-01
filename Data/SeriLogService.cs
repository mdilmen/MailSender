using Microsoft.AspNetCore.Http;
using Serilog;
using System;
using System.Linq;
using System.Net;

namespace MailSender.Data
{
    public class SeriLogService : ISeriLogService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SeriLogService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Exception Error(string title, Exception ex = null, object model = null)
        {
            ex ??= new Exception(title);

            var logForContext = this.LogPrepare(model);
            logForContext.Error(ex, title);

            return ex;
        }

        public Exception Fatal(string title, Exception ex = null, object model = null)
        {
            ex ??= new Exception(title);

            var logForContext = this.LogPrepare(model);
            logForContext.Fatal(ex, title);
            return ex;
        }

        public Exception Warning(string title, Exception ex = null, object model = null)
        {
            ex ??= new Exception(title);

            var logForContext = this.LogPrepare(model);
            logForContext.Warning(ex, title);
            return ex;
        }

        private ILogger LogPrepare(object model)
        {
            var urlAddress = _httpContextAccessor.HttpContext?.Request.Path.Value;

            var logForContext =
                    Log
                        .ForContext("IpAddress", GetIpAddress)
                        .ForContext("URLAddress", urlAddress)
                        .ForContext("ViewModel", model, true);

            return logForContext;
        }


        private string GetIpAddress => this.GetRemoteIPAddress(_httpContextAccessor.HttpContext);

        private string GetRemoteIPAddress(HttpContext context, bool allowForwarded = true)
        {
            if (context == null)
                return "";

            if (allowForwarded)
            {
                string header = (context.Request.Headers["CF-Connecting-IP"].FirstOrDefault() ?? context.Request.Headers["X-Forwarded-For"].FirstOrDefault());
                if (IPAddress.TryParse(header, out IPAddress ip))
                {
                    return ip.ToString();
                }
            }
            return context.Connection.RemoteIpAddress.ToString();
        }

        public Exception Info(string title, Exception ex = null, object model = null)
        {
            ex ??= new Exception(title);
            var logForContext = this.LogPrepare(model);
            logForContext.Information(title);
            return ex;
        }
    }
}
