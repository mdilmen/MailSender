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
        private readonly IRahatTSSRepository _rahatTSSRepository;

        public SmsService( SmsClient smsClient, IMailSenderRepository mailSenderRepository, IRahatTSSRepository rahatTSSRepository)
        {
            _smsClient = smsClient;
            _mailSenderRepository = mailSenderRepository;
            _rahatTSSRepository = rahatTSSRepository;
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

        public Task<bool> SendCongratSms()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SendToNonComing()
        {
            var rahatTSSUsers = _rahatTSSRepository.GetSurveyUsers();
            List<string> rahatTSSUserTCKNs = new List<string>();
            foreach (var user in rahatTSSUsers)
            {
                rahatTSSUserTCKNs.Add(user.TCNO);
            }
            var reports = _mailSenderRepository.GetReportPhones();
            var contracts = _mailSenderRepository.GetAllContracts();
            var models = new List<SmsModel>();
            foreach (var report in reports)
            {
                if (!rahatTSSUserTCKNs.Contains(report.MemberTCNO))
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
            }

            int i = 0;

            Console.WriteLine($"SMS Operation Started @ {DateTime.Now.ToLongTimeString()}");
            //foreach (var item in models.Where(m => !m.PhoneNumber.Contains("(533) 811-6582")))
            foreach (var item in models.Where(m => m.PhoneNumber.Contains("(533) 412-6084")))
            //foreach (var item in models)
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

        public async Task<bool> SendToNotCompleted()
        {


            var rahatTSSContracts = _rahatTSSRepository.GetContracts();
            var models = new List<SmsModel>();
            foreach (var contract in rahatTSSContracts.Where(c => c.IsApproved == false))
            {
                var model = new SmsModel()
                {
                    Name = contract.SurveyUser.FullName,
                    PhoneNumber = contract.SurveyUser.Phone,
                    Guid = contract.Guid
                };
                models.Add(model);
            }

            int i = 0;

            Console.WriteLine($"SMS Operation Started @ {DateTime.Now.ToLongTimeString()}");
            //foreach (var item in models.Where(m => !m.PhoneNumber.Contains("(533) 811-6582")))
            foreach (var item in models)
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
                    var smsModel = _smsClient.GenerateSmsModel(item.PhoneNumber, "https://rahattss.oyakgrupsigorta.com/ContractDetail/" + item.Guid, item.Name);

                    // Send to mmd
                    //var smsModel = _smsClient.GenerateSmsModel("5327654078", "https://rahattss.oyakgrupsigorta.com/ContractDetail/" + item.Guid, item.Name);

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
        public async Task<bool> SendToSurveyNotCompleted(List<UserNotCompletedModel> userModels)
        {


            //var models = new List<MailingModel>();
            var models = new List<SmsModel>();
            foreach (var user in userModels)
            {
                var model = new SmsModel()
                {
                    Name = user.FullName,
                    PhoneNumber = user.Phone
                };
                models.Add(model);
            }

            int i = 0;

            Console.WriteLine($"SMS Operation Started @ {DateTime.Now.ToLongTimeString()}");
            //foreach (var item in models.Where(m => !m.PhoneNumber.Contains("(533) 811-6582")))
            foreach (var item in models)
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
                    var smsModel = _smsClient.GenerateSmsModel(item.PhoneNumber, "https://rahattss.oyakgrupsigorta.com", item.Name);

                    // Send to mmd
                    //var smsModel = _smsClient.GenerateSmsModel("5327654078", "https://rahattss.oyakgrupsigorta.com/ContractDetail/" + item.Guid, item.Name);

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
