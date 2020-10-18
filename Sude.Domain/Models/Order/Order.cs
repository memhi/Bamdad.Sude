using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Sude.Domain.Models.Common;

namespace Sude.Domain.Models.Order
{
    public class Order : BaseModel<Guid>
    {
        [Required]
        public long SumPrice { get; set; }     
        
        [Required]
        public DateTime OrderDate { get; set; }
        public string  Description { get; set; }

        public List<OrderDetail> Details { get; set; }

    }

}
