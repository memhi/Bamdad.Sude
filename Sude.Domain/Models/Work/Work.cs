using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Sude.Domain.Models.Common;
using Sude.Domain.Models.Serving;
using Sude.Domain.Models.Account;

namespace Sude.Domain.Models.Work
{
    public class WorkInfo : BaseModel<Guid>
    {
 
        public string Title { get; set; }       
        public string Desc { get; set; }     
        public Guid WorkTypeId { get; set; }
        public virtual WorkTypeInfo WorkType { get; set; }

        public virtual ICollection<ServingInfo> Servings { get; set; }
        public Guid? AddressId { get; set; }
        public AddressInfo Address { get; set; } 
        public virtual ICollection<WorkUserInfo>  WorkUsers { get; set; }
    }

}
