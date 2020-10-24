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
        ResultSet<ServingInventoryTrackingInfo> AddServingInventoryTracking(ServingInventoryTrackingInfo request);
        ResultSet EditServingInventoryTracking(ServingInventoryTrackingInfo request);
        ResultSet DeleteServingInventoryTracking(Guid ServingInventoryTrackingId);
        ResultSet<ServingInventoryTrackingInfo> GetServingInventoryTrackingById(Guid ServingInventoryTrackingId);
        Task<ResultSet<IEnumerable<ServingInventoryTrackingInfo>>> GetServingInventoryTrackingsAsync();
        Task<ResultSet<ServingInventoryTrackingInfo>> AddServingInventoryTrackingAsync(ServingInventoryTrackingInfo request);
        Task<ResultSet> EditServingInventoryTrackingAsync(ServingInventoryTrackingInfo request);
        Task<ResultSet> DeleteServingInventoryTrackingAsync(Guid ServingInventoryTrackingId);
        Task<ResultSet<ServingInventoryTrackingInfo>> GetServingInventoryTrackingByIdAsync(Guid ServingInventoryTrackingId);
    }
}
