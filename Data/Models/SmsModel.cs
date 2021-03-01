using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSender.Data.Models
{
    public class SmsModel
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public Guid Guid { get; set; }
    }
}
