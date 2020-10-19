using Sude.Domain.Models.Work;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sude.Domain.Interfaces
{
    public interface IWorkRepository
    {
        Task<IEnumerable<WorkInfo>> GetWorksAsync();
        Task<IEnumerable<WorkInfo>> GetWorksByTypeAsync(WorkTypeInfo WorkType);
        IEnumerable<WorkInfo> GetWorks();
      //  bool IsExistServing(string Title);

        bool AddWork(WorkInfo Work);
        bool EditWork(WorkInfo Work);
        bool DeleteWork(Guid servingId);

        Task<WorkInfo> GetWorkByIdAsync(Guid WorkId);
        WorkInfo GetWorkById(Guid WorkId);
       

        void Save();
        Task SaveAsync();
    }
}
