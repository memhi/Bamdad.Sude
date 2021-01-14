using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sude.Application.Interfaces;
using Sude.Application.Result;
using Sude.Domain.Interfaces;
using Sude.Domain.Models.Order;
using Sude.Domain.Models.Report;

namespace Sude.Application.Services
{
    public class OrderService : IOrderService
    {
        private IOrderRepository _OrderRepository;
        private IOrderPaymentRepository _OrderPaymentRepository;

        public OrderService(IOrderRepository orderRepository, IOrderPaymentRepository orderPaymentRepository)
        {
            this._OrderRepository = orderRepository;
            this._OrderPaymentRepository = orderPaymentRepository;
        }


        #region Order

        public ResultSet<IEnumerable<ReportOrderGroupInfo>> GetReportOrder(DateTime orderDateFrom, DateTime orderDateTo, Guid workId, bool? isBuy, int pageSize, int pageIndex, out int rowCount)
        {

            return new ResultSet<IEnumerable<ReportOrderGroupInfo>>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = _OrderRepository.GetReportOrder( orderDateFrom,  orderDateTo,  workId,   isBuy,  pageSize,  pageIndex, out  rowCount)
            };

        }

        //public ResultSet< IEnumerable<OrderInfo>> GetSearchOrders(DateTime orderDateFrom, DateTime orderDateTo, Guid workId, bool? isBuy, int pageSize, int pageIndex, out int rowCount)
        //{

        //    return new ResultSet<IEnumerable<OrderInfo>>()
        //    {
        //        IsSucceed = true,
        //        Message = string.Empty,
        //        Data = _OrderRepository.GetSearchOrders(orderDateFrom,orderDateTo,workId,isBuy,pageSize,pageIndex,out rowCount)
        //    };

        //}

      public async  Task<ResultSet<IEnumerable<OrderInfo>>> GetOrdersWithDetailsAsync(DateTime orderDateFrom, DateTime orderDateTo, Guid workId, bool? isBuy)
        {
            return new ResultSet<IEnumerable<OrderInfo>>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = await _OrderRepository.GetOrderWithDetailsAsync(orderDateFrom, orderDateTo, workId, isBuy)
               
            };

        }
      public  ResultSet<IEnumerable<OrderInfo>> GetOrders(Guid workId, int pageIndex, int pageSize, out int rowCount,
        DateTime? orderDateFrom = null, DateTime? orderDateTo = null, Guid? customerId = null,
        bool? isBuy = null, string description = null,string orderNumber=null, Guid? paymentStatusId = null)
        {

           
            IEnumerable<OrderInfo> orders = _OrderRepository. GetOrders(workId, pageIndex, pageSize, out rowCount,
         orderDateFrom, orderDateTo, customerId, isBuy, description,orderNumber,paymentStatusId);
            return new ResultSet<IEnumerable<OrderInfo>>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = orders
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

        #endregion

        #region OrderPayment

        public ResultSet<OrderPaymentInfo> AddOrderPayment(OrderPaymentInfo orderPayment)
        {




            _OrderPaymentRepository.AddOrderPayment(orderPayment);
            _OrderPaymentRepository.Save();

            return new ResultSet<OrderPaymentInfo>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = orderPayment
            };
        }

        public async Task<ResultSet<OrderPaymentInfo>> AddOrderPaymentAsync(OrderPaymentInfo orderPayment)
        {




            _OrderPaymentRepository.AddOrderPayment(orderPayment);
           ;

            try { await _OrderPaymentRepository.SaveAsync(); }

            catch (Exception e) { return new ResultSet<OrderPaymentInfo>() { IsSucceed = false, Message = e.Message }; }

             
            return new ResultSet<OrderPaymentInfo>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = orderPayment
            };
        }

        public ResultSet<OrderPaymentInfo> GetOrderPaymentById(Guid orderPaymentId)
        {
            OrderPaymentInfo  orderPayment = _OrderPaymentRepository.GetOrderPaymentById(orderPaymentId);

            if (orderPayment == null)
                return new ResultSet<OrderPaymentInfo>()
                {
                    IsSucceed = false,
                    Message = "Order Not Found",
                    Data = null
                };

            return new ResultSet<OrderPaymentInfo>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = orderPayment
            };
        }

        public async Task<ResultSet<IEnumerable<OrderPaymentInfo>>> GetOrderPaymentByOrderIdAsync(Guid orderId)
        {
            IEnumerable<OrderPaymentInfo> orderPayments = await _OrderPaymentRepository.GetOrderPaymentsByOrderIdAsync(orderId);

            if (orderPayments == null || !orderPayments.Any())
                return new ResultSet<IEnumerable<OrderPaymentInfo>> ()
                {
                    IsSucceed = false,
                    Message = "OrderPayment Not Found",
                    Data = null
                };

            return new ResultSet<IEnumerable<OrderPaymentInfo>>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = orderPayments
            };
        }


        public async Task<ResultSet<OrderPaymentInfo>> GetOrderPaymentByIdAsync(Guid orderPaymentId)
        {
            OrderPaymentInfo orderPayment = await _OrderPaymentRepository.GetOrderPaymentByIdAsync(orderPaymentId);

            if (orderPayment == null)
                return new ResultSet<OrderPaymentInfo>()
                {
                    IsSucceed = false,
                    Message = "Order Not Found",
                    Data = null
                };

            return new ResultSet<OrderPaymentInfo>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = orderPayment
            };
        }


        public ResultSet EditOrderPayment(OrderPaymentInfo orderPayment)
        {


            if (!_OrderPaymentRepository.EditOrderPayment(orderPayment))
                return new ResultSet() { IsSucceed = false, Message = "Order Payment Not Edited" };

            try
            {
                _OrderPaymentRepository.Save();
            }
            catch
            {
                return new ResultSet() { IsSucceed = false, Message = "Order Payment Not Edited" };
            }
            return new ResultSet() { IsSucceed = true, Message = string.Empty };

        }

        public ResultSet DeleteOrderPayment(Guid orderPaymentId)
        {

            if (!_OrderPaymentRepository.DeleteOrderPayment(orderPaymentId))
                return new ResultSet() { IsSucceed = false, Message = "Order Payment Not Deleted" };

            try
            {
                _OrderPaymentRepository.Save();
            }
            catch
            {
                return new ResultSet() { IsSucceed = false, Message = "Order Not Deleted" };
            }
            return new ResultSet() { IsSucceed = true, Message = string.Empty };
        }
        public async Task<ResultSet> DeleteOrderPaymentAsync(Guid orderPaymentId)
        {


            if (!_OrderPaymentRepository.DeleteOrderPayment(orderPaymentId))
                return new ResultSet() { IsSucceed = false, Message = "Order Payment Not Deleted" };

            try
            {
                await _OrderPaymentRepository.SaveAsync();
            }
            catch
            {
                return new ResultSet() { IsSucceed = false, Message = "Order Payment Not Deleted" };
            }
            return new ResultSet() { IsSucceed = true, Message = string.Empty };
        }



        #endregion
    }
}
