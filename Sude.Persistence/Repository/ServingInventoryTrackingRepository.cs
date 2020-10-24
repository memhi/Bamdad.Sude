using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sude.Domain.Interfaces;
using Sude.Domain.Models.Serving;
 
using Sude.Persistence.Contexts;

namespace Sude.Persistence.Repository
{
    public class ServingInventoryTrackingRepository : IServingInventoryTrackingRepository
    {

        private GenericRepository<ServingInventoryTrackingInfo> _ServingInventoryTrackingRepository;
 

        public ServingInventoryTrackingRepository(SudeDBContext ctx)
        {
            this._ServingInventoryTrackingRepository = new GenericRepository<ServingInventoryTrackingInfo>(ctx);
         
        }

        public async Task<IEnumerable<ServingInventoryTrackingInfo>> GetServingInventoryTrackingsAsync()
        {
            return await _ServingInventoryTrackingRepository.GetAsync(null,null, "ServingInventoryTrackingType");
        }
        
        public bool AddServingInventoryTracking(ServingInventoryTrackingInfo ServingInventoryTracking)
        {
            try
            {
                _ServingInventoryTrackingRepository.Insert(ServingInventoryTracking);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool EditServingInventoryTracking(ServingInventoryTrackingInfo ServingInventoryTracking)
        {
            try
            {
                _ServingInventoryTrackingRepository.Update(ServingInventoryTracking);
            }
            catch
            { 
                return false; 
            }
            return true;
        }

        public async Task<ServingInventoryTrackingInfo> GetServingInventoryTrackingByIdAsync(Guid ServingInventoryTrackingId)
        {
            return await _ServingInventoryTrackingRepository.GetByIdAsync(w=>w.Id==ServingInventoryTrackingId,"ServingInventoryTrackingType");// (w => w.Id == ServingInventoryTrackingId, null, "ServingInventoryTrackingType").GetAwaiter().GetResult().FirstOrDefault();//.Result..FirstOrDefault() ;
        }

        public void Save()
        {
            _ServingInventoryTrackingRepository.Save();
        }

        public async Task SaveAsync() =>
            await _ServingInventoryTrackingRepository.SaveAsync();

        public IEnumerable<ServingInventoryTrackingInfo> GetServingInventoryTrackings()
        {
            return _ServingInventoryTrackingRepository.Get();
        }

      

        

        public bool DeleteServingInventoryTracking(Guid ServingInventoryTrackingId)
        {
            var ServingInventoryTracking = GetServingInventoryTrackingById(ServingInventoryTrackingId);
            if (ServingInventoryTracking == null)
                return false;
            try
            {

                ServingInventoryTracking.IsRemoved = true;
                ServingInventoryTracking.RemoveDate = DateTime.Now;
                _ServingInventoryTrackingRepository.Update(ServingInventoryTracking);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public ServingInventoryTrackingInfo GetServingInventoryTrackingById(Guid ServingInventoryTrackingId)
        {
            return _ServingInventoryTrackingRepository.GetById(ServingInventoryTrackingId);
        }
      
    }
}
