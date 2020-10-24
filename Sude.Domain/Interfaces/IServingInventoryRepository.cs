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
      //  bool IsExistServing(string Title);

        bool AddServingInventory(ServingInventoryInfo ServingInventory);
        bool EditServingInventory(ServingInventoryInfo ServingInventory);
        bool DeleteServingInventory(Guid servingId);

        Task<ServingInventoryInfo> GetServingInventoryByIdAsync(Guid ServingInventoryId);
        ServingInventoryInfo GetServingInventoryById(Guid ServingInventoryId);
       

        void Save();
        Task SaveAsync();
    }
}
