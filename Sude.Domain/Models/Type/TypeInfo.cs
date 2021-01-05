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
 
    public class TypeInfo : BaseModel<Guid>
    {

        public string TypeKey { get; set; }
        public string TypeTitle { get; set; } 
        public Guid TypeGroupId { get; set; }
        public TypeGroupInfo TypeGroup { get; set; }
        public string  Description { get; set; }


    }

}
