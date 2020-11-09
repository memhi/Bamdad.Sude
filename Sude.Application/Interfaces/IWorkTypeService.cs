using Sude.Application.Result;
using Sude.Domain.Models.Work;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sude.Application.Interfaces
{
    public interface IWorkTypeService
    {
        ResultSet<IEnumerable<WorkTypeInfo>> GetWorkTypes();
        ResultSet<WorkTypeInfo> AddWorkType(WorkTypeInfo  workType);
        ResultSet EditWorkType(WorkTypeInfo workType);
        ResultSet DeleteWorkType(Guid workTypeId);
        ResultSet<WorkTypeInfo> GetWorkTypeById(Guid workTypeId);
        Task<ResultSet<IEnumerable<WorkTypeInfo>>> GetWorkTypesAsync();
        Task<ResultSet<WorkTypeInfo>> AddWorkTypeAsync(WorkTypeInfo workType);
        Task<ResultSet> EditWorkTypeAsync(WorkTypeInfo workType);
        Task<ResultSet> DeleteWorkTypeAsync(Guid workTypeId);
        Task<ResultSet<WorkTypeInfo>> GetWorkTypeByIdAsync(Guid workTypeId);
    }
}
