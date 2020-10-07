using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sude.Dto.DtoModels
{
    public class DetailUserDtoModel
    {
        public object Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }        
        public string UserName { get; set; }  
        public string Phone { get; set; }     
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public string Password { get; set; }
    }
}
