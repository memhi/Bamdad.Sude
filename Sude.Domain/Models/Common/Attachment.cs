using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sude.Domain.Models.Type;

namespace Sude.Domain.Models.Common
{
    public class AttachmentInfo : BaseModel<Guid>
    {
        
        public string Title { get; set; }    
        public string Desc { get; set; }
        public Guid EntityId { get; set; }
        public Guid EntityTypeId { get; set; }
        public virtual TypeInfo EntityType { get; set; }
        public Guid? AttachmentTypeId { get; set; }
        public virtual TypeInfo AttachmentType { get; set; }
        public string AttachmentFileAddress  { get; set; }
        public string AttachmentFileType { get; set; }
        public byte[] AttachmentContent { get; set; }


    }

}
