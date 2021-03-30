using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSender.Data.EntitiesRahatTSS
{
    public class Contract
    {
        public Contract()
        {
            IsMailSend = false;
            IsSmsSend = false;
        }
        public int Id { get; set; }
        public Guid Guid { get; set; }
        public string OrderNumber { get; set; }
        public SurveyUser SurveyUser { get; set; }
        public DateTime Date { get; set; }
        public bool IsApproved { get; set; }
        public Payment Payment { get; set; }
        public bool IsMailSend { get; set; }
        public bool IsSmsSend { get; set; }
        public bool IsKvkkGraped { get; set; }


    }
}
