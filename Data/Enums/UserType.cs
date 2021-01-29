using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSender.Data.Enums
{
    public enum UserType
    {
        [Display(Name ="Eş")]
        Spouse,
        [Display(Name = "Çocuk")]
        Child
    }
}
