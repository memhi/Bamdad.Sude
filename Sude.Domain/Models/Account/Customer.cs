using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sude.Domain.Models.Common;
using Sude.Domain.Models.Work;

namespace Sude.Domain.Models.Account
{
    public class CustomerInfo : BaseModel
    {

        public bool IsActive { get; set; }
        public string Title { get; set; }  
        public string NationalCode { get; set; } 
        public string Phone { get; set; }

        public Guid WorkId { get; set; }

        public virtual WorkInfo Work { get; set; }

        public Guid? AddressId { get; set; }
        public virtual AddressInfo Address { get; set; }
      

    }
}
