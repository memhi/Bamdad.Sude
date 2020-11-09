using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sude.Dto.DtoModels.Order
{
    public class OrderDetailNewDtoModel
    {
        public string OrderId { get; set; }
        public string OrderDetailId { get; set; }
        public string ServingId { get; set; }
        //[MaxLength(5)]
        [Required(ErrorMessage = "مبلغ را وارد نمایید")]
        [Range(1,1000000000,ErrorMessage ="بازه مبلغ را درست وارد نمایید")]
        public double Price { get; set; }

        [Required(ErrorMessage = "تعداد را وارد نمایید")]
        [Range(0.0000001, 1000000000, ErrorMessage = "بازه تعداد را درست وارد نمایید")]
        public double Count { get; set; }
 
    }
}
