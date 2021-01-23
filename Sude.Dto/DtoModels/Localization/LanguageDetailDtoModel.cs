using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sude.Dto.DtoModels.Serving;

namespace Sude.Dto.DtoModels.Localization
{
    public class LanguageDetailDtoModel
    {
        public string LanguageId { get; set; }
        public string Name { get; set; }      
        public string LanguageCulture { get; set; }
        public bool Rtl { get; set; }
        public int DisplayOrder { get; set; }
        public ICollection<LocalStringResourceDetailDtoModel> LocalStringResources { get; set; }

    }
}
