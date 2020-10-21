using Sude.Domain.Models.Common;
using System;
using System.Collections.Generic;

namespace Sude.Domain.Models.Account
{
    public class RoleInfo : BaseModel
    {
        public bool IsActive { get;set; }
        public string Title { get; set; }
        public string Description { get; set; }



    }
}
