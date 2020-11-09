using Sude.Domain.Models.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sude.Domain.Interfaces
{
    public interface IOrderDetailRepository
    {
        Task<IEnumerable<OrderDetailInfo>> GetOrderDetailsAsync(Guid orderId);
        IEnumerable<OrderDetailInfo> GetOrderDetails(Guid orderId);


        bool AddOrderDetail(OrderDetailInfo orderDetail);
        bool EditOrderDetail(OrderDetailInfo orderDetail);
        bool DeleteOrderDetail(Guid orderDetailId);

        Task<OrderDetailInfo> GetOrderDetailByIdAsync(Guid orderDetailId);
        OrderDetailInfo GetOrderDetailById(Guid orderDetailId);
       

        void Save();
        Task SaveAsync();
    }
}
