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

namespace Sude.Mvc.UI.Admin.Components
{
    public class HeaderWorksViewComponent : ViewComponent
    {


        public HeaderWorksViewComponent()
        {
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {

            ResultSetDto<IEnumerable<WorkDetailDtoModel>> Worklist = await Api.GetHandler
       .GetApiAsync<ResultSetDto<IEnumerable<WorkDetailDtoModel>>>(ApiAddress.Work.GetWorks);

            string CurrentWorkId = HttpContext.Session.GetString(Constants.SessionNames.CurrentWorkId);
            string CurrentWorkName = HttpContext.Session.GetString(Constants.SessionNames.CurrentWorkName);
            if (string.IsNullOrEmpty(CurrentWorkId))
            {
                CurrentWorkId = "";
                CurrentWorkName = "یک کسب و کار انتخاب کنید";
            }
            SelectList selectLists = null;
            if (Worklist != null && Worklist.Data != null)
            {
               
                if (Worklist.Data.Count() == 1)
                {
                    CurrentWorkId = Worklist.Data.First().WorkId;
                    CurrentWorkName = Worklist.Data.First().Title;
                    HttpContext.Session.SetString(Constants.SessionNames.CurrentWorkId, CurrentWorkId);
                    HttpContext.Session.SetString(Constants.SessionNames.CurrentWorkName, CurrentWorkName);
                }

                selectLists = new SelectList(Worklist.Data as ICollection<WorkDetailDtoModel>, "WorkId", "Title", CurrentWorkId);

            }



            ViewData["Works"] = selectLists;
            ViewData["CurrentWorkName"] = CurrentWorkName;

            return View();
        }
    }
}
