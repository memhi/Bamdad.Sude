using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Sude.Domain.Models.Common;
using Sude.Domain.Models.Serving;


namespace Sude.Domain.Models.Order
{
    public class OrderDetailInfo : BaseModel<Guid>
    {
        [Required]
        public double Count { get; set; }     
        
        [Required]
        public double Price { get; set; }
         
        public OrderInfo Order { get; set; }

        public ServingInfo Serving { get;  set; }
        public string  Description { get; set; }


    }

}
