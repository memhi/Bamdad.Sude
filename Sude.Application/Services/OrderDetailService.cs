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
    public class OrderDetailService : IOrderDetailService
    {
        private IOrderDetailRepository _OrderDetailRepository;

        public OrderDetailService(IOrderDetailRepository orderDetailRepository)
        {
            this._OrderDetailRepository = orderDetailRepository;
        }
        public ResultSet<IEnumerable<OrderDetailInfo>> GetOrderDetails(Guid orderId)
        {
            return new ResultSet<IEnumerable<OrderDetailInfo>>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = _OrderDetailRepository.GetOrderDetails(orderId)
            };
        }

        public async Task<OrderDetailInfo> GetOrderDetailByServingAndOrderIdAsync(Guid orderId ,Guid servingId)
        {
            return await _OrderDetailRepository.GetOrderDetailByServingAndOrderIdAsync(orderId,servingId);
        }

        public ResultSet<OrderDetailInfo> GetOrderDetailById(Guid orderDetailId)
        {
            OrderDetailInfo OrderDetail = _OrderDetailRepository.GetOrderDetailById(orderDetailId);

            if (OrderDetail == null)
                return new ResultSet<OrderDetailInfo>()
                {
                    IsSucceed = false,
                    Message = "OrderDetail Not Found",
                    Data = null
                };

            return new ResultSet<OrderDetailInfo>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = OrderDetail
            };
        }

        public ResultSet<OrderDetailInfo> AddOrderDetail(OrderDetailInfo  orderDetail)
        {


          
 
            _OrderDetailRepository.AddOrderDetail(orderDetail);
            _OrderDetailRepository.Save();
           
            return new ResultSet<OrderDetailInfo>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = orderDetail
            };
        }

        public ResultSet EditOrderDetail(OrderDetailInfo orderDetail)
        {
           

            if (!_OrderDetailRepository.EditOrderDetail(orderDetail))
                return new ResultSet() { IsSucceed = false, Message = "OrderDetail Not Edited" };

            try
            {
                _OrderDetailRepository.Save();
            }
            catch
            {
                return new ResultSet() { IsSucceed = false, Message = "OrderDetail Not Edited" };
            }
            return new ResultSet() { IsSucceed = true, Message = string.Empty };

        }

        public ResultSet DeleteOrderDetail(Guid orderDetailId)
        {

            if (!_OrderDetailRepository.DeleteOrderDetail(orderDetailId))
                return new ResultSet() { IsSucceed = false, Message = "OrderDetail Not Deleted" };

            try
            {
                _OrderDetailRepository.Save();
            }
            catch
            {
                return new ResultSet() { IsSucceed = false, Message = "OrderDetail Not Deleted" };
            }
            return new ResultSet() { IsSucceed = true, Message = string.Empty };
        }

        public async Task<ResultSet<IEnumerable<OrderDetailInfo>>> GetOrderDetailsAsync(string orderId)
        {
            Guid orderid = Guid.Parse(orderId);
            return new ResultSet<IEnumerable<OrderDetailInfo>>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = await _OrderDetailRepository.GetOrderDetailsAsync(orderid)
            };
        }

        public async Task<ResultSet<IEnumerable<OrderDetailInfo>>> GetOrderDetailsAsync(Guid orderId)
        {
            return new ResultSet<IEnumerable<OrderDetailInfo>>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = await _OrderDetailRepository.GetOrderDetailsAsync( orderId)
            };
        }

        public async Task<ResultSet<OrderDetailInfo>> AddOrderDetailAsync(OrderDetailInfo orderDetail)
        {
            

            _OrderDetailRepository.AddOrderDetail(orderDetail);

            try{await _OrderDetailRepository.SaveAsync();}

            catch(Exception e){return new ResultSet<OrderDetailInfo>() { IsSucceed = false, Message = e.Message };}

            return new ResultSet<OrderDetailInfo>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = orderDetail
            };
        }

        public async Task<ResultSet> EditOrderDetailAsync(OrderDetailInfo orderDetail)
        {
            
            if (!_OrderDetailRepository.EditOrderDetail(orderDetail))
                return new ResultSet() { IsSucceed = false, Message = "OrderDetail Not Edited" };

            try
            {
                await _OrderDetailRepository.SaveAsync();
            }
            catch(Exception e)
            {
                return new ResultSet() { IsSucceed = false, Message = e.Message };
            }
            return new ResultSet() { IsSucceed = true, Message = string.Empty };
        }

        public async Task<ResultSet> DeleteOrderDetailAsync(Guid orderDetailId)
        {

             

            if (!_OrderDetailRepository.DeleteOrderDetail(orderDetailId))
                return new ResultSet() { IsSucceed = false, Message = "OrderDetail Not Deleted" };

            try
            {
                await _OrderDetailRepository.SaveAsync();
            }
            catch
            {
                return new ResultSet() { IsSucceed = false, Message = "OrderDetail Not Deleted" };
            }
            return new ResultSet() { IsSucceed = true, Message = string.Empty };
        }

        public async Task<ResultSet<OrderDetailInfo>> GetOrderDetailByIdAsync(Guid orderDetailId)
        {
            OrderDetailInfo OrderDetail = await _OrderDetailRepository.GetOrderDetailByIdAsync(orderDetailId);

            if (OrderDetail == null)
                return new ResultSet<OrderDetailInfo>()
                {
                    IsSucceed = false,
                    Message = "OrderDetail Not Found",
                    Data = null
                };

            return new ResultSet<OrderDetailInfo>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = OrderDetail
            };
        }
    }
}
