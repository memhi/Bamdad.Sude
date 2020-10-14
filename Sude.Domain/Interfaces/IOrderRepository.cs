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
        Task<IEnumerable<Order>> GetOrdersAsync();
        IEnumerable<Order> GetOrders();
      //  bool IsExistServing(string Title);

        bool AddOrder(Order order);
        bool EditOrder(Order order);
        bool DeleteOrder(Guid servingId);

        Task<Order> GetOrderByIdAsync(Guid orderId);
        Order GetOrderById(Guid orderId);
       

        void Save();
        Task SaveAsync();
    }
}
