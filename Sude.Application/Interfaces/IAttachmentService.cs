using Sude.Application.Result;
using Sude.Domain.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sude.Application.Interfaces
{
    public interface IAttachmentService
    {

        ResultSet<AttachmentInfo> AddAttachment(AttachmentInfo  Attachment);
        ResultSet EditAttachment(AttachmentInfo Attachment);
        ResultSet DeleteAttachment(Guid AttachmentId);
        ResultSet<AttachmentInfo> GetAttachmentById(Guid AttachmentId);
        Task<ResultSet<IEnumerable<AttachmentInfo>>> GetAttachmentsAsync(Guid? entityId = null, Guid? entityTypeId = null, Guid? attachmentTypeId = null);
        Task<ResultSet<AttachmentInfo>> AddAttachmentAsync(AttachmentInfo Attachment);
        Task<ResultSet> EditAttachmentAsync(AttachmentInfo Attachment);
        Task<ResultSet> DeleteAttachmentAsync(Guid AttachmentId);
        Task<ResultSet<AttachmentInfo>> GetAttachmentByIdAsync(Guid AttachmentId);

    }
}
