using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sude.Domain.Interfaces;
using Sude.Domain.Models.Order;
using Sude.Domain.Models.Report;
using Sude.Persistence.Contexts;

namespace Sude.Persistence.Repository
{
    public class OrderRepository : IOrderRepository
    {

        private GenericRepository<OrderInfo> _orderRepository;
        private GenericRepository<ReportOrderGroupInfo> _orderReportRepository;

        public OrderRepository(SudeDBContext ctx)
        {
            this._orderRepository = new GenericRepository<OrderInfo>(ctx);
            this._orderReportRepository = new GenericRepository<ReportOrderGroupInfo>(ctx);

        }

        public async Task<int> GetSearchOrdersCountAsync(DateTime orderDateFrom, DateTime orderDateTo, Guid? workId = null)
        {
            if (workId == null)

                return await _orderRepository.GetAsync(o => o.OrderDate >= orderDateFrom && o.OrderDate < orderDateTo).Result.ToCount();
            else
                return await _orderRepository.GetAsync(o => o.WorkId == workId && o.OrderDate >= orderDateFrom && o.OrderDate < orderDateTo).Result.ToCount();


        }


        public async Task<IEnumerable<OrderInfo>> GetOrderWithDetailsAsync(DateTime orderDateFrom, DateTime orderDateTo, Guid workId, bool? isBuy)
        {

            return await _orderRepository.GetAsync(o => o.WorkId == workId && o.OrderDate >= orderDateFrom && o.OrderDate < orderDateTo && (o.IsBuy == isBuy || isBuy == null), o => o.OrderByDescending(or => or.RegDate),
                "Details,Customer,Work,PaymentStatus,Payments,Payments.PaymentMode,Details.Serving");


           

        }
        public IEnumerable<ReportOrderGroupInfo> GetReportOrder(DateTime orderDateFrom, DateTime orderDateTo, Guid workId, bool? isBuy, int pageSize, int pageIndex, out int rowCount)
        {

            return _orderRepository.Get(o => o.WorkId == workId && o.OrderDate >= orderDateFrom && o.OrderDate < orderDateTo && (o.IsBuy == isBuy || isBuy == null))
                 .GroupBy(og => new { og.OrderDate.Date, og.IsBuy }).Select(g => new ReportOrderGroupInfo { OrderDate = g.Key.Date, IsBuy = g.Key.IsBuy, SumPrice = g.Sum(ri => ri.SumPrice) }).OrderByDescending(o => o.OrderDate)
                .ToPaged(pageIndex, pageSize, out rowCount);

            // return _orderReportRepository.GetReport("Select WorkId,IsBuy,SumPrice,OrderDate from Orders");

        }

            public IEnumerable<OrderInfo>  GetSearchOrders(DateTime orderDateFrom, DateTime orderDateTo,Guid workId,bool? isBuy,int pageSize ,int pageIndex, out int rowCount )
        {
            //if (workId == null)

            //    return  _orderRepository.GetAsync(o => o.OrderDate >= orderDateFrom && o.OrderDate < orderDateTo).Result
            //        .GroupBy(og =>new  { og.OrderDate.Date, og.IsBuy }).Select(g => new { DateOrder = g.Key.Date, IsBuy=g.Key.IsBuy, SumPrice = g.Sum(ri => ri.SumPrice)});
            //else
            
            return _orderRepository.Get(o => o.WorkId == workId && o.OrderDate >= orderDateFrom && o.OrderDate < orderDateTo && (o.IsBuy == isBuy || isBuy == null))
                .GroupBy(og => new { og.OrderDate.Date, og.IsBuy }).Select(g => new OrderInfo { OrderDate = g.Key.Date, IsBuy = g.Key.IsBuy, SumPrice = g.Sum(ri => ri.SumPrice) }).OrderByDescending(o => o.OrderDate)
               .ToPaged(pageIndex,pageSize,out rowCount);


        }



        public  IEnumerable<OrderInfo> GetOrders(Guid workId, int pageIndex, int pageSize, out int rowCount,
          DateTime? orderDateFrom = null, DateTime? orderDateTo = null, Guid? customerId = null,
          bool? isBuy = null, string description = null, string orderNumber = null)
        {


            return _orderRepository.Get( o => o.WorkId == workId && (o.Number.Contains(orderNumber) ||  orderNumber == null) && (o.OrderDate >= orderDateFrom || orderDateFrom == null)
           && (o.OrderDate < orderDateTo || orderDateTo == null) && (o.CustomerId == customerId || customerId == null)
           && (o.IsBuy == isBuy || isBuy == null) && (o.Description.Contains(description) || string.IsNullOrEmpty(description)),
             o => o.OrderByDescending(or => or.RegDate), "Work,Customer,PaymentStatus").ToPaged(pageIndex,pageSize, out rowCount);
        }


        public async Task<IEnumerable<OrderInfo>> GetOrdersByWorkIdAsync(Guid workId)
        {

            return await _orderRepository.GetAsync(o => o.WorkId == workId, o => o.OrderByDescending(or => or.RegDate), "Work,Customer,PaymentStatus") ;
        }

        public async Task<IEnumerable<OrderInfo>> GetOrdersAsync()
        {
           
            return await _orderRepository.GetAsync(null,null, "Work,Customer,PaymentStatus");
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
            return await _orderRepository.GetByIdAsync(o=>o.Id== orderId, "Details,Customer,Work,PaymentStatus,Payments,Payments.PaymentMode,Details.Serving");
                
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
            return _orderRepository.GetById(o => o.Id == orderId, "Details,Customer,Work,Payments,Payments.PaymentMode,PaymentStatus");
        }
    }
}
