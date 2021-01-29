using MailSender.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSender.Data.Entities
{
    public class Premium
    {
        public int Id { get; set; }
        public City City { get; set; }
        public Company Company { get; set; }
        public AgeRange AgeRange { get; set; }
        public decimal Amount { get; set; }
        public PremiumLimitType PremiumLimitType { get; set; }
        public PremiumType PremiumType { get; set; }
    }
}
