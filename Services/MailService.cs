using FluentEmail.Core;
//using MailKit.Net.Smtp;
using MailSender.Data;
using MailSender.Data.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MailSender.Services
{
    public class MailService : IMailService
    {
        private readonly IMailSenderRepository _mailSenderRepository;
        private readonly ILogger<MailService> _logger;
        private readonly IRahatTSSRepository _rahatTSSRepository;

        public MailService(IMailSenderRepository mailSenderRepository, ILogger<MailService> logger, IRahatTSSRepository rahatTSSRepository)
        {
            _mailSenderRepository = mailSenderRepository;
            _logger = logger;
            _rahatTSSRepository = rahatTSSRepository;
            // _smtpClient = smtpClient;
        }
        public async Task<bool> Send()
        {
            SmtpClient client = new SmtpClient("mysmtpserver")
            {
                UseDefaultCredentials = false,
                //Credentials = new NetworkCredential("oyakmenkul\tssbilgilendirme", "12qwasZX."),
                Host = "srvexc.oyakyatirim.pvt",
                Port = 25,
                //EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Timeout = 10000
            };



            var reports = _mailSenderRepository.GetReportEmails();
            var contracts = _mailSenderRepository.GetAllContracts();
            var models = new List<MailingModel>();
            foreach (var report in reports)
            {
                var contract = contracts.Where(c => c.Id == report.ContractId).FirstOrDefault();
                var model = new MailingModel()
                {
                    Name = report.MemberName,
                    Email = report.MemberEmail,
                    Guid = contract.Guid
                };
                models.Add(model);
            }




            int i = 0;
            //foreach (var item in models.Where(m => m.Email.Contains("turnavy48@gmail.com")))
            //foreach (var item in models.Where(m => m.Email.Contains("mmdilmen@gmail.com")))
            foreach (var item in models.Skip(1000).Take(1))
            //foreach (var item in models.Take(500))
            //foreach (var item in models.Where(m => !m.PhoneNumber.Contains("(532) 682-6840") && !m.PhoneNumber.Contains("(506) 584-4970")).Skip(1000).Take(2))
            //foreach (var item in models.Skip(1500).Take(1000))
            //foreach (var item in models.Where(m => m.Email.Contains("orhanugurluoglu@yahoo.com")).Skip(1500).Take(1000))
            //foreach (var item in models.Where(m => m.Name.Contains("ORHAN UĞURLUOĞLU")))
            {
                i++;

                LinkedResource img = new LinkedResource(@"C:\Users\mdilmen\source\repos\MailSender\Images\tss1.jpeg", MediaTypeNames.Image.Jpeg)
                {
                    ContentId = "MyImage"
                };

                MailMessage mailMessage = new MailMessage
                {
                    From = new MailAddress("tssbilgilendirme@oyakgrupsigorta.com", "OYAK Grup Sigorta"),

                    Subject = "OYAK Rahat TSS Ön Talep Toplama Anketi hk.",
                    IsBodyHtml = true,
                    Priority = MailPriority.High,
                    BodyEncoding = Encoding.Default
                };
                var strLink = "https://rahattss.oyakgrupsigorta.com/ContractMember/" + $"{item.Guid}";
                var strAlternateView =

                          $"<html><head></head><body>" +
                              "<div>" +
                              $"<h4>Sn. {item.Name} </h4>" +
                              "Tamamlayıcı Sağlık Sigortası Anketine katılımınız için teşekkür ederiz.<br>" +
                              "Anket sonuçlarına göre siz değerli üyelerimiz için primlerde revizeler yapılmıştır.<br>" +
                              $"Avantajlı güncel primler ile poliçenizi tanzim ettirmek için <a href={strLink}>tıklayınız!</a> <br>" +
                              //"https://rahattss.oyakgrupsigorta.com/ContractMember/" + $"{item.Guid}<br><br>" +
                              "<br>" +
                              "Saygılarımızla,<br>" +
                              "OYAK Grup Sigorta ve Reasürans Brokerliği A.Ş.<br>" +
                              "<br>" +
                              "<div>" +
                                    "<img src=cid:MyImage id='img' alt='' />" +
                              "</div>" +
                              "</div>" +
                          "</body></html>";
                AlternateView av = AlternateView.CreateAlternateViewFromString(strAlternateView, null, MediaTypeNames.Text.Html);
                av.LinkedResources.Add(img);
                mailMessage.AlternateViews.Add(av);
                try
                {
                    mailMessage.To.Clear();
                    mailMessage.To.Add(item.Email);
                    client.Send(mailMessage);
                    //mailMessage.To.Add("esra.kantarceken@oyakgrupsigorta.com");
                    //mailMessage.To.Add("mustafa.dilmen@oyakyatirim.com.tr");
                    //client.Send(mailMessage);
                    _logger.LogInformation("Mail Send to {address}", item.Email);
                    Console.WriteLine($"{i} Mail Send to {item.Email}");
                }
                catch (Exception ex)
                {
                    _logger.LogError("Mail Could not be Send to {address} {exception}", item, ex.Message);
                    Console.WriteLine($"Mail Could not be Send to {item.Email} {ex.Message}");
                }
                Thread.Sleep(200);
            }
            return true;
        }

        public async Task<bool> SendToNonComing()
        {
            SmtpClient client = new SmtpClient("mysmtpserver")
            {
                UseDefaultCredentials = false,
                //Credentials = new NetworkCredential("oyakmenkul\tssbilgilendirme", "12qwasZX."),
                Host = "srvexc.oyakyatirim.pvt",
                Port = 25,
                //EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Timeout = 10000
            };


            var rahatTSSUsers = _rahatTSSRepository.GetSurveyUsers();
            List<string> rahatTSSUserTCKNs = new List<string>();
            foreach (var user in rahatTSSUsers)
            {
                rahatTSSUserTCKNs.Add(user.TCNO);
            }
            var reports = _mailSenderRepository.GetReportEmails();
            var contracts = _mailSenderRepository.GetAllContracts();
            var models = new List<MailingModel>();
            foreach (var report in reports)
            {
                if (!rahatTSSUserTCKNs.Contains(report.MemberTCNO))
                {
                    var contract = contracts.Where(c => c.Id == report.ContractId).FirstOrDefault();
                    var model = new MailingModel()
                    {
                        Name = report.MemberName,
                        Email = report.MemberEmail,
                        Guid = contract.Guid
                    };
                    models.Add(model);
                }
            }


            int i = 0;
            //foreach (var item in models.Where(m => m.Email.Contains("turnavy48@gmail.com")))
            //foreach (var item in models.Where(m => m.Email.Contains("mmdilmen@gmail.com")))
            foreach (var item in models)
            //foreach (var item in models.Take(500))
            //foreach (var item in models.Where(m => !m.PhoneNumber.Contains("(532) 682-6840") && !m.PhoneNumber.Contains("(506) 584-4970")).Skip(1000).Take(2))
            //foreach (var item in models.Skip(1500).Take(1000))
            //foreach (var item in models.Where(m => m.Email.Contains("orhanugurluoglu@yahoo.com")).Skip(1500).Take(1000))
            //foreach (var item in models.Where(m => m.Name.Contains("ORHAN UĞURLUOĞLU")))
            {
                i++;

                LinkedResource img = new LinkedResource(@"C:\Users\mdilmen\source\repos\MailSender\Images\tss1.jpeg", MediaTypeNames.Image.Jpeg)
                {
                    ContentId = "MyImage"
                };

                MailMessage mailMessage = new MailMessage
                {
                    From = new MailAddress("tssbilgilendirme@oyakgrupsigorta.com", "OYAK Grup Sigorta"),

                    Subject = "OYAK Rahat TSS Ön Talep Toplama Anketi hk.",
                    IsBodyHtml = true,
                    Priority = MailPriority.High,
                    BodyEncoding = Encoding.Default
                };
                var strLink = "https://rahattss.oyakgrupsigorta.com/ContractMember/" + $"{item.Guid}";
                var strAlternateView =

                          $"<html><head></head><body>" +
                              "<div>" +
                              $"<h4>Sn. {item.Name} </h4>" +
                              "Tamamlayıcı Sağlık Sigortası Anketine katılımınız için teşekkür ederiz.<br>" +
                              "Anket sonuçlarına göre siz değerli üyelerimiz için primlerde revizeler yapılmıştır.<br>" +
                              $"Avantajlı güncel primler ile poliçenizi tanzim ettirmek için <a href={strLink}>tıklayınız!</a> <br>" +
                              //"https://rahattss.oyakgrupsigorta.com/ContractMember/" + $"{item.Guid}<br><br>" +
                              "<br>" +
                              "Saygılarımızla,<br>" +
                              "OYAK Grup Sigorta ve Reasürans Brokerliği A.Ş.<br>" +
                              "<br>" +
                              "<div>" +
                                    "<img src=cid:MyImage id='img' alt='' />" +
                              "</div>" +
                              "</div>" +
                          "</body></html>";
                AlternateView av = AlternateView.CreateAlternateViewFromString(strAlternateView, null, MediaTypeNames.Text.Html);
                av.LinkedResources.Add(img);
                mailMessage.AlternateViews.Add(av);
                try
                {
                    mailMessage.To.Clear();
                    mailMessage.To.Add(item.Email);
                    client.Send(mailMessage);
                    //mailMessage.To.Add("esra.kantarceken@oyakgrupsigorta.com");
                    //mailMessage.To.Add("mustafa.dilmen@oyakyatirim.com.tr");
                    //client.Send(mailMessage);
                    _logger.LogInformation("Mail Send to {address}", item.Email);
                    Console.WriteLine($"{i} Mail Send to {item.Email}");
                }
                catch (Exception ex)
                {
                    _logger.LogError("Mail Could not be Send to {address} {exception}", item, ex.Message);
                    Console.WriteLine($"Mail Could not be Send to {item.Email} {ex.Message}");
                }
                Thread.Sleep(200);
            }
            return true;
        }

        public async Task<bool> SendToNotCompleted()
        {
            SmtpClient client = new SmtpClient("mysmtpserver")
            {
                UseDefaultCredentials = false,
                //Credentials = new NetworkCredential("oyakmenkul\tssbilgilendirme", "12qwasZX."),
                Host = "srvexc.oyakyatirim.pvt",
                Port = 25,
                //EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Timeout = 10000
            };


            var rahatTSSContracts = _rahatTSSRepository.GetContracts();
            var models = new List<MailingModel>();
            foreach (var contract in rahatTSSContracts.Where(c => c.IsApproved == false))
            {                    
                    var model = new MailingModel()
                    {
                        Name = contract.SurveyUser.FullName,
                        Email = contract.SurveyUser.Email,
                        Guid = contract.Guid
                    };
                    models.Add(model);
            }


            int i = 0;
            //foreach (var item in models.Where(m => m.Email.Contains("turnavy48@gmail.com")))
            //foreach (var item in models.Where(m => m.Email.Contains("mmdilmen@gmail.com")))
            foreach (var item in models)
            //foreach (var item in models.Take(500))
            //foreach (var item in models.Where(m => !m.PhoneNumber.Contains("(532) 682-6840") && !m.PhoneNumber.Contains("(506) 584-4970")).Skip(1000).Take(2))
            //foreach (var item in models.Skip(1500).Take(1000))
            //foreach (var item in models.Where(m => m.Email.Contains("orhanugurluoglu@yahoo.com")).Skip(1500).Take(1000))
            //foreach (var item in models.Where(m => m.Name.Contains("ORHAN UĞURLUOĞLU")))
            {
                i++;

                LinkedResource img = new LinkedResource(@"C:\Users\mdilmen\source\repos\MailSender\Images\tss1.jpeg", MediaTypeNames.Image.Jpeg)
                {
                    ContentId = "MyImage"
                };

                MailMessage mailMessage = new MailMessage
                {
                    From = new MailAddress("tssbilgilendirme@oyakgrupsigorta.com", "OYAK Grup Sigorta"),

                    Subject = "OYAK Rahat TSS Poliçe Onayı hk.",
                    IsBodyHtml = true,
                    Priority = MailPriority.High,
                    BodyEncoding = Encoding.Default
                };
                var strLink = "https://rahattss.oyakgrupsigorta.com/ContractDetail/" + $"{item.Guid}";
                var strAlternateView =

                          $"<html><head></head><body>" +
                              "<div>" +
                              $"<h4>Sn. {item.Name} </h4>" +
                              "OYAK Grup Sigorta Rahat TSS poliçesi için üretim ekranına giriş yaptığınızı ancak poliçenizin henüz onaylanmadığını görüyoruz.<br>" +
                              "Diğer üyelerimiz gibi poliçenizi onaylayarak Rahat TSS’nin avantajlı kullanımına dahil olabilirsiniz.<br>" +
                              $"Kaydınızı tamamlamak için <a href={strLink}>tıklayınız!</a> <br>" +
                              "<br>" +
                              "Saygılarımızla,<br>" +
                              "OYAK Grup Sigorta ve Reasürans Brokerliği A.Ş.<br>" +
                              "<br>" +
                              "<div>" +
                                    "<img src=cid:MyImage id='img' alt='' />" +
                              "</div>" +
                              "</div>" +
                          "</body></html>";
                AlternateView av = AlternateView.CreateAlternateViewFromString(strAlternateView, null, MediaTypeNames.Text.Html);
                av.LinkedResources.Add(img);
                mailMessage.AlternateViews.Add(av);
                try
                {
                    mailMessage.To.Clear();
                    mailMessage.To.Add(item.Email);
                    client.Send(mailMessage);
                    //mailMessage.To.Add("esra.kantarceken@oyakgrupsigorta.com");
                    //mailMessage.To.Add("mustafa.dilmen@oyakyatirim.com.tr");
                    //client.Send(mailMessage);
                    _logger.LogInformation("Mail Send to {address}", item.Email);
                    Console.WriteLine($"{i} Mail Send to {item.Email}");
                }
                catch (Exception ex)
                {
                    _logger.LogError("Mail Could not be Send to {address} {exception}", item, ex.Message);
                    Console.WriteLine($"Mail Could not be Send to {item.Email} {ex.Message}");
                }
                Thread.Sleep(200);
            }
            return true;
        }
    }
}
