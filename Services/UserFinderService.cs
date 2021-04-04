using ClosedXML.Excel;
using MailSender.Data;
using MailSender.Data.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSender.Services
{
    public class UserFinderService : IUserFinderService
    {
        private readonly IMailSenderRepository _mailSenderRepository;

        public UserFinderService(IMailSenderRepository mailSenderRepository)
        {
            _mailSenderRepository = mailSenderRepository;
        }
        public async Task<List<UserNotCompletedModel>> Find()
        {
            List<UserNotCompletedModel> result = new List<UserNotCompletedModel>();
            var users = _mailSenderRepository.GetAllUsers();
            var reports = _mailSenderRepository.GetReportEmails();


            foreach (var user in users)
            {
                var userFoundInReport = reports.Where(r => r.MemberTCNO == user.TCNO).FirstOrDefault();

                if (userFoundInReport is null)
                {
                    result.Add(new UserNotCompletedModel()
                    {
                        Id = user.Id,
                        Email = user.Email,
                        FullName = user.FullName,
                        Phone = user.Phone,
                        Tckn = user.TCNO                                                
                    });
                }
            }


            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Üyeler");
            var currentRow = 1;
            worksheet.Cell(currentRow, 1).Value = "Id";
            worksheet.Cell(currentRow, 2).Value = "E-Posta";
            worksheet.Cell(currentRow, 3).Value = "Ad Soyad";
            worksheet.Cell(currentRow, 4).Value = "Telefon";
            worksheet.Cell(currentRow, 5).Value = "TCKN";
            foreach (var item in result)
            {
                currentRow++;
                worksheet.Cell(currentRow, 1).Value = item.Id;
                worksheet.Cell(currentRow, 2).Value = item.Email;
                worksheet.Cell(currentRow, 3).Value = item.FullName;
                worksheet.Cell(currentRow, 4).Value = item.Phone;
                worksheet.Cell(currentRow, 5).Value = item.Tckn;
            }
            //using var stream = new MemoryStream();
            //workbook.SaveAs(stream);
            //var content = stream.ToArray();
            workbook.SaveAs(@"C:\Temp\Anket_Üyeler.xlsx");
            return result;
        }
    }
}
