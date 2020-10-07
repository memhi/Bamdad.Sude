using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sude.Dto.DtoModels.Serving
{
    public class ServingEditDtoModel
    {
        public string ServingId { get; set; }

        [Required(ErrorMessage ="عنوان را وارد نمایید")]
        public string Title { get; set; }

        [Range(100, 100000,
            ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int Price { get; set; }

        public string Desc { get; set; }
    }
}
