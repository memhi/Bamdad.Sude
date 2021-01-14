using Sude.Application.Result;
using Sude.Domain.Models.Order;
using Sude.Domain.Models.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sude.Application.Interfaces
{
    public interface IOrderService
    {
        ResultSet<IEnumerable<ReportOrderGroupInfo>> GetReportOrder(DateTime orderDateFrom, DateTime orderDateTo, Guid workId, bool? isBuy, int pageSize, int pageIndex, out int rowCount);
       // ResultSet<IEnumerable<OrderInfo>> GetSearchOrders(DateTime orderDateFrom, DateTime orderDateTo, Guid workId, bool? isBuy, int pageSize, int pageIndex, out int rowCount);
        ResultSet<IEnumerable<OrderInfo>> GetOrders();
        Task<ResultSet<IEnumerable<OrderInfo>>> GetOrdersWithDetailsAsync(DateTime orderDateFrom, DateTime orderDateTo, Guid workId, bool? isBuy);
        ResultSet<IEnumerable<OrderInfo>> GetOrders(Guid workId, int pageIndex, int pageSize, out int rowCount,
        DateTime? orderDateFrom = null, DateTime? orderDateTo = null, Guid? customerId = null,
        bool? isBuy = null, string description = null, string orderNumber = null, Guid? paymentStatusId = null);
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
        ResultSet<OrderPaymentInfo> AddOrderPayment(OrderPaymentInfo orderPayment);
        Task<ResultSet<OrderPaymentInfo>> AddOrderPaymentAsync(OrderPaymentInfo orderPayment);
        ResultSet<OrderPaymentInfo> GetOrderPaymentById(Guid orderPaymentId);

        Task<ResultSet<IEnumerable<OrderPaymentInfo>>> GetOrderPaymentByOrderIdAsync(Guid orderId);
        Task<ResultSet<OrderPaymentInfo>> GetOrderPaymentByIdAsync(Guid orderPaymentId);
        ResultSet EditOrderPayment(OrderPaymentInfo orderPayment);
        ResultSet DeleteOrderPayment(Guid orderPaymentId);
        Task<ResultSet> DeleteOrderPaymentAsync(Guid orderPaymentId);

    }
}
