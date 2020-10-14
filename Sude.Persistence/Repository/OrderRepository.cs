﻿using System;
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

        private GenericRepository<Order> _orderRepository;

        public OrderRepository(SudeDBContext ctx)
        {
            this._orderRepository = new GenericRepository<Order>(ctx);
        }

        public async Task<IEnumerable<Order>> GetOrdersAsync()
        {
            return await _orderRepository.GetAsync();
        }
        public bool AddOrder(Order order)
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

        public bool EditOrder(Order order)
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

        public async Task<Order> GetOrderByIdAsync(Guid orderId)
        {
            return await _orderRepository.GetByIdAsync(orderId);
        }

        public void Save()
        {
            _orderRepository.Save();
        }

        public async Task SaveAsync() =>
            await _orderRepository.SaveAsync();

        public IEnumerable<Order> GetOrders()
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

        public Order GetOrderById(Guid orderId)
        {
            return _orderRepository.GetById(orderId);
        }
    }
}
