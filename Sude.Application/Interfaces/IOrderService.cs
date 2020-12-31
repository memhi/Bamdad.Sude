using Sude.Application.Result;
using Sude.Domain.Models.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sude.Application.Interfaces
{
    public interface IOrderService
    {
        ResultSet<IEnumerable<OrderInfo>> GetSearchOrders(DateTime orderDateFrom, DateTime orderDateTo, Guid workId, bool? isBuy, int pageSize, int pageIndex, out int rowCount);
        ResultSet<IEnumerable<OrderInfo>> GetOrders();
        Task<ResultSet<IEnumerable<OrderInfo>>> GetOrdersByWorkIdAsync(Guid workId);
        ResultSet<OrderInfo> AddOrder(OrderInfo order);
        ResultSet EditOrder(OrderInfo order);
        ResultSet DeleteOrder(Guid orderId);
        ResultSet<OrderInfo> GetOrderById(Guid orderId);
        Task<ResultSet<IEnumerable<OrderInfo>>> GetOrdersAsync();
        Task<ResultSet<OrderInfo>> AddOrderAsync(OrderInfo order);
        Task<ResultSet> EditOrderAsync(OrderInfo order);
        Task<ResultSet> DeleteOrderAsync(Guid orderId);
        Task<ResultSet<OrderInfo>> GetOrderByIdAsync(Guid orderId);
        Task<ResultSet<int>> GetSearchOrdersCountAsync(DateTime orderDateFrom, DateTime orderDateTo, Guid? workId = null);
    }
}
