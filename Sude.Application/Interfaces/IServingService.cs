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
   //     ResultSet<IEnumerable<ServingInfo>> GetServings();
     //   ResultSet<ServingInfo> AddServing(ServingInfo  serving);

        Task<ResultSet<IEnumerable<ServingInfo>>> GetServingsByWorkIdAsync(Guid workId);
        Task<ResultSet<IEnumerable<ServingInfo>>> GetServingsByWorkIdAndHasTrackingAsync(Guid workId);
     //   ResultSet EditServing(ServingInfo   serving);
     //  ResultSet DeleteServing(Guid servingId);
     //    ResultSet<ServingInfo> GetServingById(Guid servingId);
        Task<ResultSet<IEnumerable<ServingInfo>>> GetServingsAsync();
        Task<ResultSet<ServingInfo>> AddServingAsync(ServingInfo  serving);
        Task<ResultSet> EditServingAsync(ServingInfo  serving);
        Task<ResultSet> DeleteServingAsync(Guid servingId);
        Task<ResultSet<ServingInfo>> GetServingByIdAsync(Guid servingId);
    }
}
