using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sude.Dto.DtoModels
{
    public class UpdateUserDtoModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        [MaxLength(100)]
        public string UserName { get; set; }
        [Required]
        [MaxLength(100)]
        public string Phone { get; set; }
        [Required]
        [MaxLength(120)]
        [EmailAddress]
        public string Email { get; set; }
        public long RolId { get; set; }
    }
}
