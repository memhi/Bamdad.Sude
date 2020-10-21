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
    public class WorkRepository : IWorkRepository
    {

        private GenericRepository<WorkInfo> _WorkRepository;
 

        public WorkRepository(SudeDBContext ctx)
        {
            this._WorkRepository = new GenericRepository<WorkInfo>(ctx);
         
        }

        public async Task<IEnumerable<WorkInfo>> GetWorksAsync()
        {
            return await _WorkRepository.GetAsync(null,null, "WorkType");
        }
        public async Task<IEnumerable<WorkInfo>> GetWorksByTypeAsync(WorkTypeInfo WorkType)
        {
            return await _WorkRepository.GetAsync(w=>w.WorkType==WorkType);
        }
        public bool AddWork(WorkInfo Work)
        {
            try
            {
                _WorkRepository.Insert(Work);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool EditWork(WorkInfo Work)
        {
            try
            {
                _WorkRepository.Update(Work);
            }
            catch
            { 
                return false; 
            }
            return true;
        }

        public async Task<WorkInfo> GetWorkByIdAsync(Guid WorkId)
        {
            return await _WorkRepository.GetByIdAsync(w=>w.Id==WorkId,"WorkType");// (w => w.Id == WorkId, null, "WorkType").GetAwaiter().GetResult().FirstOrDefault();//.Result..FirstOrDefault() ;
        }

        public void Save()
        {
            _WorkRepository.Save();
        }

        public async Task SaveAsync() =>
            await _WorkRepository.SaveAsync();

        public IEnumerable<WorkInfo> GetWorks()
        {
            return _WorkRepository.Get();
        }

      

        

        public bool DeleteWork(Guid WorkId)
        {
            var Work = GetWorkById(WorkId);
            if (Work == null)
                return false;
            try
            {

                Work.IsRemoved = true;
                Work.RemoveDate = DateTime.Now;
                _WorkRepository.Update(Work);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public WorkInfo GetWorkById(Guid WorkId)
        {
            return _WorkRepository.GetById(WorkId);
        }
      
    }
}
