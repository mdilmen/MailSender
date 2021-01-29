
using MailSender.Data.Entities;
using MailSender.Data.Enums;
using System;
using System.Collections.Generic;

namespace MailSender.Data
{
    public interface IMailSenderRepository
    {
        Contract GetContractById(int id);
        Contract GetContractWithPremiumsById(int id);
        Contract GetContractByGuid(Guid guid);
        bool SaveAll();
        void AddEntity(object model);
        List<Company> GetAllCompanies();

        List<AgeRange> GetAllAgeRanges();

        Premium GetPremium(Company company, City city, AgeRange ageRange,PremiumType premiumType, PremiumLimitType premiumLimitType);
        Premium GetPremium(int id);
        List<Premium> GetPremiums(Company company, City city, AgeRange ageRange);
        List<City> GetAllCities();
        City GetCity(int id);
        Company GetCompany(int id);

        void UpdateEntity(object model);

        SurveyUser GetSurveyUser(int id);
        SubUser GetSubUser(int id);
        List<string> GetReportEmails();
    }
}