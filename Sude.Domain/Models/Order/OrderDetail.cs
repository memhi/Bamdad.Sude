using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        public double Count { get; set; }             
       
        public double Price { get; set; }



   
  
        public Guid OrderId { get; set; }
     
        public virtual OrderInfo Order { get; set; }


      
 
        public Guid? ServingId { get;  set; }
        public virtual ServingInfo Serving { get; set; }
           
        public string  Description { get; set; }


    }

}
