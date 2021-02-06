using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sude.Dto.DtoModels.Common;
using Sude.Dto.DtoModels.Content;

namespace Sude.Dto.DtoModels.Work
{
    public class WorkDetailDtoModel
    {
        public string WorkId { get; set; }
        public string WorkTypeId { get; set; }
        public string WorkTypeName { get; set; }
        public string Title { get; set; }      
        public string Desc { get; set; }
        public List<AttachmentNewDtoModel> Attachments { get; set; }
        public AddressDtoModel Address { get; set; }

    }
}
