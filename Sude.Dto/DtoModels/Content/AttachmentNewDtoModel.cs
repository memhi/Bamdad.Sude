using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sude.Dto.DtoModels.Content
{
    public class AttachmentNewDtoModel
    {
        public string AttachmentId { get; set; }
        public string Title { get; set; }
        public string EntityId { get; set; }

        public string EntityTypeId { get; set; }

        public string AttachmenTypetId { get; set; }
        public string AttachmentFileAddress { get; set; }
        public string AttachmentFileType { get; set; }
        public byte[] AttachmentContent { get; set; }
    }
}
