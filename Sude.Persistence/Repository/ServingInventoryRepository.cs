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
        public async Task<IEnumerable<ServingInventoryInfo>> GetServingInventorysByTypeAsync(Guid inventoryTypeId)
        {
            return await _ServingInventoryRepository.GetAsync(w=>w.InventoryTypeId== inventoryTypeId);
        }
        public bool AddServingInventory(ServingInventoryInfo servingInventory)
        {
            try
            {
                _ServingInventoryRepository.Insert(servingInventory);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool EditServingInventory(ServingInventoryInfo servingInventory)
        {
            try
            {
                _ServingInventoryRepository.Update(servingInventory);
            }
            catch
            { 
                return false; 
            }
            return true;
        }

        public async Task<ServingInventoryInfo> GetServingInventoryByIdAsync(Guid servingInventoryId)
        {
            return await _ServingInventoryRepository.GetByIdAsync(w=>w.Id==servingInventoryId,"ServingInventoryType");// (w => w.Id == ServingInventoryId, null, "ServingInventoryType").GetAwaiter().GetResult().FirstOrDefault();//.Result..FirstOrDefault() ;
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

      

        

        public bool DeleteServingInventory(Guid servingInventoryId)
        {
            var servingInventory = GetServingInventoryById(servingInventoryId);
            if (servingInventory == null)
                return false;
            try
            {

                servingInventory.IsRemoved = true;
               servingInventory.RemoveDate = DateTime.Now;
                _ServingInventoryRepository.Update(servingInventory);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public ServingInventoryInfo GetServingInventoryById(Guid servingInventoryId)
        {
            return _ServingInventoryRepository.GetById(servingInventoryId);
        }
      
    }
}
