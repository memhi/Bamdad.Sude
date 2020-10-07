using Sude.Domain.Models.Serving;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sude.Domain.Interfaces
{
    public interface IServingRepository
    {
        Task<IEnumerable<Serving>> GetServingsAsync();
        IEnumerable<Serving> GetServings();
        bool IsExistServing(string Title);

        bool AddServing(Serving serving);
        bool EditServing(Serving serving);
        bool DeleteServing(Guid servingId);

        Task<Serving> GetServingByIdAsync(Guid servingId);
        Serving GetServingById(Guid servingId);
       

        void Save();
        Task SaveAsync();
    }
}
