using MailSender.Data.EntitiesRahatTSS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSender.Data
{
    public interface IRahatTSSRepository
    {
        List<SurveyUser> GetSurveyUsers();
        List<Contract> GetContracts();
    }
}
