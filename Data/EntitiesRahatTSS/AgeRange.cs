using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSender.Data.EntitiesRahatTSS
{
    public class AgeRange
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int From { get; set; }
        public int To { get; set; }
        public int AgeRangeOrder { get; set; }
        public int CompanyId { get; set; }
    }
}
