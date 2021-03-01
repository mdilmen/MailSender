using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSender.Data
{
    public interface ISeriLogService
    {
        Exception Error(string title, Exception ex = null, object model = null);
        Exception Fatal(string title, Exception ex = null, object model = null);
        Exception Warning(string title, Exception ex = null, object model = null);
        Exception Info(string title, Exception ex = null, object model = null);
    }
}
