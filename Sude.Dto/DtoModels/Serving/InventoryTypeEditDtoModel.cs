using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sude.Dto.DtoModels.Serving
{
    public class InventoryTypeEditDtoModel
    {
        public string InventoryTypeId { get; set; }

        [Required(ErrorMessage = "عنوان را وارد نمایید")]
        public string Title { get; set; }



        public string Description { get; set; }

    }
}
