using FluentEmail.Core;
//using MailKit.Net.Smtp;
using MailSender.Data;
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
            SmtpClient client = new SmtpClient("mysmtpserver");
            client.UseDefaultCredentials = false;
            //client.Credentials = new NetworkCredential("tssbilgilendirme@oyakgrupsigorta.com", "12qwasZX.");
            client.Host = "srvexc.oyakyatirim.pvt";
            client.Port = 25;
            //client.EnableSsl = true;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Timeout = 10000;

            MailMessage mailMessage = new MailMessage
            {
                From = new MailAddress("tssbilgilendirme@oyakgrupsigorta.com"),
                Body = @"<html><head></head><body>
                        <div>
                        <h4> Değerli Üyemiz;</h4>
                          Tamamlayıcı Sağlık Sigortası Ön Talep Toplama Anketine katılımınız için teşekkür ederiz.<br>
                          Anket sonuçlarına göre çalışmalarımız Şubat ayı içinde tamamlanacaktır.<br>
                          En kısa sürede süreç ile ilgili sizleri tekrar bilgilendiriyor olacağız.<br><br>
                          Saygılarımızla,<br>
                          OYAK Grup Sigorta ve Reasürans Brokerliği A.Ş.<br>
                          <br>                          
                        </div>
                          </body></html>",
                Subject = "Tamamlayıcı Sağlık Sigortası Ön Talep Toplama Anketi hk.",
                IsBodyHtml = true,
                Priority = MailPriority.High,
                BodyEncoding = Encoding.Default
            };

            var reports = _mailSenderRepository.GetReportEmails();
            //mailMessage.To.Add("mmdilmen@gmail.com");
            int i = 0;
            foreach (var item in reports.Where(r => r.Contains("turansm9@gmail.com")))
            {
                i++;
                try
                {
                    mailMessage.To.Clear();
                    mailMessage.To.Add(item);
                    //client.Send(mailMessage);
                    //mailMessage.To.Add("mustafa.dilmen@oyakyatirim.com.tr");
                    client.Send(mailMessage);
                    _logger.LogInformation("Mail Send to {address}", item);
                    Console.WriteLine($"{i} Mail Send to {item}" );
                }
                catch (Exception ex)
                {
                    _logger.LogError("Mail Could not be Send to {address} {exception}", item, ex.Message);
                    Console.WriteLine($"Mail Could not be Send to {item} {ex.Message}");
                }
                Thread.Sleep(200);
            }
            return true;
        }
    }
}
