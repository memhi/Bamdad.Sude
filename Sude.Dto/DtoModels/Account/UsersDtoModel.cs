
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sude.Dto.DtoModels
{
    public class UsersDtoModel
    {
        public IEnumerable<DetailUserDtoModel> Users { get; set; }
    }
}
