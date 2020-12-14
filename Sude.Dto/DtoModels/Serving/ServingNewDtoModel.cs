using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sude.Dto.DtoModels.Serving
{
    public class ServingNewDtoModel
    {
        public string ServingId { get; set; }

        [Required(ErrorMessage ="عنوان را وارد نمایید")]
        public string Title { get; set; }

        //[MaxLength(5)]
        [Required(ErrorMessage = "مبلغ را وارد نمایید")]
        [Range(0,4000000000,ErrorMessage ="بازه مبلغ را درست وارد نمایید")]
        public int Price { get; set; }

        public string Desc { get; set; }

        public bool IsActive  { get; set; }
        public string WorkId { get; set; }
        public bool HasInventoryTracking { get; set; }
    }
}
