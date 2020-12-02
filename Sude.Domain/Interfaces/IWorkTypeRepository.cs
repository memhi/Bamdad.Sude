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
 

        bool AddWorkType(WorkTypeInfo workType);
        bool EditWorkType(WorkTypeInfo workType);
        bool DeleteWorkType(Guid workTypeId);

        Task<WorkTypeInfo> GetWorkTypeByIdAsync(Guid workTypeId);
        WorkTypeInfo GetWorkTypeById(Guid workTypeId);
        Task<WorkTypeInfo> GetWorkTypeByTitleAsync(string title);
            WorkTypeInfo GetWorkTypeByTitle(string title);
        WorkTypeInfo GetWorkTypeWithWorksById(Guid workTypeId);

        void Save();
        Task SaveAsync();
    }
}
