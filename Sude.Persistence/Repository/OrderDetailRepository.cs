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
    public class OrderDetailRepository : IOrderDetailRepository
    {

        private GenericRepository<OrderDetailInfo> _OrderDetailRepository;

        public OrderDetailRepository(SudeDBContext ctx)
        {
            this._OrderDetailRepository = new GenericRepository<OrderDetailInfo>(ctx);
        }

        public async Task<IEnumerable<OrderDetailInfo>> GetOrderDetailsAsync(Guid orderId)
        {
           
            return await _OrderDetailRepository.GetAsync(od=>od.OrderId==orderId,null,"Serving");
        }
        public bool AddOrderDetail(OrderDetailInfo orderDetail)
        {
            try
            {
                _OrderDetailRepository.Insert(orderDetail);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool EditOrderDetail(OrderDetailInfo orderDetail)
        {
            try
            {
                _OrderDetailRepository.Update(orderDetail);
            }
            catch
            { 
                return false; 
            }
            return true;
        }

        public async Task<OrderDetailInfo> GetOrderDetailByIdAsync(Guid orderDetailId)
        {
            return await _OrderDetailRepository.GetByIdAsync(od => od.Id == orderDetailId, "Serving");
        }

        public void Save()
        {
            _OrderDetailRepository.Save();
        }

        public async Task SaveAsync() =>
            await _OrderDetailRepository.SaveAsync();

        public IEnumerable<OrderDetailInfo> GetOrderDetails(Guid orderId)
        {
            return _OrderDetailRepository.Get(od=>od.OrderId==orderId, null, "Serving");
        }

      

        

        public bool DeleteOrderDetail(Guid orderDetailId)
        {
            var orderDetail = GetOrderDetailById(orderDetailId);
            if (orderDetail == null)
                return false;
            try
            {

                //orderDetail.IsRemoved = true;
               // orderDetail.RemoveDate = DateTime.Now;
                _OrderDetailRepository.Delete(orderDetail);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public OrderDetailInfo GetOrderDetailById(Guid orderDetailId)
        {
            return _OrderDetailRepository.GetById(od=>od.Id== orderDetailId,"Serving");
        }
    }
}
