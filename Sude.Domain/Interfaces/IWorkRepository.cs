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
        Task<IEnumerable<WorkInfo>> GetWorksByTypeAsync(WorkTypeInfo workType);
        Task<IEnumerable<WorkInfo>> GetWorksByUserIdAsync(Guid UserID);
        IEnumerable<WorkInfo> GetWorks();
 

        bool AddWork(WorkInfo work);
        bool EditWork(WorkInfo work);
        bool DeleteWork(Guid workId);

        Task<WorkInfo> GetWorkByIdAsync(Guid workId);
        WorkInfo GetWorkById(Guid workId);
        Task<WorkInfo> GetWorkAsync(string title, WorkTypeInfo workType);
        WorkInfo GetWork(string title, WorkTypeInfo workType);



        void Save();
        Task SaveAsync();
    }
}
