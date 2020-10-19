using Sude.Application.Result;
using Sude.Domain.Models.Serving;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sude.Application.Interfaces
{
    public interface IServingService
    {
        ResultSet<IEnumerable<ServingInfo>> GetServings();
        ResultSet<ServingInfo> AddServing(ServingInfo request);
        ResultSet EditServing(ServingInfo request);
        ResultSet DeleteServing(Guid servingId);
        ResultSet<ServingInfo> GetServingById(Guid servingId);
        Task<ResultSet<IEnumerable<ServingInfo>>> GetServingsAsync();
        Task<ResultSet<ServingInfo>> AddServingAsync(ServingInfo request);
        Task<ResultSet> EditServingAsync(ServingInfo request);
        Task<ResultSet> DeleteServingAsync(Guid servingId);
        Task<ResultSet<ServingInfo>> GetServingByIdAsync(Guid servingId);
    }
}
