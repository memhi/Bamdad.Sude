using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sude.Application.Interfaces;
using Sude.Application.Result;
using Sude.Domain.Interfaces;
using Sude.Domain.Models.Work;

namespace Sude.Application.Services
{
    public class WorkService : IWorkService
    {
        private IWorkRepository _WorkRepository;
       

        public WorkService(IWorkRepository workRepository)
        {
            this._WorkRepository = workRepository;
        
        }
        public ResultSet<IEnumerable<WorkInfo>> GetWorks()
        {
            return new ResultSet<IEnumerable<WorkInfo>>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = _WorkRepository.GetWorks()
            };
        }

        public ResultSet<WorkInfo> GetWorkById(Guid workId)
        {
            WorkInfo Work = _WorkRepository.GetWorkById(workId);

            if (Work == null)
                return new ResultSet<WorkInfo>()
                {
                    IsSucceed = false,
                    Message = "Work Not Found",
                    Data = null
                };

            return new ResultSet<WorkInfo>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = Work
            };
        }

        public ResultSet<WorkInfo> AddWork(WorkInfo  work)
        {
            WorkInfo workInfo = _WorkRepository.GetWork(work.Title, work.WorkType);
            if (workInfo != null && workInfo.Title == work.Title)
            {

                return new ResultSet<WorkInfo>() { IsSucceed = false, Message = "Duplicate Data" };


            }

            _WorkRepository.AddWork(work);
            _WorkRepository.Save();

            return new ResultSet<WorkInfo>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = work
            };
        }

        public ResultSet EditWork(WorkInfo work)
        {
            WorkInfo workInfo = _WorkRepository.GetWork(work.Title, work.WorkType);
            if (workInfo != null && workInfo.Id != work.Id)
            {

                return new ResultSet<WorkInfo>() { IsSucceed = false, Message = "Duplicate Data" };


            }

            if (!_WorkRepository.EditWork(work))
                return new ResultSet() { IsSucceed = false, Message = "Work Not Edited" };

            try
            {
                _WorkRepository.Save();
            }
            catch
            {
                return new ResultSet() { IsSucceed = false, Message = "Work Not Edited" };
            }
            return new ResultSet() { IsSucceed = true, Message = string.Empty };

        }

        public ResultSet DeleteWork(Guid workId)
        {

            if (!_WorkRepository.DeleteWork(workId))
                return new ResultSet() { IsSucceed = false, Message = "Work Not Deleted" };

            try
            {
                _WorkRepository.Save();
            }
            catch
            {
                return new ResultSet() { IsSucceed = false, Message = "Work Not Deleted" };
            }
            return new ResultSet() { IsSucceed = true, Message = string.Empty };
        }

        public async Task<ResultSet<IEnumerable<WorkInfo>>> GetWorksAsync()
        {
            return new ResultSet<IEnumerable<WorkInfo>>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = await _WorkRepository.GetWorksAsync()
            };
        }

        public async Task<ResultSet<WorkInfo>> AddWorkAsync(WorkInfo work)
        {
            WorkInfo workInfo = await _WorkRepository.GetWorkAsync(work.Title, work.WorkType);
            if(workInfo!=null && workInfo.Title== work.Title)
            {
               
                    return new ResultSet<WorkInfo>() { IsSucceed = false, Message = "Duplicate Data" };
                  

          }

            _WorkRepository.AddWork(work);

            try { await _WorkRepository.SaveAsync(); }

            catch (Exception e) { return new ResultSet<WorkInfo>() { IsSucceed = false, Message = e.Message }; }

            return new ResultSet<WorkInfo>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = work
            };
        }

        public async Task<ResultSet> EditWorkAsync(WorkInfo work)
        {


            WorkInfo workInfo = await _WorkRepository.GetWorkAsync(work.Title, work.WorkType);
            if (workInfo != null && workInfo.Id != work.Id)
            {

                return new ResultSet<WorkInfo>() { IsSucceed = false, Message = "Duplicate Data" };


            }


            if (!_WorkRepository.EditWork(work))
                return new ResultSet() { IsSucceed = false, Message = "Work Not Edited" };

            try
            {
                await _WorkRepository.SaveAsync();
            }
            catch (Exception e)
            {
                return new ResultSet() { IsSucceed = false, Message = e.Message };
            }
            return new ResultSet() { IsSucceed = true, Message = string.Empty };
        }

        public async Task<ResultSet> DeleteWorkAsync(Guid workId)
        {

            
            if (!_WorkRepository.DeleteWork(workId))
                return new ResultSet() { IsSucceed = false, Message = "Work Not Deleted" };

            try
            {
                await _WorkRepository.SaveAsync();
            }
            catch
            {
                return new ResultSet() { IsSucceed = false, Message = "Work Not Deleted" };
            }
            return new ResultSet() { IsSucceed = true, Message = string.Empty };
        }

        public async Task<ResultSet<WorkInfo>> GetWorkByIdAsync(Guid workId)
        {
            WorkInfo Work = await _WorkRepository.GetWorkByIdAsync(workId);

            if (Work == null)
                return new ResultSet<WorkInfo>()
                {
                    IsSucceed = false,
                    Message = "Work Not Found",
                    Data = null
                };

            return new ResultSet<WorkInfo>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = Work
            };
        }
    }
}
