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
    public class ServingInventoryInfo : BaseModel<Guid>
    {

        public double CurrentInventory { get; set; }

        public string Description { get; set; }

        public Guid ServingId { get; set; }
        public virtual ServingInfo Serving { get; set; }        
       
        public Guid? InventoryTypeId { get; set; }
        public virtual InventoryTypeInfo InventoryType { get; set; }
 
        public ICollection<ServingInventoryTrackingInfo> ServingInventoryTrackings { get; set; }
  


    }

}