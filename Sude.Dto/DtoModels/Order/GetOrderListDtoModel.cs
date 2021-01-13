using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sude.Dto.DtoModels.Account;

namespace Sude.Dto.DtoModels.Order
{
    

    public class GetOrderListDtoModel
    {


        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; } 
        public bool? IsBuy { get; set; }
        public string CustomerId { get; set; }
        public string OrderNumber { get; set; }
        public string Description { get; set; }
 
        public int PageIndex { get; set; }



    }
}
