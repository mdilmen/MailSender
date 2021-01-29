using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSender.Data.Entities
{
    public class Report
    {
        public int Id { get; set; }
        public int ContractId { get; set; }
        public DateTime CreateDate { get; set; }
        public string PremiumCompany { get; set; }
        public string PremiumTotalAmount { get; set; }


        //Member
        public string MemberName { get; set; }
        public string MemberTCNO { get; set; }
        public string MemberBirthDate { get; set; }
        public string MemberCity { get; set; }
        public string MemberEmail { get; set; }
        public string MemberPhone { get; set; }
        public string MemberWorkType { get; set; }        
        public string MemberPremiumCompany { get; set; }
        public string MemberPremiumType { get; set; }
        public string MemberPremiumLimitType { get; set; }
        public string MemberPremiumAmount { get; set; }
        //Spouse
        public string SpouseName { get; set; }
        public string SpouseTCNO { get; set; }
        public string SpouseBirthDate { get; set; }
        public string SpouseCity { get; set; }    
        public string SpouseWorkType { get; set; }
        public string SpousePremiumCompany { get; set; }
        public string SpousePremiumType { get; set; }
        public string SpousePremiumLimitType { get; set; }
        public string SpousePremiumAmount { get; set; }

        //Child_1
        public string Child_1_Name { get; set; }
        public string Child_1_TCNO { get; set; }
        public string Child_1_BirthDate { get; set; }
        public string Child_1_City { get; set; }             
        public string Child_1_PremiumCompany { get; set; }
        public string Child_1_PremiumType { get; set; }
        public string Child_1_PremiumLimitType { get; set; }
        public string Child_1_PremiumAmount { get; set; }

        //Child_2
        public string Child_2_Name { get; set; }
        public string Child_2_TCNO { get; set; }
        public string Child_2_BirthDate { get; set; }
        public string Child_2_City { get; set; }              
        public string Child_2_PremiumCompany { get; set; }
        public string Child_2_PremiumType { get; set; }
        public string Child_2_PremiumLimitType { get; set; }
        public string Child_2_PremiumAmount { get; set; }

        //Child_3
        public string Child_3_Name { get; set; }
        public string Child_3_TCNO { get; set; }
        public string Child_3_BirthDate { get; set; }
        public string Child_3_City { get; set; }             
        public string Child_3_PremiumCompany { get; set; }
        public string Child_3_PremiumType { get; set; }
        public string Child_3_PremiumLimitType { get; set; }
        public string Child_3_PremiumAmount { get; set; }

        //Child_4
        public string Child_4_Name { get; set; }
        public string Child_4_TCNO { get; set; }
        public string Child_4_BirthDate { get; set; }
        public string Child_4_City { get; set; }              
        public string Child_4_PremiumCompany { get; set; }
        public string Child_4_PremiumType { get; set; }
        public string Child_4_PremiumLimitType { get; set; }
        public string Child_4_PremiumAmount { get; set; }

    }
}
