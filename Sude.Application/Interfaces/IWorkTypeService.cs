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
        ResultSet<WorkTypeInfo> AddWorkType(WorkTypeInfo request);
        ResultSet EditWorkType(WorkTypeInfo request);
        ResultSet DeleteWorkType(Guid WorkTypeId);
        ResultSet<WorkTypeInfo> GetWorkTypeById(Guid WorkTypeId);
        Task<ResultSet<IEnumerable<WorkTypeInfo>>> GetWorkTypesAsync();
        Task<ResultSet<WorkTypeInfo>> AddWorkTypeAsync(WorkTypeInfo request);
        Task<ResultSet> EditWorkTypeAsync(WorkTypeInfo request);
        Task<ResultSet> DeleteWorkTypeAsync(Guid WorkTypeId);
        Task<ResultSet<WorkTypeInfo>> GetWorkTypeByIdAsync(Guid WorkTypeId);
    }
}
