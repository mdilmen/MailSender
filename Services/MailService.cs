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
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MailSender.Services
{
    public class MailService : IMailService
    {
        private readonly IMailSenderRepository _mailSenderRepository;
        private readonly ILogger<MailService> _logger;

        public MailService(IMailSenderRepository mailSenderRepository, ILogger<MailService> logger)
        {
            _mailSenderRepository = mailSenderRepository;
            _logger = logger;
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
            foreach (var item in models.Where(m => m.Email.Contains("turnavy48@gmail.com")))
                {
                i++;

                MailMessage mailMessage = new MailMessage
                {
                    From = new MailAddress("tssbilgilendirme@oyakgrupsigorta.com"),
                    Body =
                          $"<html><head></head><body>" +
                              "<div>" +
                              $"<h4>Sn. {item.Name} </h4>" +
                              "Tamamlayıcı Sağlık Sigortası Anketine katılımınız için teşekkür ederiz.<br>" +
                              "Anket sonuçlarına göre siz değerli üyelerimiz için primlerde revizeler yapılmıştır.<br>" +
                              "Avantajlı güncel primler ile poliçenizi tanzim ettirmek için aşağıdaki linki tıklayınız!<br>" +
                              "https://test.oyakgrupsigorta.com/ContractMember/" + $"{item.Guid}<br><br>" +
                              "Saygılarımızla,<br>" +
                              "OYAK Grup Sigorta ve Reasürans Brokerliği A.Ş.<br>" +
                              "<br>" +
                              "</div>" +
                          "</body></html>",
                    Subject = "Oyak Rahat TSS Ön Talep Toplama Anketi hk.",
                    IsBodyHtml = true,
                    Priority = MailPriority.High,
                    BodyEncoding = Encoding.Default
                };

                try
                {                    
                    mailMessage.To.Clear();
                    //mailMessage.To.Add(item.Email);
                    //client.Send(mailMessage);
                    mailMessage.To.Add("mustafa.dilmen@oyakyatirim.com.tr");
                    //mailMessage.To.Add("mmdilmen@gmail.com");
                    client.Send(mailMessage);
                    _logger.LogInformation("Mail Send to {address}", item.Email);
                    Console.WriteLine($"{i} Mail Send to {item.Email}" );
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
