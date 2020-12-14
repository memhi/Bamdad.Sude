using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Sude.Domain.Models.Common;
using Sude.Domain.Models.Order;
using Sude.Domain.Models.Work;

namespace Sude.Domain.Models.Serving
{
    public class ServingRelatedInfo : BaseModel<Guid>
    {
     
        public Guid ServingParentId { get; set; }
        public Guid ServingChildeId { get; set; }
        public double ConsumeChilde { get; set; }

      



    }

}