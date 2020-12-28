using Sude.Domain.Models.Common;
using Sude.Domain.Models.Work;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sude.Domain.Interfaces
{
    public interface IWorkUserRepository
    {
        Task<IEnumerable<WorkUserInfo>> GetWorkUsersAsync();
        IEnumerable<WorkUserInfo> GetWorkUsers();
        Task<IEnumerable<WorkUserInfo>> GetWorkUsersAsync(Guid? WorkId, Guid UserId);

        bool AddWorkUser(WorkUserInfo WorkUser);
        bool EditWorkUser(WorkUserInfo WorkUser);
        bool DeleteWorkUser(Guid WorkUserId);

        Task<WorkUserInfo> GetWorkUserByIdAsync(Guid WorkUserId);
        WorkUserInfo GetWorkUserById(Guid WorkUserId);
       
        void Save();
        Task SaveAsync();
    }
}
