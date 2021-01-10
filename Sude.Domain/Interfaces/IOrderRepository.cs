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
        IEnumerable<OrderInfo> GetOrders(Guid workId, int pageIndex, int pageSize, out int rowCount,
         DateTime? orderDateFrom = null, DateTime? orderDateTo = null, Guid? customerId = null,
         bool? isBuy = null, string description = null);
        Task<int> GetSearchOrdersCountAsync(DateTime orderDateFrom, DateTime orderDateTo, Guid? workId = null);
        bool AddOrder(OrderInfo order);
        bool EditOrder(OrderInfo order);
        bool DeleteOrder(Guid orderId);

        Task<OrderInfo> GetOrderByIdAsync(Guid orderId);
        OrderInfo GetOrderById(Guid orderId);

        IEnumerable<OrderInfo> GetSearchOrders(DateTime orderDateFrom, DateTime orderDateTo,Guid workId, bool? isBuy, int pageSize, int pageIndex, out int rowCount);

        void Save();
        Task SaveAsync();
    }
}
