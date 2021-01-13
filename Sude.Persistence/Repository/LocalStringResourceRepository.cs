using Sude.Domain.Interfaces;
using Sude.Domain.Models.Localization;
using Sude.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sude.Persistence.Repository
{
    public class LocalStringResourceRepository : ILocalStringResourceRepository
    {

        private GenericRepository<LocalStringResourceInfo> _LocalStringResourceRepository;

        public LocalStringResourceRepository(SudeDBContext ctx)
        {
            //this._ctx = ctx;
            this._LocalStringResourceRepository = new GenericRepository<LocalStringResourceInfo>(ctx);
        }

   
       

       
        public IEnumerable<LocalStringResourceInfo> GetLocalStringResources()
        {
            //return _ctx.LocalStringResources;
            return _LocalStringResourceRepository.Get();
        }

        public async Task<IEnumerable<LocalStringResourceInfo>> GetLocalStringResourcesByLanguageIdAsync(Guid languageId)
        {
            //return _ctx.LocalStringResources;
            return await _LocalStringResourceRepository.GetAsync(lsr=>lsr.LanguageId==languageId);
        }

        public async Task<IEnumerable<LocalStringResourceInfo>> GetLocalStringResourcesAsync()
        {
            //return _ctx.LocalStringResources;
            return await _LocalStringResourceRepository.GetAsync();
        }
        public bool AddLocalStringResource(LocalStringResourceInfo LocalStringResource)
        {
            try
            {
                _LocalStringResourceRepository.Insert(LocalStringResource);
            }
            catch
            {
                return false;
            }
            return true;
        }

       


        
       
      
        public LocalStringResourceInfo GetLocalStringResourceById(Guid LocalStringResourceId)
        {
            //return _ctx.LocalStringResources.Find(LocalStringResourceId);
            return _LocalStringResourceRepository.GetById(LocalStringResourceId);
        }
        public async  Task<LocalStringResourceInfo> GetLocalStringResourceByIdAsync(Guid LocalStringResourceId)
        {
            //return _ctx.LocalStringResources.Find(LocalStringResourceId);
            return await _LocalStringResourceRepository.GetByIdAsync(LocalStringResourceId);
        }
        public bool EditLocalStringResource(LocalStringResourceInfo LocalStringResource)
        {
            //_ctx.Entry(LocalStringResource).State = EntityState.Modified;
            ////_ctx.LocalStringResources.Update(LocalStringResource);
            _LocalStringResourceRepository.Update(LocalStringResource);
            return true;
        }
        public bool DeleteLocalStringResource(Guid LocalStringResourceId)
        {
            var LocalStringResource = GetLocalStringResourceById(LocalStringResourceId);
            if (LocalStringResource == null)
                return false;
            try
            {


                _LocalStringResourceRepository.Delete(LocalStringResource);
            }
            catch
            {
                return false;
            }
            return true;
        }
        public void Save()
        {
            //_ctx.SaveChanges();
            _LocalStringResourceRepository.Save();
        }

        public async Task SaveAsync() =>
           await _LocalStringResourceRepository.SaveAsync();
    }
}
