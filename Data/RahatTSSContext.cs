using MailSender.Data.EntitiesRahatTSS;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSender.Data
{
    public class RahatTSSContext :DbContext
    {
        public RahatTSSContext(DbContextOptions<RahatTSSContext> options) : base(options)
        {
        }
        public DbSet<SurveyUser> SurveyUsers { get; set; }
        public DbSet<Contract> Contracts { get; set; }
    }
}
