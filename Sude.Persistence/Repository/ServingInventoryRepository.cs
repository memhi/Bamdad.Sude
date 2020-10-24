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
    public class ServingInventoryRepository : IServingInventoryRepository
    {

        private GenericRepository<ServingInventoryInfo> _ServingInventoryRepository;
 

        public ServingInventoryRepository(SudeDBContext ctx)
        {
            this._ServingInventoryRepository = new GenericRepository<ServingInventoryInfo>(ctx);
         
        }

        public async Task<IEnumerable<ServingInventoryInfo>> GetServingInventorysAsync()
        {
            return await _ServingInventoryRepository.GetAsync(null,null, "ServingInventoryType");
        }
        public async Task<IEnumerable<ServingInventoryInfo>> GetServingInventorysByTypeAsync(Guid InventoryTypeId)
        {
            return await _ServingInventoryRepository.GetAsync(w=>w.InventoryTypeId== InventoryTypeId);
        }
        public bool AddServingInventory(ServingInventoryInfo ServingInventory)
        {
            try
            {
                _ServingInventoryRepository.Insert(ServingInventory);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool EditServingInventory(ServingInventoryInfo ServingInventory)
        {
            try
            {
                _ServingInventoryRepository.Update(ServingInventory);
            }
            catch
            { 
                return false; 
            }
            return true;
        }

        public async Task<ServingInventoryInfo> GetServingInventoryByIdAsync(Guid ServingInventoryId)
        {
            return await _ServingInventoryRepository.GetByIdAsync(w=>w.Id==ServingInventoryId,"ServingInventoryType");// (w => w.Id == ServingInventoryId, null, "ServingInventoryType").GetAwaiter().GetResult().FirstOrDefault();//.Result..FirstOrDefault() ;
        }

        public void Save()
        {
            _ServingInventoryRepository.Save();
        }

        public async Task SaveAsync() =>
            await _ServingInventoryRepository.SaveAsync();

        public IEnumerable<ServingInventoryInfo> GetServingInventorys()
        {
            return _ServingInventoryRepository.Get();
        }

      

        

        public bool DeleteServingInventory(Guid ServingInventoryId)
        {
            var ServingInventory = GetServingInventoryById(ServingInventoryId);
            if (ServingInventory == null)
                return false;
            try
            {

                ServingInventory.IsRemoved = true;
                ServingInventory.RemoveDate = DateTime.Now;
                _ServingInventoryRepository.Update(ServingInventory);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public ServingInventoryInfo GetServingInventoryById(Guid ServingInventoryId)
        {
            return _ServingInventoryRepository.GetById(ServingInventoryId);
        }
      
    }
}
