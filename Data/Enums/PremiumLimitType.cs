using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSender.Data.Enums
{
    public enum PremiumLimitType
    {
        [Display(Name = "Yatarak + 6 Adet Ayakta")]
        Altı = 6,
        [Display(Name = "Yatarak + 8 Adet Ayakta")]
        Sekiz = 8,
        [Display(Name = "Yatarak + 10 Adet Ayakta")]
        On = 10,
        [Display(Name = "Sadece Yatarak Tedaviler")]
        Yok = 0
    }
}
