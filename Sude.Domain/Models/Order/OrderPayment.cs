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
using Sude.Domain.Models.Type;

namespace Sude.Domain.Models.Order
{

    public class OrderPaymentInfo : BaseModel<Guid>
    {
        public double Price { get; set; }      
        public Guid OrderId { get; set; }     
        public virtual OrderInfo Order { get; set; }
        public TypeInfo PaymentMode { get; set; }
        public string  Description { get; set; }


    }

}
