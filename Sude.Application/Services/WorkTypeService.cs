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

        public WorkTypeService(IWorkTypeRepository workTypeRepository)
        {
            this._WorkTypeRepository = workTypeRepository;
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

        public ResultSet<WorkTypeInfo> GetWorkTypeById(Guid workTypeId)
        {
            WorkTypeInfo WorkType = _WorkTypeRepository.GetWorkTypeById(workTypeId);

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

        public ResultSet<WorkTypeInfo> AddWorkType(WorkTypeInfo  workType)
        {
            WorkTypeInfo workTypeInfo = _WorkTypeRepository.GetWorkTypeByTitle(workType.Title);
            if (workTypeInfo != null && workTypeInfo.Title == workType.Title)
            {
                return new ResultSet<WorkTypeInfo>() { IsSucceed = false, Message = "Duplicate Data" };

            }
            _WorkTypeRepository.AddWorkType(workType);
            _WorkTypeRepository.Save();

            return new ResultSet<WorkTypeInfo>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = workType
            };
        }

        public ResultSet EditWorkType(WorkTypeInfo workType)
        {
            WorkTypeInfo  workTypeInfo = _WorkTypeRepository.GetWorkTypeByTitle(workType.Title);
            if (workTypeInfo != null && workTypeInfo.Id != workType.Id)
            {
                return new ResultSet<WorkTypeInfo>() { IsSucceed = false, Message = "Duplicate Data" };

            }

            if (!_WorkTypeRepository.EditWorkType(workType))
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

        public ResultSet DeleteWorkType(Guid workTypeId)
        {

            if (!_WorkTypeRepository.DeleteWorkType(workTypeId))
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


   


        public async Task<ResultSet<WorkTypeInfo>> AddWorkTypeAsync(WorkTypeInfo workType)
        {
            WorkTypeInfo workTypeInfo = _WorkTypeRepository.GetWorkTypeByTitle(workType.Title);
            if (workTypeInfo != null && workTypeInfo.Title == workType.Title)
            {
                return new ResultSet<WorkTypeInfo>() { IsSucceed = false, Message = "Duplicate Data" };

            }


            _WorkTypeRepository.AddWorkType(workType);

            try { await _WorkTypeRepository.SaveAsync(); }

            catch (Exception e) { return new ResultSet<WorkTypeInfo>() { IsSucceed = false, Message = e.Message }; }

            return new ResultSet<WorkTypeInfo>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = workType
            };
        }

        public async Task<ResultSet> EditWorkTypeAsync(WorkTypeInfo workType)
        {
            WorkTypeInfo workTypeInfo = _WorkTypeRepository.GetWorkTypeByTitle(workType.Title);
            if (workTypeInfo != null && workTypeInfo.Id != workType.Id)
            {
                return new ResultSet<WorkTypeInfo>() { IsSucceed = false, Message = "Duplicate Data" };

            }
            if (!_WorkTypeRepository.EditWorkType(workType))
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

        public async Task<ResultSet> DeleteWorkTypeAsync(Guid workTypeId)
        {

            WorkTypeInfo workType = _WorkTypeRepository.GetWorkTypeWithWorksById(workTypeId);
            if(workType==null)
                return new ResultSet() { IsSucceed = false, Message = "WorkType Not Found" };

            if(workType.Works!=null && workType.Works.Count()>0)
                return new ResultSet() { IsSucceed = false, Message = "WorkType Not Deleted. WorkType has work" };

            if (!_WorkTypeRepository.DeleteWorkType(workTypeId))
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

        public async Task<ResultSet<WorkTypeInfo>> GetWorkTypeByIdAsync(Guid workTypeId)
        {
            WorkTypeInfo WorkType = await _WorkTypeRepository.GetWorkTypeByIdAsync(workTypeId);

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
