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
    public class ServingRepository : IServingRepository
    {

        private GenericRepository<ServingInfo> _servingRepository;

        public ServingRepository(SudeDBContext ctx)
        {
            this._servingRepository = new GenericRepository<ServingInfo>(ctx);
        }

        public async Task<IEnumerable<ServingInfo>> GetServingsByWorkIdAsync(Guid workId)
        {
            return await _servingRepository.GetAsync(s=>s.WorkId==workId, null, "Work,ServingInventory");
        }

        public async Task<IEnumerable<ServingInfo>> GetServingsAsync()
        {
            return await _servingRepository.GetAsync(null,null, "Work,ServingInventory");
        }
        public bool AddServing(ServingInfo serving)
        {
            try
            {
                _servingRepository.Insert(serving);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool EditServing(ServingInfo serving)
        {
            try
            {
                
                _servingRepository.Update(serving);
            }
            catch
            { 
                return false; 
            }
            return true;
        }

        public async Task<ServingInfo> GetServingByIdAsync(Guid servingId)
        {
            return await _servingRepository.GetByIdAsync(s=>s.Id==servingId, "Work,ServingInventory");
        
        }

        public void Save()
        {
            _servingRepository.Save();
        }

        public async Task SaveAsync() =>
            await _servingRepository.SaveAsync();

        public IEnumerable<ServingInfo> GetServings()
        {
            return _servingRepository.Get(null, null, "Work,ServingInventory");
        }

        public bool IsExistServing(string title,Guid? id, Guid workId)
        {
            return (_servingRepository.Get(s => s.Title == title && s.WorkId==workId && (id==null || s.Id!=id)).Count() > 0 ? true : false);
        }

        

        public bool DeleteServing(Guid servingId)
        {
            var serving = GetServingById(servingId);
            if (serving == null)
                return false;
            try
            {
                //_servingRepository.Delete(serving);
                serving.IsRemoved = true;
                serving.RemoveDate = DateTime.Now;
                _servingRepository.Update(serving);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public ServingInfo GetServingById(Guid servingId)
        {
            return _servingRepository.GetById(s=>s.Id== servingId, "Work,ServingInventory");
        }
    }
}
