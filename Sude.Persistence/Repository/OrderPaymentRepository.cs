using Sude.Domain.Interfaces;
using Sude.Domain.Models.Order;
using Sude.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sude.Persistence.Repository
{
    public class OrderPaymentRepository : IOrderPaymentRepository
    {

        private GenericRepository<OrderPaymentInfo> _OrderPaymentRepository;

        public OrderPaymentRepository(SudeDBContext ctx)
        {
            //this._ctx = ctx;
            this._OrderPaymentRepository = new GenericRepository<OrderPaymentInfo>(ctx);
        }

   
       

       
    

        
        public void AddOrderPayment(OrderPaymentInfo OrderPayment)
        {
            //_ctx.OrderPayments.Add(OrderPayment);
            _OrderPaymentRepository.Insert(OrderPayment);
        }

     
      
      
        public async Task<IEnumerable<OrderPaymentInfo>> GetOrderPaymentsByOrderIdAsync(Guid OrdertId)
        {

            return await _OrderPaymentRepository.GetAsync(t => t.OrderId == OrdertId,null, "PaymentMode");
        }
        public OrderPaymentInfo GetOrderPaymentById(Guid OrderPaymentId)
        {
            //return _ctx.OrderPayments.Find(OrderPaymentId);
            return _OrderPaymentRepository.GetById(o=>o.Id==OrderPaymentId,"PaymentMode");
        }
        public async  Task<OrderPaymentInfo> GetOrderPaymentByIdAsync(Guid OrderPaymentId)
        {
            //return _ctx.OrderPayments.Find(OrderPaymentId);
            return await _OrderPaymentRepository.GetByIdAsync(o => o.Id == OrderPaymentId, "PaymentMode");
        }
        public bool EditOrderPayment(OrderPaymentInfo orderPayment)
        {
            try
            {
                _OrderPaymentRepository.Update(orderPayment);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool DeleteOrderPayment(Guid orderPaymentId)
        {
            var orderPayment = GetOrderPaymentById(orderPaymentId);
            if (orderPayment == null)
                return false;
            try
            {

                orderPayment.IsRemoved = true;
                orderPayment.RemoveDate = DateTime.Now;
                _OrderPaymentRepository.Update(orderPayment);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public void Save()
        {
            //_ctx.SaveChanges();
            _OrderPaymentRepository.Save();
        }

        public async Task SaveAsync() =>
         await _OrderPaymentRepository.SaveAsync();



    }
}
