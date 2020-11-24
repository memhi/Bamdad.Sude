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
    public class NewsCommentInfo : BaseModel<Guid>
    {

        public string Title { get; set; }
        public string Comment { get; set; }

        public Guid NewsId { get; set; }
        public virtual NewsInfo Blog { get; set; }
        public bool IsApproved { get; set; }
        public Guid? NewsCommentId { get; set; }
        public virtual NewsCommentInfo NEwsComment { get; set; }
        public Guid? UserId { get; set; }


    }

}
