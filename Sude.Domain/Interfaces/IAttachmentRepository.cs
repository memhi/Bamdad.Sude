using Sude.Domain.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sude.Domain.Interfaces
{
    public interface IAttachmentRepository
    {
       
        void AddAttachment(AttachmentInfo attachment);

        Task<IEnumerable<AttachmentInfo>> GetAttachmentsAsync(Guid? entityId = null, Guid? entityTypeId = null, Guid? attachmentTypeId = null);

        AttachmentInfo GetAttachmentById(Guid aAttachmentId);
        Task<AttachmentInfo> GetAttachmentByIdAsync(Guid attachmentId);
      
 

        bool EditAttachment(AttachmentInfo attachment);
        bool DeleteAttachment(Guid attachmentId);
        void Save();

        Task SaveAsync();
    }
}
