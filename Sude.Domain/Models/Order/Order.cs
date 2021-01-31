using System;
using System.Collections.Generic;
 
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Sude.Domain.Models.Account;
using Sude.Domain.Models.Common;
using Sude.Domain.Models.Type;
using Sude.Domain.Models.Work;

namespace Sude.Domain.Models.Order
{
    public class OrderInfo : BaseModel<Guid>
    {
 
        public double SumPrice { get; set; }           
   
        public DateTime OrderDate { get; set; }
        public string  Description { get; set; }
        public string  Number { get; set; }
        public bool Status { get; set; }
        public bool IsBuy { get; set; }
        public Guid WorkId { get; set; }
        public virtual WorkInfo Work { get; set; }
        public Guid? CustomerId { get; set; }
        public virtual CustomerInfo Customer { get; set; }
        public Guid? PaymentStatusId { get; set; }
        public virtual TypeInfo PaymentStatus { get; set; }
        public virtual ICollection<OrderDetailInfo> Details { get; set; }
        public virtual ICollection<OrderPaymentInfo> Payments { get; set; }
 

    }

}
