using Sude.Application.Result;
using Sude.Domain.Models.Serving;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sude.Application.Interfaces
{
    public interface IInventoryTypeService
    {
        ResultSet<IEnumerable<InventoryTypeInfo>> GetInventoryTypes();
        ResultSet<InventoryTypeInfo> AddInventoryType(InventoryTypeInfo  inventoryType);
        ResultSet EditInventoryType(InventoryTypeInfo inventoryType);
        ResultSet DeleteInventoryType(Guid inventoryTypeId);
        ResultSet<InventoryTypeInfo> GetInventoryTypeById(Guid inventoryTypeId);
        Task<ResultSet<IEnumerable<InventoryTypeInfo>>> GetInventoryTypesAsync();
        Task<ResultSet<InventoryTypeInfo>> AddInventoryTypeAsync(InventoryTypeInfo inventoryType);
        Task<ResultSet> EditInventoryTypeAsync(InventoryTypeInfo inventoryType);
        Task<ResultSet> DeleteInventoryTypeAsync(Guid inventoryTypeId);
        Task<ResultSet<InventoryTypeInfo>> GetInventoryTypeByIdAsync(Guid inventoryTypeId);
    }
}
