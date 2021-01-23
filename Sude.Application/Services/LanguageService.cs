using Sude.Application.Interfaces;
using Sude.Domain.Models.Localization;
using Sude.Common;
using Sude.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sude.Application.Result;

namespace Sude.Application.Services
{
    public class LanguageService : ILanguageService
    {
        private ILanguageRepository _LanguageRepository;
        private ILocalStringResourceRepository _LocalStringResourceRepository;
        public LanguageService(ILanguageRepository languageRepository, ILocalStringResourceRepository localStringResourceRepository)
        {
            this._LanguageRepository = languageRepository;
            this._LocalStringResourceRepository = localStringResourceRepository;
        }


  
        public async Task<ResultSet<IEnumerable<LanguageInfo>>> GetLanguagesAsync()
        {
            return new ResultSet<IEnumerable<LanguageInfo>>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = await _LanguageRepository.GetLanguagesAsync()
                
            };
           
        }

         
        public async Task<ResultSet<LanguageInfo>> GetLanguageByIdAsync(Guid languageId)
        {
            LanguageInfo  language   = await _LanguageRepository.GetLanguageByIdAsync(languageId);
            
            if (language == null)
                return new ResultSet<LanguageInfo>()
                {
                    IsSucceed = false,
                    Message = "اطلاعات زبان با این شناسه پیدا نشد",
                    Data = null
                };

            return new ResultSet<LanguageInfo>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = new LanguageInfo()
                {
                    Id = language.Id,
                     Rtl=language.Rtl,
                      DisplayOrder=language.DisplayOrder,
                       LanguageCulture=language.LanguageCulture,
                       Name=language.Name,
                        Published=language.Published 

                }
            };

        }

        public async Task<ResultSet<IEnumerable<LocalStringResourceInfo>>> GetLocalStringResourcesAsync(Guid languageId)
        {
            IEnumerable<LocalStringResourceInfo>  localStringResources  = await _LocalStringResourceRepository.GetLocalStringResourcesByLanguageIdAsync(languageId);

            if (localStringResources == null)
                return new ResultSet<IEnumerable<LocalStringResourceInfo>>()
                {
                    IsSucceed = false,
                    Message = "اطلاعات زبان با این شناسه پیدا نشد",
                    Data = null
                };

            return new ResultSet<IEnumerable<LocalStringResourceInfo>>
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = localStringResources
            };

        }


        public async Task<ResultSet<IEnumerable<LocalStringResourceInfo>>> GetLocalStringResourcesAsync()
        {
            IEnumerable<LocalStringResourceInfo> localStringResources = await _LocalStringResourceRepository.GetLocalStringResourcesAsync();

            if (localStringResources == null)
                return new ResultSet<IEnumerable<LocalStringResourceInfo>>()
                {
                    IsSucceed = false,
                    Message = "اطلاعات پیدا نشد",
                    Data = null
                };

            return new ResultSet<IEnumerable<LocalStringResourceInfo>>
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = localStringResources
            };

        }







    }
}
