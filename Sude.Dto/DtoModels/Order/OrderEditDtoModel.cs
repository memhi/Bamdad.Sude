using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sude.Dto.DtoModels.Order
{
    public class OrderEditDtoModel
    {
        public string OrderId { get; set; }
        public string WorkId { get; set; }

        public string OrderNumber { get; set; }
        public string Description { get; set; }
        public DateTime OrderDate { get; set; }
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        
    }
}
