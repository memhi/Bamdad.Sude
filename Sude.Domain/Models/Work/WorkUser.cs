using Sude.Domain.Models.Common;
using System;
using System.Collections.Generic;

namespace Sude.Domain.Models.Work
{
    public class WorkUserInfo : BaseModel
    {
        public Guid UserId { get; set; }
        public Guid WorkId { get;set; }
        public WorkInfo Work { get; set; }
    
 



    }
}
