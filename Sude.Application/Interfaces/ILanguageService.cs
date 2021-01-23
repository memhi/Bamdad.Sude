using Sude.Application.Result;
using Sude.Domain.Models.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sude.Application.Interfaces
{
    public interface ILanguageService
    {

        Task<ResultSet<IEnumerable<LanguageInfo>>> GetLanguagesAsync();   
     
        Task<ResultSet<LanguageInfo>> GetLanguageByIdAsync(Guid LanguageId);

        Task<ResultSet<IEnumerable<LocalStringResourceInfo>>> GetLocalStringResourcesAsync(Guid LanguageId);

        Task<ResultSet<IEnumerable<LocalStringResourceInfo>>> GetLocalStringResourcesAsync();





    }
}
