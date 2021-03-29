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
            
            Console.WriteLine($"SMS Operation Started @ {DateTime.Now.ToLongTimeString()}");
                //foreach (var item in models.Where(m => !m.PhoneNumber.Contains("(533) 811-6582")))
                foreach (var item in models.Skip(1000).Take(1))
                //foreach (var item in models.Take(500))
                //foreach (var item in models.Where(m => !m.PhoneNumber.Contains("(532) 682-6840") && !m.PhoneNumber.Contains("(506) 584-4970")).Skip(1000).Take(2))
                //foreach (var item in models.Where(m => m.PhoneNumber.Contains("682-6840")))
                //foreach (var item in models.Skip(1500).Take(1000))
                //foreach (var item in models.Where(m => m.PhoneNumber.Contains("(533) 811-6582")))
                {
                i++;
               
                try
                {
                    // Send to regular user
                    var smsModel = _smsClient.GenerateSmsModel(item.PhoneNumber, "https://rahattss.oyakgrupsigorta.com/ContractMember/" + item.Guid, item.Name);

                    // Send to mmd
                    //var smsModel = _smsClient.GenerateSmsModel("5327654078", "https://rahattss.oyakgrupsigorta.com/ContractMember/" + item.Guid, item.Name);

                    var response = await _smsClient.SendSms(smsModel);
                    if (!response.isErrorOccured)
                    {
                        Console.WriteLine($"{i} SMS Send to {item.PhoneNumber} , {item.Name}");
                    }
                    else
                    {
                        Console.WriteLine($"SMS Could not be Send to {item.PhoneNumber} , {item.Name} ,  {response.message}");
                    }
                    
                }
                catch (Exception ex)
                {
                    //_logger.LogError("SMS Could not be Send to {address} {exception}", item, ex.Message);
                    Console.WriteLine($"SMS Could not be Send to {item.PhoneNumber} , {item.Name} ,  {ex.Message}");
                }
                Thread.Sleep(200);
            }
            Console.WriteLine($"SMS Operation Ended @ {DateTime.Now.ToLongTimeString()}");
            return true;
        }
    }
}
