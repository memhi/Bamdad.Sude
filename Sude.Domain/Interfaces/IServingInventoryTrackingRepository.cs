using Sude.Domain.Models.Serving;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sude.Domain.Interfaces
{
    public interface IServingInventoryTrackingRepository
    {
        Task<IEnumerable<ServingInventoryTrackingInfo>> GetServingInventoryTrackingsAsync();
        IEnumerable<ServingInventoryTrackingInfo> GetServingInventoryTrackings();


        bool AddServingInventoryTracking(ServingInventoryTrackingInfo servingInventoryTracking);
        bool EditServingInventoryTracking(ServingInventoryTrackingInfo servingInventoryTracking);
        bool DeleteServingInventoryTracking(Guid servingInventoryTrackingId);

        Task<ServingInventoryTrackingInfo> GetServingInventoryTrackingByIdAsync(Guid servingInventoryTrackingId);
        ServingInventoryTrackingInfo GetServingInventoryTrackingById(Guid servingInventoryTrackingId);
       

        void Save();
        Task SaveAsync();
    }
}
