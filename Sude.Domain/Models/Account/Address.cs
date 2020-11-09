using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sude.Domain.Models.Common;

namespace Sude.Domain.Models.Account
{
    public class AddressInfo : BaseModel
    {

        public string PostalCode { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }


    }
}
