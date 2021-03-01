using Microsoft.EntityFrameworkCore;
using MailSender.Data.Entities;
using MailSender.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailSender.Data.Models;

namespace MailSender.Data
{
    public class MailSenderRepository : IMailSenderRepository
    {
        private readonly MailSenderContext _context;

        public MailSenderRepository(MailSenderContext context)
        {
            _context = context;
        }
        public List<Report> GetReportEmails()
        {
            //List<Dictionary<string, string>> results = new List<Dictionary<string, string>>();            
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
            return reportsEdited;
        }
        public List<Contract> GetAllContracts()
        {
            return _context.Contracts.ToList();
        }

        public List<Report> GetReportPhones()
        {
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
            return reportsEdited;
        }
    }
}
