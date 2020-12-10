using Sude.Domain.Models.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sude.Domain.Interfaces
{
    public interface IOrderRepository
    {
        Task<IEnumerable<OrderInfo>> GetOrdersAsync();
        IEnumerable<OrderInfo> GetOrders();
        Task<IEnumerable<OrderInfo>> GetOrdersByWorkIdAsync(Guid workId);
        Task<int> GetSearchOrdersCountAsync(DateTime orderDateFrom, DateTime orderDateTo, Guid? workId = null);
        bool AddOrder(OrderInfo order);
        bool EditOrder(OrderInfo order);
        bool DeleteOrder(Guid orderId);

        Task<OrderInfo> GetOrderByIdAsync(Guid orderId);
        OrderInfo GetOrderById(Guid orderId);
       

        void Save();
        Task SaveAsync();
    }
}
