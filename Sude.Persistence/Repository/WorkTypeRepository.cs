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
        
        public bool AddWorkType(WorkTypeInfo WorkType)
        {
            try
            {
                _WorkTypeRepository.Insert(WorkType);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool EditWorkType(WorkTypeInfo WorkType)
        {
            try
            {
                _WorkTypeRepository.Update(WorkType);
            }
            catch
            { 
                return false; 
            }
            return true;
        }

        public async Task<WorkTypeInfo> GetWorkTypeByIdAsync(Guid WorkTypeId)
        {
            return await _WorkTypeRepository.GetByIdAsync(WorkTypeId);
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


       

        public WorkTypeInfo GetWorkTypeByTitle(string Title)
        {
            IEnumerable<WorkTypeInfo> wts = _WorkTypeRepository.Get(it => it.Title == Title);
            if (wts != null && wts.Count() > 0)
                return wts.First();
            return null;
        }
        public async Task<WorkTypeInfo> GetWorkTypeByTitleAsync(string Title)
        {
            IEnumerable<WorkTypeInfo> wts = await _WorkTypeRepository.GetAsync(it => it.Title == Title);
            if (wts != null && wts.Count() > 0)
                return wts.First();
            return null;
        }


        public bool DeleteWorkType(Guid WorkTypeId)
        {
            var WorkType = GetWorkTypeById(WorkTypeId);
            if (WorkType == null)
                return false;
            try
            {

                WorkType.IsRemoved = true;
                WorkType.RemoveDate = DateTime.Now;
                _WorkTypeRepository.Update(WorkType);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public WorkTypeInfo GetWorkTypeById(Guid WorkTypeId)
        {
            return _WorkTypeRepository.GetById(WorkTypeId);
        }
    }
}
