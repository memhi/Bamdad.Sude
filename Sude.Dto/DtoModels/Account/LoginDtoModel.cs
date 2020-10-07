using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sude.Dto.DtoModels
{
    public class LoginDtoModel
    {
        [Required]
        [MaxLength(100)]
        [DisplayName("نام کاربر")]
        public string UserName { get; set; }
        [Required]
        [MaxLength(100)]        
        [DataType(DataType.Password)]
        [DisplayName("رمز عبور")]
        public string Password { get; set; }

        [DisplayName("مرا بخاطر بسپار")]
        public bool RememberMe { get; set; }


    }
}
