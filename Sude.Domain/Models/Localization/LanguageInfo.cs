using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Sude.Domain.Models.Common;
using Sude.Domain.Models.Serving;


namespace Sude.Domain.Models.Localization
{
 
    public class LanguageInfo : BaseModel<Guid>
    {

        public string Name { get; set; }
        public string LanguageCulture { get; set; } 
        public bool Rtl { get; set; }
        public int DisplayOrder { get; set; }
        public bool Published { get; set; }
        public virtual ICollection<LocalStringResourceInfo>  LocalStringResources { get; set; }

    }

}
