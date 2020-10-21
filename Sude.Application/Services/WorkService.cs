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
       

        public WorkService(IWorkRepository WorkRepository)
        {
            this._WorkRepository = WorkRepository;
        
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

        public ResultSet<WorkInfo> GetWorkById(Guid WorkId)
        {
            WorkInfo Work = _WorkRepository.GetWorkById(WorkId);

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

        public ResultSet<WorkInfo> AddWork(WorkInfo request)
        {
            _WorkRepository.AddWork(request);
            _WorkRepository.Save();

            return new ResultSet<WorkInfo>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = request
            };
        }

        public ResultSet EditWork(WorkInfo request)
        {
            if (!_WorkRepository.EditWork(request))
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

        public ResultSet DeleteWork(Guid WorkId)
        {

            if (!_WorkRepository.DeleteWork(WorkId))
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

        public async Task<ResultSet<WorkInfo>> AddWorkAsync(WorkInfo request)
        {
            _WorkRepository.AddWork(request);

            try { await _WorkRepository.SaveAsync(); }

            catch (Exception e) { return new ResultSet<WorkInfo>() { IsSucceed = false, Message = e.Message }; }

            return new ResultSet<WorkInfo>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = request
            };
        }

        public async Task<ResultSet> EditWorkAsync(WorkInfo request)
        {
            if (!_WorkRepository.EditWork(request))
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

        public async Task<ResultSet> DeleteWorkAsync(Guid WorkId)
        {



























            if (!_WorkRepository.DeleteWork(WorkId))
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

        public async Task<ResultSet<WorkInfo>> GetWorkByIdAsync(Guid WorkId)
        {
            WorkInfo Work = await _WorkRepository.GetWorkByIdAsync(WorkId);

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
