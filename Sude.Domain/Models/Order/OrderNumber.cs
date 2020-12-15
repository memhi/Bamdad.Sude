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
using Sude.Domain.Models.Work;

namespace Sude.Domain.Models.Order
{
 
    public class OrderNumberInfo : BaseModel<Guid>
    { 
       
       public int LastNumber { get; set; }
        public string  Year { get; set; }       
        public bool IsBuy { get; set; }
        public Guid WorkId { get; set; }
        public WorkInfo Work { get; set; }
    
    }

}
