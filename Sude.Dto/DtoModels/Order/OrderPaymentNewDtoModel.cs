using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sude.Dto.DtoModels.Account;

namespace Sude.Dto.DtoModels.Order
{
    public class OrderPaymentNewDtoModel
    {
        public string OrderPaymentId { get; set; }
        public string OrderId { get; set; }
        public string PaymentModeId { get; set; }
        public double PaymentPrice { get; set; }
 
        public string Description { get; set; }



    }
}
