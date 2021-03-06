﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sude.Dto.DtoModels.Common;
using Sude.Dto.DtoModels.Content;

namespace Sude.Dto.DtoModels.Work
{
    public class WorkEditDtoModel
    {
        public string WorkId { get; set; }
        public string WorkTypeId { get; set; }
        public string UserId { get; set; }
        [Required(ErrorMessage ="عنوان را وارد نمایید")]
        public string Title { get; set; }
 
        public string Desc { get; set; }

        public List<AttachmentNewDtoModel> Attachments { get; set; }
        public AddressDtoModel Address { get; set; }


    }
}
