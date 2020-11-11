using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sude.Domain.Interfaces;
using Sude.Domain.Models.Order;
 
using Sude.Persistence.Contexts;

namespace Sude.Persistence.Repository
{
    public class OrderRepository : IOrderRepository
    {

        private GenericRepository<OrderInfo> _orderRepository;

        public OrderRepository(SudeDBContext ctx)
        {
            this._orderRepository = new GenericRepository<OrderInfo>(ctx);
        }

        public async Task<IEnumerable<OrderInfo>> GetOrdersAsync()
        {
           
            return await _orderRepository.GetAsync(null,null,"Work,Customer");
        }
        public bool AddOrder(OrderInfo order)
        {
            try
            {
                _orderRepository.Insert(order);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool EditOrder(OrderInfo order)
        {
            try
            {
                _orderRepository.Update(order);
            }
            catch
            { 
                return false; 
            }
            return true;
        }

        public async Task<OrderInfo> GetOrderByIdAsync(Guid orderId)
        {
            return await _orderRepository.GetByIdAsync(o=>o.Id== orderId,"OrderDetail,Customer,Work");
                
        }

        public void Save()
        {
            _orderRepository.Save();
        }

        public async Task SaveAsync() =>
            await _orderRepository.SaveAsync();

        public IEnumerable<OrderInfo> GetOrders()
        {
            return _orderRepository.Get();
        }

      

        

        public bool DeleteOrder(Guid orderId)
        {
            var order = GetOrderById(orderId);
            if (order == null)
                return false;
            try
            {

                order.IsRemoved = true;
                order.RemoveDate = DateTime.Now;
                _orderRepository.Update(order);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public OrderInfo GetOrderById(Guid orderId)
        {
            return _orderRepository.GetById(o => o.Id == orderId, "OrderDetail,Customer,Work");
        }
    }
}
