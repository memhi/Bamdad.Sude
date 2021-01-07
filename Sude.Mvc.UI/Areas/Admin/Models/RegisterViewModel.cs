using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sude.Mvc.UI.Admin.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "نام را وارد نمایید")]
        public string Name { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        [Required(ErrorMessage = "شماره موبایل را وارد نمایید")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "کلمه عبور را وارد نمایید")]
        public string Password { get; set; }
        [Required(ErrorMessage = "تکرار کلمه عبور را وارد نمایید")]
        public string ConfirmPassword { get; set; }

    }
}
