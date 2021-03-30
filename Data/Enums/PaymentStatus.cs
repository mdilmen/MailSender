using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSender.Data.Enums
{
    public enum PaymentStatus
    {
        [Display(Name = "Başarılı")]
        Success,
        [Display(Name = "Başarısız")]
        Failure,
        [Display(Name = "Bilinmiyor")]
        Unknown
    }
}
