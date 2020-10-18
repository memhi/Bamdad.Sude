using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sude.Domain.Models.Common;
using Sude.Domain.Models.Work;

namespace Sude.Domain.Models.Serving
{
    public class ServingInfo : BaseModel<Guid>
    {
        public string Title { get; set; }
        public int Price { get; set; }
        public string Desc { get; set; }
        public WorkInfo Work { get; set; }

    }

}