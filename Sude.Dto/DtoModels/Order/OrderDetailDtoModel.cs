using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sude.Dto.DtoModels.Account;
using Sude.Dto.DtoModels.Content;

namespace Sude.Dto.DtoModels.Order
{
    public class OrderDetailDtoModel
    {
        public string OrderId { get; set; }
        public string WorkId { get; set; }
        public string WorkName { get; set; }     
        public string OrderNumber { get; set; }
        public string Description { get; set; }
        public double SumPrice { get; set; }
        public bool IsBuy { get; set; }
        public string PaymentStatusTitle { get; set; }
        public string PaymentStatusId { get; set; }
        public DateTime OrderDate { get; set; }
        public CustomerDetailDtoModel Customer { get; set; }
        public ICollection<OrderDetailDetailDtoModel> OrderDetails { get; set; }
        public ICollection<OrderPaymentDetailDtoModel> OrderPayments { get; set; }
        public ICollection<AttachmentNewDtoModel> Attachments { get; set; }

    }
}
