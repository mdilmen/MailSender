using FluentEmail.Core;
//using MailKit.Net.Smtp;
using MailSender.Data;
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

        public MailService(IMailSenderRepository mailSenderRepository)
        {            
            _mailSenderRepository = mailSenderRepository;
            // _smtpClient = smtpClient;
        }
        public async Task<bool> Send()
        {
            SmtpClient client = new SmtpClient("mysmtpserver");
            client.UseDefaultCredentials = false;
            // client.Credentials = new NetworkCredential("username", "password");
            client.Host = "srvexc.oyakyatirim.pvt";
            client.Port = 25;

            MailMessage mailMessage = new MailMessage
            {
                From = new MailAddress("tssbilgilendirme@oyakgrupsigorta.com"),
                Body = "body",
                Subject = "Test"
            };            
            try
            {
                var reports = _mailSenderRepository.GetReportEmails();
                foreach (var item in reports)
                {
                    mailMessage.To.Add(item);
                    client.Send(mailMessage);
                }
            }
            catch (Exception ex)
            {

                throw;
            }


            return true;
        }
    }
}
