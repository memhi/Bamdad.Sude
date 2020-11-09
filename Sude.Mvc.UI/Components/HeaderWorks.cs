using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sude.Dto.DtoModels.Result;
using Sude.Dto.DtoModels.Work;
using Sude.Mvc.UI.ApiManagement;

namespace Sude.Mvc.UI.Components
{
    public class HeaderWorksViewComponent: ViewComponent
    {


        public HeaderWorksViewComponent()
        {
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {

            ResultSetDto<IEnumerable<WorkDetailDtoModel>> Worklist = await Api.GetHandler
       .GetApiAsync<ResultSetDto<IEnumerable<WorkDetailDtoModel>>>(ApiAddress.Work.GetWorks);

            string CurrentWorkId = HttpContext.Session.GetString("CurrentWorkId");
            if (string.IsNullOrEmpty(CurrentWorkId))
                CurrentWorkId = "";
            SelectList selectLists = new SelectList(Worklist.Data as ICollection<WorkDetailDtoModel>, "WorkId", "Title",CurrentWorkId);
            ViewData["Works"] = selectLists;


            return View();
        }
    }
}
