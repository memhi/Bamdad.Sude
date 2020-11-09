using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sude.Dto.DtoModels.Result
{
    public class ResultSetDto
    {
        public bool IsSucceed { get; set; } = true;
        public string Message { get; set; } = "";
    }

    public class ResultSetDto<T> : ResultSetDto
    {
        public int RowCount { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public T Data { get; set; }
    }
}
