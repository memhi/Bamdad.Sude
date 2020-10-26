using Sude.Domain.Models.Common;
using Sude.Domain.Models.Work;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sude.Domain.Interfaces
{
    public interface IWorkTypeRepository
    {
        Task<IEnumerable<WorkTypeInfo>> GetWorkTypesAsync();
        IEnumerable<WorkTypeInfo> GetWorkTypes();
      //  bool IsExistServing(string Title);

        bool AddWorkType(WorkTypeInfo WorkType);
        bool EditWorkType(WorkTypeInfo WorkType);
        bool DeleteWorkType(Guid servingId);

        Task<WorkTypeInfo> GetWorkTypeByIdAsync(Guid WorkTypeId);
        WorkTypeInfo GetWorkTypeById(Guid WorkTypeId);
        Task<WorkTypeInfo> GetWorkTypeByTitleAsync(string Title);
            WorkTypeInfo GetWorkTypeByTitle(string Title);
   

        void Save();
        Task SaveAsync();
    }
}
