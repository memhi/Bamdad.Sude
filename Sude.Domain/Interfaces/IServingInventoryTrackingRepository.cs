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
      //  bool IsExistServing(string Title);

        bool AddServingInventoryTracking(ServingInventoryTrackingInfo ServingInventoryTracking);
        bool EditServingInventoryTracking(ServingInventoryTrackingInfo ServingInventoryTracking);
        bool DeleteServingInventoryTracking(Guid servingId);

        Task<ServingInventoryTrackingInfo> GetServingInventoryTrackingByIdAsync(Guid ServingInventoryTrackingId);
        ServingInventoryTrackingInfo GetServingInventoryTrackingById(Guid ServingInventoryTrackingId);
       

        void Save();
        Task SaveAsync();
    }
}
