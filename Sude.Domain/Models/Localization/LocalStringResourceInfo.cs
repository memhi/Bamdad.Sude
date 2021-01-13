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
 
    public class LocalStringResourceInfo : BaseModel<Guid>
    {

        public string ResourceName { get; set; }
        public string ResourceValue { get; set; } 
        public Guid LanguageId { get; set; }
        public virtual LanguageInfo Language { get; set; }
    }

}
