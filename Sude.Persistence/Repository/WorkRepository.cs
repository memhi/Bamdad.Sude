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

        public async Task<WorkInfo> GetWorkAsync(string title,WorkTypeInfo workType)
        {
            IEnumerable<WorkInfo> workInfos= await _WorkRepository.GetAsync(w => w.WorkType == workType && w.Title==title, null, "");
            if(workInfos!=null)
                    return workInfos.FirstOrDefault();
            return null;
        }

        public   WorkInfo GetWork(string title, WorkTypeInfo workType)
        {
            IEnumerable<WorkInfo> workInfos =  _WorkRepository.Get(w => w.WorkType == workType && w.Title == title, null, "");
            if (workInfos != null)
                return workInfos.FirstOrDefault();
            return null;
        }
        public async Task<IEnumerable<WorkInfo>> GetWorksByTypeAsync(WorkTypeInfo workType)
        {
            return await _WorkRepository.GetAsync(w=>w.WorkType==workType);
        }
        public bool AddWork(WorkInfo work)
        {
            try
            {
                _WorkRepository.Insert(work);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool EditWork(WorkInfo work)
        {
            try
            {
                _WorkRepository.Update(work);
            }
            catch
            { 
                return false; 
            }
            return true;
        }

        public async Task<WorkInfo> GetWorkByIdAsync(Guid workId)
        {
            return await _WorkRepository.GetByIdAsync(w=>w.Id==workId,"WorkType");// (w => w.Id == WorkId, null, "WorkType").GetAwaiter().GetResult().FirstOrDefault();//.Result..FirstOrDefault() ;
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

      

        

        public bool DeleteWork(Guid workId)
        {
            var work = GetWorkById(workId);
            if (work == null)
                return false;
            try
            {

                work.IsRemoved = true;
                work.RemoveDate = DateTime.Now;
                _WorkRepository.Update(work);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public WorkInfo GetWorkById(Guid workId)
        {
            return _WorkRepository.GetById(workId);
        }
      
    }
}
