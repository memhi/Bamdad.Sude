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
    public class OrderDetail : BaseModel<Guid>
    {
        [Required]
        public double Count { get; set; }     
        
        [Required]
        public double Price { get; set; }
         
        public Order Order { get; set; }

        public Sude.Domain.Models.Serving.Serving Serving { get; set; }
        public string  Description { get; set; }


    }

}
