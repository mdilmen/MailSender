using Microsoft.EntityFrameworkCore;
using MailSender.Data.Entities;
using MailSender.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSender.Data
{
    public class MailSenderRepository : IMailSenderRepository
    {
        private readonly MailSenderContext _context;

        public MailSenderRepository(MailSenderContext context)
        {
            _context = context;
        }
        //public Contract GetContractById(int id)
        //{            
        //    var contract =  _context.Contracts.Where(c => c.Id == id)
        //                            .Include(c => c.SurveyUser).ThenInclude(c => c.City)
        //                            .Include(c => c.SurveyUser)                                    
        //                            .FirstOrDefault();
        //    var subUsers = _context.SubUsers.Include(s => s.City).Where(s => s.SurveyUser == contract.SurveyUser).ToList();
        //    contract.SurveyUser.SubUsers = subUsers;
        //    return contract;
        //}

        //public bool SaveAll()
        //{
        //    return _context.SaveChanges() > 0;
        //}
        //public void AddEntity(object model)
        //{
        //    _context.Add(model);
        //}

        //public List<Company> GetAllCompanies()
        //{
        //    return _context.Companies.ToList();
        //}

        //public List<AgeRange> GetAllAgeRanges()
        //{
        //    return _context.AgeRanges.ToList();
        //}

        //public Premium GetPremium(Company company, City city, AgeRange ageRange, PremiumType premiumType, PremiumLimitType premiumLimitType)
        //{
        //    Premium premium = new Premium();
        //    var list = _context.Premiums.Include(p =>p.City).Where(p => p.Company == company 
        //                                                                && p.AgeRange == ageRange && p.PremiumType == premiumType 
        //                                                                && p.PremiumLimitType == premiumLimitType);
        //    if (list.Any())
        //    {
        //        if (list.Where(l => l.City == city).Any())
        //        {
        //            premium = list.Where(l => l.City == city).FirstOrDefault();
        //        }
        //        else
        //        {
        //            // city is not listed in this premium, City shall be considered as Other
        //            City cityOther = _context.Cities.Where(ct => ct.Name.Contains("Diğer")).FirstOrDefault();
        //            premium = list.Where(l => l.City == cityOther).FirstOrDefault();
        //        }
        //    }
        //    return premium;

        //}

        //public List<City> GetAllCities()
        //{
        //    return _context.Cities.ToList();
        //}

        //public City GetCity(int id)
        //{
        //    return _context.Cities.Where(c => c.Id == id).FirstOrDefault();
        //}

        //public Company GetCompany(int id)
        //{
        //    return _context.Companies.Where(c => c.Id == id).FirstOrDefault();
        //}

        //public void UpdateEntity(object model)
        //{
        //    if (model != null)
        //    {
        //        _context.Update(model);
        //    }
        //}

        //public Contract GetContractByGuid(Guid guid)
        //{
        //    var contract = _context.Contracts.Where(c => c.Guid == guid)
        //                .Include(c => c.SurveyUser).ThenInclude(c => c.City)
        //                .Include(c => c.SurveyUser)                        
        //                .FirstOrDefault();
        //    var subUsers = _context.SubUsers.Include(s => s.City).Where(s => s.SurveyUser == contract.SurveyUser).ToList();
        //    contract.SurveyUser.SubUsers = subUsers;
        //    return contract;
        //}

        //public List<Premium> GetPremiums(Company company, City city, AgeRange ageRange)
        //{
        //    //Premium premium = new Premium();
        //    List<Premium> premiums = new List<Premium>();
        //    var list = _context.Premiums.Include(p => p.City).Where(p => p.Company == company && p.AgeRange == ageRange);
        //    if (list.Any())
        //    {
        //        if (list.Where(l => l.City == city).Any())
        //        {
        //            premiums = list.Where(l => l.City == city).ToList();
        //        }
        //        else
        //        {
        //            // city is not listed in this premium, City shall be considered as Other
        //            City cityOther = _context.Cities.Where(ct => ct.Name.Contains("Diğer")).FirstOrDefault();
        //            premiums = list.Where(l => l.City == cityOther).ToList();
        //        }
        //    }
        //    return premiums;
        //}

        //public Premium GetPremium(int id)
        //{
        //    return _context.Premiums.Where(p => p.Id == id).FirstOrDefault();
        //}

        //public SurveyUser GetSurveyUser(int id)
        //{
        //    return _context.SurveyUsers.Where(s => s.Id == id).FirstOrDefault();
        //}

        //public SubUser GetSubUser(int id)
        //{
        //    return _context.SubUsers.Where(s => s.Id == id).FirstOrDefault();
        //}

        //public Contract GetContractWithPremiumsById(int id)
        //{
        //    var contract = _context.Contracts.Where(c => c.Id == id)
        //                            .Include(c => c.SurveyUser).ThenInclude(c => c.City)
        //                            .Include(c => c.SurveyUser).ThenInclude(c => c.Premium).ThenInclude(c => c.Company)
        //                            .Include(c => c.SurveyUser).ThenInclude(c => c.Premium).ThenInclude(c => c.City)
        //                            .Include(c => c.SurveyUser)
        //                            .FirstOrDefault();
        //    var subUsers = _context.SubUsers
        //                        .Include(s => s.City)
        //                        .Include(s => s.Premium).ThenInclude(s => s.Company).Where(s => s.SurveyUser == contract.SurveyUser).ToList();
        //    // contract.SurveyUser.Premium = _context.Premiums.Include(p => p.Company).Where(p => p.Id == contract.SurveyUser.Premium.Id).FirstOrDefault();

        //    contract.SurveyUser.SubUsers = subUsers;
        //    return contract;
        //}
        public List<string> GetReportEmails()
        {
            List<string> results = new List<string>();
            var reportsEdited = new List<Report>();
            var reports = _context.Reports.ToList();
            foreach (var item in reports)
            {
                var itemEdited = reportsEdited.Where(r => r.MemberTCNO == item.MemberTCNO).FirstOrDefault();
                // new item
                if (itemEdited == null)
                {
                    reportsEdited.Add(item);
                }
                // same item exits check id, get the greater id
                else
                {
                    if (itemEdited.Id < item.Id)
                    {
                        reportsEdited.Remove(itemEdited);
                        reportsEdited.Add(item);
                    }
                }
            }
            foreach (var item in reportsEdited)
            {
                results.Add(item.MemberEmail);
            }
            return results;
        }
    }
}
