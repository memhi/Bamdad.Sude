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
   public class OrderNumberRepository: IOrderNumberRepository
    {
        private GenericRepository<OrderNumberInfo> _orderNumberRepository;

        public OrderNumberRepository(SudeDBContext ctx)
        {
            this._orderNumberRepository = new GenericRepository<OrderNumberInfo>(ctx);
        }

        public OrderNumberInfo GetOrderNumberByWorkId(Guid workId, bool isBuy)
        {

            return  _orderNumberRepository.GetById(o => o.WorkId == workId && o.IsBuy== isBuy);
        }
        public bool AddOrderNumber(OrderNumberInfo  orderNumber)
        {
            try
            {
                _orderNumberRepository.Insert(orderNumber);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool EditOrderNumber(OrderNumberInfo orderNumber)
        {
            try
            {

                _orderNumberRepository.Update(orderNumber);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public void Save()
        {
            _orderNumberRepository.Save();
        }

        public async Task SaveAsync() =>
            await _orderNumberRepository.SaveAsync();
    }
}
