using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sude.Dto.DtoModels.Result
{
    public class ResultSetDto
    {
        public bool IsSuced { get; set; } = true;
        public string Message { get; set; } = "";
    }

    public class ResultSetDto<T> : ResultSetDto
    {
        public T Data { get; set; }
    }
}
