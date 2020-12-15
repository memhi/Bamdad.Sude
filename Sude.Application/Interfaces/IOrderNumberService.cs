using Sude.Application.Result;
using Sude.Domain.Models.Account;
using Sude.Domain.Models.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sude.Application.Interfaces
{
    public interface IOrderNumberService
    {
       ResultSet<OrderNumberInfo> GetNewOrderNumberByWorkId(Guid workId,bool isBuy);
        ResultSet<OrderNumberInfo> GenerateOrderNumberByWorkId(Guid workId, bool isBuy);




    }
}
