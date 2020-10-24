using Sude.Application.Result;
using Sude.Domain.Models.Serving;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sude.Application.Interfaces
{
    public interface IServingInventoryService
    {
        ResultSet<IEnumerable<ServingInventoryInfo>> GetServingInventorys();
        ResultSet<ServingInventoryInfo> AddServingInventory(ServingInventoryInfo request);
        ResultSet EditServingInventory(ServingInventoryInfo request);
        ResultSet DeleteServingInventory(Guid ServingInventoryId);
        ResultSet<ServingInventoryInfo> GetServingInventoryById(Guid ServingInventoryId);
        Task<ResultSet<IEnumerable<ServingInventoryInfo>>> GetServingInventorysAsync();
        Task<ResultSet<ServingInventoryInfo>> AddServingInventoryAsync(ServingInventoryInfo request);
        Task<ResultSet> EditServingInventoryAsync(ServingInventoryInfo request);
        Task<ResultSet> DeleteServingInventoryAsync(Guid ServingInventoryId);
        Task<ResultSet<ServingInventoryInfo>> GetServingInventoryByIdAsync(Guid ServingInventoryId);
    }
}
