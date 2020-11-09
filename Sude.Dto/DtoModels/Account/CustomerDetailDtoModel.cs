using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sude.Dto.DtoModels.Account
{
    public class CustomerDetailDtoModel
    {
        public string CustomerId { get; set; }
        public string WorkId { get; set; }
        public string Title { get; set; }

        public string NationalCode { get; set; }
        public string Phone { get; set; }
 


    }
}
