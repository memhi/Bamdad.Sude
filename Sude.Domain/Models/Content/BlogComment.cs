using System;
using System.Collections.Generic;
 
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Sude.Domain.Models.Common;
using Sude.Domain.Models.Work;

namespace Sude.Domain.Models.Content
{
    public class BlogCommentInfo : BaseModel<Guid>
    {

        public string Title { get; set; }
        public string Comment { get; set; }
        
        public Guid BlogId { get; set; }
        public virtual BlogInfo Blog { get; set; }
        public bool IsApproved { get; set; }
        public Guid? BlogCommentId { get; set; }
        public virtual BlogCommentInfo BlogComment  { get; set; }
        public Guid? UserId { get; set; }


    }

}
