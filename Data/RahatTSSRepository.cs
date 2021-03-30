using MailSender.Data.EntitiesRahatTSS;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSender.Data
{
    public class RahatTSSRepository : IRahatTSSRepository
    {
        private readonly RahatTSSContext _rahatTSSContext;

        public RahatTSSRepository(RahatTSSContext rahatTSSContext)
        {
            _rahatTSSContext = rahatTSSContext;
        }

        public List<Contract> GetContracts()
        {
            return _rahatTSSContext.Contracts.Include(c => c.SurveyUser).ToList();
        }

        public List<SurveyUser> GetSurveyUsers()
        {
            return _rahatTSSContext.SurveyUsers.ToList();
        }
    }
}
