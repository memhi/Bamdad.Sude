using Sude.Domain.Models.Serving;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sude.Domain.Interfaces
{
    public interface IServingInventoryRepository
    {
        Task<IEnumerable<ServingInventoryInfo>> GetServingInventorysAsync();
        IEnumerable<ServingInventoryInfo> GetServingInventorys();
 

        bool AddServingInventory(ServingInventoryInfo servingInventory);
        bool EditServingInventory(ServingInventoryInfo servingInventory);
        bool DeleteServingInventory(Guid servingInventoryId);

        Task<ServingInventoryInfo> GetServingInventoryByIdAsync(Guid servingInventoryId);
        Task<ServingInventoryInfo> GetServingInventoryByServingIdAsync(Guid servingId);
        ServingInventoryInfo GetServingInventoryById(Guid servingInventoryId);
       

        void Save();
        Task SaveAsync();
    }
}
