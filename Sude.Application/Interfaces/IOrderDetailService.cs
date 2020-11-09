using Sude.Application.Result;
using Sude.Domain.Models.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sude.Application.Interfaces
{
    public interface IOrderDetailService
    {
        ResultSet<IEnumerable<OrderDetailInfo>> GetOrderDetails(Guid orderId);
        ResultSet<OrderDetailInfo> AddOrderDetail(OrderDetailInfo  orderDetail);
        ResultSet EditOrderDetail(OrderDetailInfo orderDetail);
        ResultSet DeleteOrderDetail(Guid orderDetailId);
        ResultSet<OrderDetailInfo> GetOrderDetailById(Guid orderDetailId);
        Task<ResultSet<IEnumerable<OrderDetailInfo>>> GetOrderDetailsAsync(Guid orderId);
        Task<ResultSet<OrderDetailInfo>> AddOrderDetailAsync(OrderDetailInfo orderDetail);
        Task<ResultSet> EditOrderDetailAsync(OrderDetailInfo orderDetail);
        Task<ResultSet> DeleteOrderDetailAsync(Guid orderDetailId);
        Task<ResultSet<OrderDetailInfo>> GetOrderDetailByIdAsync(Guid orderDetailId);
    }
}
