using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sude.Dto.DtoModels.Work
{
    public class WorkTypeEditDtoModel
    {
        public string WorkTypeId { get; set; }

        [Required(ErrorMessage ="عنوان را وارد نمایید")]
        public string Title { get; set; }
 
        public string Desc { get; set; }
 
    }
}
