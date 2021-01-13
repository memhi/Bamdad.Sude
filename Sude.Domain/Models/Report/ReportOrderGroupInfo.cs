using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sude.Domain.Models.Report
{
    public class ReportOrderGroupInfo
    {
        public Guid WorkId { get; set; }
        public double SumPrice { get; set; }

        public DateTime OrderDate { get; set; }
        
        public bool IsBuy { get; set; }
        
        
    }
}
