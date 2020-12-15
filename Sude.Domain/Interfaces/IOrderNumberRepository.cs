using Sude.Domain.Models.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sude.Domain.Interfaces
{
    public interface IOrderNumberRepository
    {
 
         OrderNumberInfo GetOrderNumberByWorkId (Guid workId,bool isBuy);      
        bool AddOrderNumber(OrderNumberInfo orderNumber);
        bool EditOrderNumber(OrderNumberInfo orderNumber);
 

        void Save();
      
    }
}
