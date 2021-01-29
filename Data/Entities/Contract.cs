using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSender.Data.Entities
{
    public class Contract
    {
        public int Id { get; set; }                
        public Guid Guid { get; set; }
        public SurveyUser SurveyUser { get; set; }       
        public DateTime Date { get; set; }
        public bool IsApproved { get; set; }
    }
}
