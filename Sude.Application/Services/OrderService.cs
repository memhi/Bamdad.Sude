using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sude.Application.Interfaces;
using Sude.Application.Result;
using Sude.Domain.Interfaces;
using Sude.Domain.Models.Order;
 

namespace Sude.Application.Services
{
    public class OrderService : IOrderService
    {
        private IOrderRepository _OrderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            this._OrderRepository = orderRepository;
        }

     public ResultSet< IEnumerable<OrderInfo>> GetSearchOrders(DateTime orderDateFrom, DateTime orderDateTo, Guid workId, bool? isBuy, int pageSize, int pageIndex, out int rowCount)
        {

            return new ResultSet<IEnumerable<OrderInfo>>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = _OrderRepository.GetSearchOrders(orderDateFrom,orderDateTo,workId,isBuy,pageSize,pageIndex,out rowCount)
            };

        }

        public ResultSet<IEnumerable<OrderInfo>> GetOrders()
        {
            return new ResultSet<IEnumerable<OrderInfo>>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = _OrderRepository.GetOrders()
            };
        }



        public ResultSet<OrderInfo> GetOrderById(Guid orderId)
        {
            OrderInfo Order = _OrderRepository.GetOrderById(orderId);

            if (Order == null)
                return new ResultSet<OrderInfo>()
                {
                    IsSucceed = false,
                    Message = "Order Not Found",
                    Data = null
                };

            return new ResultSet<OrderInfo>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = Order
            };
        }

        public ResultSet<OrderInfo> AddOrder(OrderInfo  order)
        {


          
 
            _OrderRepository.AddOrder(order);
            _OrderRepository.Save();
           
            return new ResultSet<OrderInfo>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = order
            };
        }

        public ResultSet EditOrder(OrderInfo order)
        {
           

            if (!_OrderRepository.EditOrder(order))
                return new ResultSet() { IsSucceed = false, Message = "Order Not Edited" };

            try
            {
                _OrderRepository.Save();
            }
            catch
            {
                return new ResultSet() { IsSucceed = false, Message = "Order Not Edited" };
            }
            return new ResultSet() { IsSucceed = true, Message = string.Empty };

        }

        public ResultSet DeleteOrder(Guid orderId)
        {

            if (!_OrderRepository.DeleteOrder(orderId))
                return new ResultSet() { IsSucceed = false, Message = "Order Not Deleted" };

            try
            {
                _OrderRepository.Save();
            }
            catch
            {
                return new ResultSet() { IsSucceed = false, Message = "Order Not Deleted" };
            }
            return new ResultSet() { IsSucceed = true, Message = string.Empty };
        }


        public async Task<ResultSet<int>> GetSearchOrdersCountAsync
            (DateTime orderDateFrom,DateTime orderDateTo, Guid? workId=null    )
        {
            var countResult = await _OrderRepository.GetSearchOrdersCountAsync(orderDateFrom, orderDateTo, workId);
            return new ResultSet<int>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = countResult

            };
        }


        public async Task<ResultSet<IEnumerable<OrderInfo>>> GetOrdersAsync()
        {
            return new ResultSet<IEnumerable<OrderInfo>>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = await _OrderRepository.GetOrdersAsync()
            };
        }


        public async Task<ResultSet<IEnumerable<OrderInfo>>> GetOrdersByWorkIdAsync(Guid workId)
        {
            return new ResultSet<IEnumerable<OrderInfo>>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = await _OrderRepository.GetOrdersByWorkIdAsync(workId)
            };
        }


        public async Task<ResultSet<OrderInfo>> AddOrderAsync(OrderInfo order)
        {
            

            _OrderRepository.AddOrder(order);

            try{await _OrderRepository.SaveAsync();}

            catch(Exception e){return new ResultSet<OrderInfo>() { IsSucceed = false, Message = e.Message };}

            return new ResultSet<OrderInfo>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = order
            };
        }

        public async Task<ResultSet> EditOrderAsync(OrderInfo order)
        {
            
            if (!_OrderRepository.EditOrder(order))
                return new ResultSet() { IsSucceed = false, Message = "Order Not Edited" };

            try
            {
                await _OrderRepository.SaveAsync();
            }
            catch(Exception e)
            {
                return new ResultSet() { IsSucceed = false, Message = e.Message };
            }
            return new ResultSet() { IsSucceed = true, Message = string.Empty };
        }

        public async Task<ResultSet> DeleteOrderAsync(Guid orderId)
        {

             
            if (!_OrderRepository.DeleteOrder(orderId))
                return new ResultSet() { IsSucceed = false, Message = "Order Not Deleted" };

            try
            {
                await _OrderRepository.SaveAsync();
            }
            catch
            {
                return new ResultSet() { IsSucceed = false, Message = "Order Not Deleted" };
            }
            return new ResultSet() { IsSucceed = true, Message = string.Empty };
        }

        public async Task<ResultSet<OrderInfo>> GetOrderByIdAsync(Guid orderId)
        {
            OrderInfo Order = await _OrderRepository.GetOrderByIdAsync(orderId);

            if (Order == null)
                return new ResultSet<OrderInfo>()
                {
                    IsSucceed = false,
                    Message = "Order Not Found",
                    Data = null
                };

            return new ResultSet<OrderInfo>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = Order
            };
        }
    }
}
