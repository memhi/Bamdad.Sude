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
        ResultSet<WorkInfo> AddWork(WorkInfo  work);
        ResultSet EditWork(WorkInfo work);
        ResultSet DeleteWork(Guid workId);
        ResultSet<WorkInfo> GetWorkById(Guid workId);
        Task<ResultSet<IEnumerable<WorkInfo>>> GetWorksAsync();
        Task<ResultSet<WorkInfo>> AddWorkAsync(WorkInfo work);
        Task<ResultSet<WorkUserInfo>> AddWorkUserAsync(WorkUserInfo workUser);
        Task<ResultSet> EditWorkAsync(WorkInfo work);
        Task<ResultSet> DeleteWorkAsync(Guid workId);
        Task<ResultSet<WorkInfo>> GetWorkByIdAsync(Guid workId);
        Task<ResultSet<IEnumerable<WorkInfo>>> GetWorksByUserIdAsync(Guid UserID);
    }
}
