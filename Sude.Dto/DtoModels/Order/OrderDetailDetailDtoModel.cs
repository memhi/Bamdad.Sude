using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sude.Dto.DtoModels.Order
{
    public class OrderDetailDetailDtoModel
    {
        public string OrderId { get; set; }
        public string OrderDetailId { get; set; }
        public string ServingId { get; set; }
        public string ServingTitle { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }       
        public double Count { get; set; }
 
    }
}
