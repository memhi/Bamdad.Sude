using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sude.Application.Interfaces;
using Sude.Application.Result;
using Sude.Domain.Interfaces;
using Sude.Domain.Models.Common;
using Sude.Domain.Models.Work;

namespace Sude.Application.Services
{
    public class WorkTypeService : IWorkTypeService
    {
        private IWorkTypeRepository _WorkTypeRepository;

        public WorkTypeService(IWorkTypeRepository WorkTypeRepository)
        {
            this._WorkTypeRepository = WorkTypeRepository;
        }
        public ResultSet<IEnumerable<WorkTypeInfo>> GetWorkTypes()
        {
            return new ResultSet<IEnumerable<WorkTypeInfo>>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = _WorkTypeRepository.GetWorkTypes()
            };
        }

        public ResultSet<WorkTypeInfo> GetWorkTypeById(Guid WorkTypeId)
        {
            WorkTypeInfo WorkType = _WorkTypeRepository.GetWorkTypeById(WorkTypeId);

            if (WorkType == null)
                return new ResultSet<WorkTypeInfo>()
                {
                    IsSucceed = false,
                    Message = "WorkType Not Found",
                    Data = null
                };

            return new ResultSet<WorkTypeInfo>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = WorkType
            };
        }

        public ResultSet<WorkTypeInfo> AddWorkType(WorkTypeInfo request)
        {
            WorkTypeInfo workType = _WorkTypeRepository.GetWorkTypeByTitle(request.Title);
            if (workType != null && workType.Title == request.Title)
            {
                return new ResultSet<WorkTypeInfo>() { IsSucceed = false, Message = "Duplicate Data" };

            }
            _WorkTypeRepository.AddWorkType(request);
            _WorkTypeRepository.Save();

            return new ResultSet<WorkTypeInfo>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = request
            };
        }

        public ResultSet EditWorkType(WorkTypeInfo request)
        {
            WorkTypeInfo  workType = _WorkTypeRepository.GetWorkTypeByTitle(request.Title);
            if (workType != null && workType.Id != request.Id)
            {
                return new ResultSet<WorkTypeInfo>() { IsSucceed = false, Message = "Duplicate Data" };

            }

            if (!_WorkTypeRepository.EditWorkType(request))
                return new ResultSet() { IsSucceed = false, Message = "WorkType Not Edited" };

            try
            {
                _WorkTypeRepository.Save();
            }
            catch
            {
                return new ResultSet() { IsSucceed = false, Message = "WorkType Not Edited" };
            }
            return new ResultSet() { IsSucceed = true, Message = string.Empty };

        }

        public ResultSet DeleteWorkType(Guid WorkTypeId)
        {

            if (!_WorkTypeRepository.DeleteWorkType(WorkTypeId))
                return new ResultSet() { IsSucceed = false, Message = "WorkType Not Deleted" };

            try
            {
                _WorkTypeRepository.Save();
            }
            catch
            {
                return new ResultSet() { IsSucceed = false, Message = "WorkType Not Deleted" };
            }
            return new ResultSet() { IsSucceed = true, Message = string.Empty };
        }

        public async Task<ResultSet<IEnumerable<WorkTypeInfo>>> GetWorkTypesAsync()
        {
            return new ResultSet<IEnumerable<WorkTypeInfo>>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = await _WorkTypeRepository.GetWorkTypesAsync()
            };
        }


   


        public async Task<ResultSet<WorkTypeInfo>> AddWorkTypeAsync(WorkTypeInfo request)
        {
            WorkTypeInfo workType = await _WorkTypeRepository.GetWorkTypeByTitleAsync(request.Title);
            if (workType != null && workType.Title == request.Title)
            {
                return new ResultSet<WorkTypeInfo>() { IsSucceed = false, Message = "Duplicate Data" };

            }


            _WorkTypeRepository.AddWorkType(request);

            try { await _WorkTypeRepository.SaveAsync(); }

            catch (Exception e) { return new ResultSet<WorkTypeInfo>() { IsSucceed = false, Message = e.Message }; }

            return new ResultSet<WorkTypeInfo>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = request
            };
        }

        public async Task<ResultSet> EditWorkTypeAsync(WorkTypeInfo request)
        {
            WorkTypeInfo workType = await _WorkTypeRepository.GetWorkTypeByTitleAsync(request.Title);
            if (workType != null && workType.Id != request.Id)
            {
                return new ResultSet<WorkTypeInfo>() { IsSucceed = false, Message = "Duplicate Data" };

            }
            if (!_WorkTypeRepository.EditWorkType(request))
                return new ResultSet() { IsSucceed = false, Message = "WorkType Not Edited" };

            try
            {
                await _WorkTypeRepository.SaveAsync();
            }
            catch (Exception e)
            {
                return new ResultSet() { IsSucceed = false, Message = e.Message };
            }
            return new ResultSet() { IsSucceed = true, Message = string.Empty };
        }

        public async Task<ResultSet> DeleteWorkTypeAsync(Guid WorkTypeId)
        {



























            if (!_WorkTypeRepository.DeleteWorkType(WorkTypeId))
                return new ResultSet() { IsSucceed = false, Message = "WorkType Not Deleted" };

            try
            {
                await _WorkTypeRepository.SaveAsync();
            }
            catch
            {
                return new ResultSet() { IsSucceed = false, Message = "WorkType Not Deleted" };
            }
            return new ResultSet() { IsSucceed = true, Message = string.Empty };
        }

        public async Task<ResultSet<WorkTypeInfo>> GetWorkTypeByIdAsync(Guid WorkTypeId)
        {
            WorkTypeInfo WorkType = await _WorkTypeRepository.GetWorkTypeByIdAsync(WorkTypeId);

            if (WorkType == null)
                return new ResultSet<WorkTypeInfo>()
                {
                    IsSucceed = false,
                    Message = "WorkType Not Found",
                    Data = null
                };

            return new ResultSet<WorkTypeInfo>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = WorkType
            };
        }
    }
}
