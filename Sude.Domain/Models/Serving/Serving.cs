using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sude.Domain.Models.Common;

namespace Sude.Domain.Models.Serving
{
    public class Serving : BaseModel<Guid>
    {
        public string Title { get; set; }
        public int Price { get; set; }
        public string Desc { get; set; }
    }

}
