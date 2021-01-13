using Sude.Domain.Models.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sude.Domain.Interfaces
{
    public interface ILocalStringResourceRepository
    {
        Task<IEnumerable<LocalStringResourceInfo>> GetLocalStringResourcesAsync();
        IEnumerable<LocalStringResourceInfo> GetLocalStringResources();
        Task<IEnumerable<LocalStringResourceInfo>> GetLocalStringResourcesByLanguageIdAsync(Guid languageId);

        bool AddLocalStringResource(LocalStringResourceInfo LocalStringResource);
        bool EditLocalStringResource(LocalStringResourceInfo LocalStringResource);
        bool DeleteLocalStringResource(Guid LocalStringResourceId);

        Task<LocalStringResourceInfo> GetLocalStringResourceByIdAsync(Guid LocalStringResourceId);
 
        LocalStringResourceInfo GetLocalStringResourceById(Guid LocalStringResourceId);
       

        void Save();
        Task SaveAsync();
    }
}
