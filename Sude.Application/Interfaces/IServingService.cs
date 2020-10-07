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
        ResultSet<IEnumerable<Serving>> GetServings();
        ResultSet<Serving> AddServing(Serving request);
        ResultSet EditServing(Serving request);
        ResultSet DeleteServing(Guid servingId);
        ResultSet<Serving> GetServingById(Guid servingId);
        Task<ResultSet<IEnumerable<Serving>>> GetServingsAsync();
        Task<ResultSet<Serving>> AddServingAsync(Serving request);
        Task<ResultSet> EditServingAsync(Serving request);
        Task<ResultSet> DeleteServingAsync(Guid servingId);
        Task<ResultSet<Serving>> GetServingByIdAsync(Guid servingId);
    }
}
