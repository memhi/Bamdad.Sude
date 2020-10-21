using Sude.Application.Result;
using Sude.Domain.Models.Work;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sude.Application.Interfaces
{
    public interface IWorkService
    {
        ResultSet<IEnumerable<WorkInfo>> GetWorks();
        ResultSet<WorkInfo> AddWork(WorkInfo request);
        ResultSet EditWork(WorkInfo request);
        ResultSet DeleteWork(Guid WorkId);
        ResultSet<WorkInfo> GetWorkById(Guid WorkId);
        Task<ResultSet<IEnumerable<WorkInfo>>> GetWorksAsync();
        Task<ResultSet<WorkInfo>> AddWorkAsync(WorkInfo request);
        Task<ResultSet> EditWorkAsync(WorkInfo request);
        Task<ResultSet> DeleteWorkAsync(Guid WorkId);
        Task<ResultSet<WorkInfo>> GetWorkByIdAsync(Guid WorkId);
    }
}
