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
    public class InventoryTypeInfo : BaseModel<Guid>
    {
       
       
        public string Title { get; set; }
     
        public string Description { get; set; }


 


    }

}