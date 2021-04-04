using MailSender.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSender.Services
{
    public interface IUserFinderService
    {
        Task<List<UserNotCompletedModel>> Find();
    }

}