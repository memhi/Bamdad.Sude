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
        ResultSet<ServingInventoryInfo> AddServingInventory(ServingInventoryInfo  servingInventory);
        ResultSet EditServingInventory(ServingInventoryInfo servingInventory);
        ResultSet DeleteServingInventory(Guid servingInventoryId);
        ResultSet<ServingInventoryInfo> GetServingInventoryById(Guid servingInventoryId);
        Task<ResultSet<IEnumerable<ServingInventoryInfo>>> GetServingInventorysAsync();
        Task<ResultSet<ServingInventoryInfo>> AddServingInventoryAsync(ServingInventoryInfo servingInventory);
        Task<ResultSet> EditServingInventoryAsync(ServingInventoryInfo servingInventory);
        Task<ResultSet> DeleteServingInventoryAsync(Guid servingInventoryId);
        Task<ResultSet<ServingInventoryInfo>> GetServingInventoryByIdAsync(Guid servingInventoryId);
    }
}
