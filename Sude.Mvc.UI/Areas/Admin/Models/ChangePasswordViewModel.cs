using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sude.Mvc.UI.Admin.Models
{
    public class ChangePasswordViewModel
    {
     
        public string userId { get; set; }
        [Required(ErrorMessage = "کلمه عبور قبلی را وارد نمایید")]
        public string currentPassword { get; set; }

        [Required(ErrorMessage = "کلمه عبور را وارد نمایید")]
        public string password { get; set; }

        [Required(ErrorMessage = "تکرار کلمه عبور را وارد نمایید")]
        public string confirmPassword { get; set; }

    }
}
