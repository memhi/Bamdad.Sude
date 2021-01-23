using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using IdentityModel.Client;
using Org.BouncyCastle.Asn1;
using Sude.Dto.DtoModels.Localization;
using Sude.Dto.DtoModels.Result;
using Sude.Mvc.UI.ApiManagement;

namespace Sude.Mvc.UI
{
    public class LanguageResources
    {

        public ICollection<LanguageDetailDtoModel> Languages;
        public ICollection<LocalStringResourceDetailDtoModel> LocalStringResources;
        private readonly SudeSessionContext _SudeSessionContext;
        public LanguageResources()
        {
            if (Languages == null)
            {
              var result =  Api.GetHandler
           .GetApiAsync<ResultSetDto<IEnumerable<LanguageDetailDtoModel>>>(ApiAddress.Localization.GetAllLanguages);
                if (result.Result.IsSucceed && result.Result.Data != null)
                {
                    Languages = result.Result.Data.ToList();
                }
            }

            if (LocalStringResources == null)
            {
               var result =  Api.GetHandler
           .GetApiAsync<ResultSetDto<IEnumerable<LocalStringResourceDetailDtoModel>>>(ApiAddress.Localization.GetAllLocalResources);
                if (result.Result.IsSucceed && result.Result.Data != null)
                {
                    LocalStringResources = result.Result.Data.ToList();
                }
            }
        }

        public async Task<string> GetLocalResourceValue(string Name)
        {
          

            LocalStringResourceDetailDtoModel localStringResource = LocalStringResources.Where(lr => lr.LanguageId == _SudeSessionContext.CurrentLanguage.LanguageId && lr.ResourceName == Name).FirstOrDefault();
            if (localStringResource == null)
                return Name;
            return localStringResource.ResourceValue;

        }
      
    }
}
