﻿using System;
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
    public class ServingInfo : BaseModel<Guid>
    {
        public string Title { get; set; }
        public int Price { get; set; }
        public string Desc { get; set; }
        public Guid WorkId { get; set; }
        public virtual WorkInfo Work { get; set; }

        public bool IsActive { get; set; }

        public bool HasInventoryTracking { get; set; }
      
        public virtual ServingInventoryInfo ServingInventory { get; set; }

 




    }

}