using System;
using System.Collections.Generic; 
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sude.Domain.Models.Common;

namespace Sude.Domain.Models.Work
{
    public class WorkInfo : BaseModel<Guid>
    {
 
        public string Title { get; set; }       
        public string Desc { get; set; }
        public WorkTypeInfo WorkType { get; set; }
    }

}
