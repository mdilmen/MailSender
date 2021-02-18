using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSender.Data.Models
{
    public class MailingModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public Guid Guid { get; set; }
    }
}
