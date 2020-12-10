using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sude.Dto.DtoModels.Account;

namespace Sude.Dto.DtoModels.Order
{
    public class OrderNewDtoModel
    {
        public string OrderId { get; set; }
        public string WorkId { get; set; }
 
        public string OrderNumber { get; set; }
        public string Description { get; set; }
        public DateTime OrderDate { get; set; }
        public string CustomerId { get; set; }

        public string CustomerName { get; set; }
        public bool IsBuy { get; set; }
        public CustomerNewDtoModel Customer{ get; set; }
        public ICollection<OrderDetailDetailDtoModel> OrderDetails { get; set; }



    }
}
