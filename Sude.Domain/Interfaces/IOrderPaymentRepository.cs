using Sude.Domain.Models.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sude.Domain.Interfaces
{
    public interface IOrderPaymentRepository
    {

      void  AddOrderPayment(OrderPaymentInfo orderPayment);
        Task<IEnumerable<OrderPaymentInfo>> GetOrderPaymentsByOrderIdAsync(Guid ordertId);
            OrderPaymentInfo GetOrderPaymentById(Guid orderPaymentId);
        Task<OrderPaymentInfo> GetOrderPaymentByIdAsync(Guid orderPaymentId);
        bool EditOrderPayment(OrderPaymentInfo orderPayment);

        bool DeleteOrderPayment(Guid orderPaymentId);
        void Save();
        Task SaveAsync();
    }
}
