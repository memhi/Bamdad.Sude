using Sude.Application.Result;
using Sude.Domain.Models.Serving;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sude.Application.Interfaces
{
    public interface IServingInventoryTrackingService
    {
        ResultSet<IEnumerable<ServingInventoryTrackingInfo>> GetServingInventoryTrackings();
        ResultSet<ServingInventoryTrackingInfo> AddServingInventoryTracking(ServingInventoryTrackingInfo  servingInventoryTracking);
        ResultSet EditServingInventoryTracking(ServingInventoryTrackingInfo servingInventoryTracking);
        ResultSet DeleteServingInventoryTracking(Guid servingInventoryTrackingId);
        ResultSet<ServingInventoryTrackingInfo> GetServingInventoryTrackingById(Guid servingInventoryTrackingId);
        Task<ResultSet<IEnumerable<ServingInventoryTrackingInfo>>> GetServingInventoryTrackingsAsync();
        Task<ResultSet<ServingInventoryTrackingInfo>> AddServingInventoryTrackingAsync(ServingInventoryTrackingInfo servingInventoryTracking);
        Task<ResultSet> EditServingInventoryTrackingAsync(ServingInventoryTrackingInfo servingInventoryTracking);
        Task<ResultSet> DeleteServingInventoryTrackingAsync(Guid servingInventoryTrackingId);
        Task<ResultSet<ServingInventoryTrackingInfo>> GetServingInventoryTrackingByIdAsync(Guid servingInventoryTrackingId);
    }
}
