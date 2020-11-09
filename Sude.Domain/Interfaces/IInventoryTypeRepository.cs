using Sude.Domain.Models.Serving;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sude.Domain.Interfaces
{
    public interface IInventoryTypeRepository
    {
        IEnumerable<InventoryTypeInfo> GetInventoryTypes();
        #region Sync Methods

        InventoryTypeInfo GetInventoryTypeByTitle(string title);
        bool AddInventoryType(InventoryTypeInfo inventoryType);
        bool EditInventoryType(InventoryTypeInfo inventoryType);
        bool DeleteInventoryType(Guid inventoryTypeId);

        InventoryTypeInfo GetInventoryTypeById(Guid inventoryTypeId);




        void Save();

        #endregion


        #region Async Methods
        Task<InventoryTypeInfo> GetInventoryTypeByIdAsync(Guid inventoryTypeId);
        Task<IEnumerable<InventoryTypeInfo>> GetInventoryTypesAsync();
        Task<InventoryTypeInfo> GetInventoryTypeByTitleAsync(string Title);
        Task SaveAsync();

        #endregion


      
    }
}
