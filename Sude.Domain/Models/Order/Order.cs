using System;
using System.Collections.Generic;
 
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Sude.Domain.Models.Common;
using Sude.Domain.Models.Work;

namespace Sude.Domain.Models.Order
{
    public class OrderInfo : BaseModel<Guid>
    {
 
        public long SumPrice { get; set; }     
        
   
        public DateTime OrderDate { get; set; }
        public string  Description { get; set; }

        public Guid WorkId { get; set; }
        public virtual WorkInfo Work { get; set; }

        public virtual ICollection<OrderDetailInfo> Details { get; set; }

    }

}
