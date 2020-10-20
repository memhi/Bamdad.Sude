using System;
using System.Collections.Generic;
 
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Sude.Domain.Models.Common;
 

namespace Sude.Domain.Models.Order
{
    public class OrderInfo : BaseModel<Guid>
    {
 
        public long SumPrice { get; set; }     
        
   
        public DateTime OrderDate { get; set; }
        public string  Description { get; set; }

        public List<OrderDetailInfo> Details { get; set; }

    }

}
