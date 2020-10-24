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
        ResultSet<InventoryTypeInfo> AddInventoryType(InventoryTypeInfo request);
        ResultSet EditInventoryType(InventoryTypeInfo request);
        ResultSet DeleteInventoryType(Guid InventoryTypeId);
        ResultSet<InventoryTypeInfo> GetInventoryTypeById(Guid InventoryTypeId);
        Task<ResultSet<IEnumerable<InventoryTypeInfo>>> GetInventoryTypesAsync();
        Task<ResultSet<InventoryTypeInfo>> AddInventoryTypeAsync(InventoryTypeInfo request);
        Task<ResultSet> EditInventoryTypeAsync(InventoryTypeInfo request);
        Task<ResultSet> DeleteInventoryTypeAsync(Guid InventoryTypeId);
        Task<ResultSet<InventoryTypeInfo>> GetInventoryTypeByIdAsync(Guid InventoryTypeId);
    }
}
