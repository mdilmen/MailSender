
using MailSender.Data.Entities;
using MailSender.Data.Enums;
using System;
using System.Collections.Generic;

namespace MailSender.Data
{
    public interface IMailSenderRepository
    {
        List<Report> GetReportEmails();
        List<Contract> GetAllContracts();
        List<Report> GetReportPhones();

        List<SurveyUser> GetAllUsers();
    }
}