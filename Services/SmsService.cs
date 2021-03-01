using MailSender.Data;
using MailSender.Data.Models;
using MailSender.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MailSender.Services
{
    public class SmsService : ISmsService
    {
        private readonly SmsClient _smsClient;
        private readonly IMailSenderRepository _mailSenderRepository;

        public SmsService( SmsClient smsClient, IMailSenderRepository mailSenderRepository)
        {
            _smsClient = smsClient;
            _mailSenderRepository = mailSenderRepository;
        }
        public async Task<bool> Send()
        {
            var reports = _mailSenderRepository.GetReportPhones();
            var contracts = _mailSenderRepository.GetAllContracts();
            var models = new List<SmsModel>();
            foreach (var report in reports)
            {
                var contract = contracts.Where(c => c.Id == report.ContractId).FirstOrDefault();
                var model = new SmsModel()
                {
                    Name = report.MemberName,
                    PhoneNumber = report.MemberPhone,
                    Guid = contract.Guid
                };
                models.Add(model);
            }

            int i = 0;
            //foreach (var item in models.Where(m => m.Email.Contains("turnavy48@gmail.com")))
            foreach (var item in models.Where(m => m.PhoneNumber.Contains("(553) 876-8294")))
            {
                i++;

                try
                {
                    // Send to regular user
                    //var smsModel = _smsClient.GenerateSmsModel(item.PhoneNumber, "https://test.oyakgrupsigorta.com/" + "Policy /PolicyInfoDetail?guid=" + item.Guid, item.Name);

                    // Send to mmd
                    var smsModel = _smsClient.GenerateSmsModel("5327654078", "https://test.oyakgrupsigorta.com/" + "Policy /PolicyInfoDetail?guid=" + item.Guid, item.Name);

                    await _smsClient.SendSms(smsModel);
                    Console.WriteLine($"{i} SMS Send to {item.PhoneNumber} , {item.Name}");
                }
                catch (Exception ex)
                {
                    //_logger.LogError("SMS Could not be Send to {address} {exception}", item, ex.Message);
                    Console.WriteLine($"SMS Could not be Send to {item.PhoneNumber} , {item.Name} ,  {ex.Message}");
                }
                Thread.Sleep(200);
            }
            return true;
        }
    }
}
