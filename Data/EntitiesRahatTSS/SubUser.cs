using MailSender.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSender.Data.EntitiesRahatTSS
{
    public class SubUser
    {
        public int Id { get; set; }
        public string TCNO { get; set; }
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public City City { get; set; }
        public SurveyUser SurveyUser { get; set; }
        public UserType UserType { get; set; }
        public int SubUserOrder { get; set; }
        public WorkType WorkType { get; set; }
        public Premium Premium { get; set; }
    }
}
