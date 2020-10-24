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

        #region Sync Methods
        IEnumerable<InventoryTypeInfo> GetInventoryTypes();
        //  bool IsExistServing(string Title);
        InventoryTypeInfo GetInventoryTypeByTitle(string Title);
        bool AddInventoryType(InventoryTypeInfo InventoryType);
        bool EditInventoryType(InventoryTypeInfo InventoryType);
        bool DeleteInventoryType(Guid servingId);

        InventoryTypeInfo GetInventoryTypeById(Guid InventoryTypeId);




        void Save();

        #endregion


        #region Async Methods
        Task<InventoryTypeInfo> GetInventoryTypeByIdAsync(Guid InventoryTypeId);
        Task<IEnumerable<InventoryTypeInfo>> GetInventoryTypesAsync();
        Task<InventoryTypeInfo> GetInventoryTypeByTitleAsync(string Title);
        Task SaveAsync();

        #endregion


      
    }
}
