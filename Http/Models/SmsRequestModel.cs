using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSender.Http.Models
{
    public class SmsRequestModel
    {
        public SmsRequestModel()
        {
            numbers = new List<string>();
        }
        public string messageText { get; set; }
        public bool isTurkish { get; set; }
        public DateTime date { get; set; }
        public List<string> numbers { get; set; }
    }
}
