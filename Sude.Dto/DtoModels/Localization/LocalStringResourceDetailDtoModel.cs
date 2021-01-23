using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sude.Dto.DtoModels.Localization
{
    public class LocalStringResourceDetailDtoModel
    {
        public string LocalStringResourceId { get; set; }
        public string LanguageId { get; set; }
        public string ResourceName { get; set; }      
        public string ResourceValue { get; set; }
     

    }
}
