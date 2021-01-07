using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sude.Mvc.UI.Admin.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "شماره موبایل را وارد نمایید")]
        public string Username { get; set; }

        [Required(ErrorMessage = "کلمه عبور را وارد نمایید")]
        public string Password { get; set; }
    }
}
