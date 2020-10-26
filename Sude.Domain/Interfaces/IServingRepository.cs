using Sude.Domain.Models.Serving;
using Sude.Domain.Models.Work;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sude.Domain.Interfaces
{
    public interface IServingRepository
    {
        Task<IEnumerable<ServingInfo>> GetServingsAsync();
        IEnumerable<ServingInfo> GetServings();
        bool IsExistServing(string title, Guid? id, Guid workId);

        bool AddServing(ServingInfo serving);
        bool EditServing(ServingInfo serving);
        bool DeleteServing(Guid servingId);

        Task<ServingInfo> GetServingByIdAsync(Guid servingId);
        ServingInfo GetServingById(Guid servingId);

    


        void Save();
        Task SaveAsync();
    }
}
