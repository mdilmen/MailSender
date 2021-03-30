using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSender.Data.EntitiesRahatTSS
{
    public class ReportSimplified
    {
        public int Id { get; set; }
        public int ContractId { get; set; }
        public DateTime CreateDate { get; set; }        

        // ****

        public string Name { get; set; }
        public string ParentMemberName { get; set; }
        public string TCNO { get; set; }
        public string BirthDate { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }        
        public string PremiumCompany { get; set; }        
        public string PremiumLimitType { get; set; }
        public string PremiumAmount { get; set; }

    }
}
