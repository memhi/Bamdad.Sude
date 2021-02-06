using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sude.Dto.DtoModels.Common
{
    public class AddressDtoModel
    {
        public string AddressId { get; set; }
        public string PostalCode { get; set; }
        public string Address { get; set; }  
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
    }
}
