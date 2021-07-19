using MailSender.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSender.Services
{
    public interface ISmsService
    {
        Task<bool> Send();
        Task<bool> SendToNonComing();
        Task<bool> SendToNotCompleted();
        Task<bool> SendToSurveyNotCompleted(List<UserNotCompletedModel> models);
        Task<bool> SendCongratSms();

    }
}
