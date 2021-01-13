using Sude.Domain.Models.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sude.Domain.Interfaces
{
    public interface ILanguageRepository
    {
        Task<IEnumerable<LanguageInfo>> GetLanguagesAsync();
        IEnumerable<LanguageInfo> GetLanguages();
 

        bool AddLanguage(LanguageInfo Language);
        bool EditLanguage(LanguageInfo Language);
        bool DeleteLanguage(Guid LanguageId);

        Task<LanguageInfo> GetLanguageByIdAsync(Guid LanguageId);
 
        LanguageInfo GetLanguageById(Guid LanguageId);
       

        void Save();
        Task SaveAsync();
    }
}
