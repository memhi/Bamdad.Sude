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


namespace Sude.Domain.Models.Type
{
 
    public class TypeGroupInfo : BaseModel<Guid>
    { 
        public string TypeGroupKey { get; set; }             
       
        public string TypeGroupTitle { get; set; }
        
        public string  Description { get; set; }


    }

}
