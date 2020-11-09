using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sude.Domain.Interfaces;
 
using Sude.Domain.Models.Work;
 
using Sude.Persistence.Contexts;

namespace Sude.Persistence.Repository
{
    public class WorkTypeRepository : IWorkTypeRepository
    {

        private GenericRepository<WorkTypeInfo> _WorkTypeRepository;

        public WorkTypeRepository(SudeDBContext ctx)
        {
            this._WorkTypeRepository = new GenericRepository<WorkTypeInfo>(ctx);
        }

        public async Task<IEnumerable<WorkTypeInfo>> GetWorkTypesAsync()
        {
            return await _WorkTypeRepository.GetAsync();
        }
        
        public bool AddWorkType(WorkTypeInfo workType)
        {
            try
            {
                _WorkTypeRepository.Insert(workType);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool EditWorkType(WorkTypeInfo workType)
        {
            try
            {
                _WorkTypeRepository.Update(workType);
            }
            catch
            { 
                return false; 
            }
            return true;
        }

        public async Task<WorkTypeInfo> GetWorkTypeByIdAsync(Guid workTypeId)
        {
            return await _WorkTypeRepository.GetByIdAsync(workTypeId);
        }

        public void Save()
        {
            _WorkTypeRepository.Save();
        }

        public async Task SaveAsync() =>
            await _WorkTypeRepository.SaveAsync();

        public IEnumerable<WorkTypeInfo> GetWorkTypes()
        {
            return _WorkTypeRepository.Get();
        }


       

        public WorkTypeInfo GetWorkTypeByTitle(string title)
        {
            IEnumerable<WorkTypeInfo> wts = _WorkTypeRepository.Get(it => it.Title == title);
            if (wts != null && wts.Count() > 0)
                return wts.First();
            return null;
        }
        public async Task<WorkTypeInfo> GetWorkTypeByTitleAsync(string title)
        {
            IEnumerable<WorkTypeInfo> wts = await _WorkTypeRepository.GetAsync(it => it.Title == title);
            if (wts != null && wts.Count() > 0)
                return wts.First();
            return null;
        }


        public bool DeleteWorkType(Guid workTypeId)
        {
            var workType = GetWorkTypeById(workTypeId);
            if (workType == null)
                return false;
            try
            {

                workType.IsRemoved = true;
                workType.RemoveDate = DateTime.Now;
                _WorkTypeRepository.Update(workType);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public WorkTypeInfo GetWorkTypeById(Guid workTypeId)
        {
            return _WorkTypeRepository.GetById(workTypeId);
        }
    }
}
