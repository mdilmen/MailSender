using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSender.Http.Models
{
    public class SmsResponseModel
    {
        public bool isErrorOccured { get; set; }
        public string message { get; set; }
        public int code { get; set; }
        public string response { get; set; }
    }
}
