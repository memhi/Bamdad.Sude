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
        public readonly SudeSessionContext _sudeSessionContext;

        public HeaderWorksViewComponent(SudeSessionContext sudeSessionContext)
        {
            _sudeSessionContext = sudeSessionContext;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {


            ResultSetDto<IEnumerable<WorkDetailDtoModel>> Worklist = await Api.GetHandler
       .GetApiAsync<ResultSetDto<IEnumerable<WorkDetailDtoModel>>>(ApiAddress.Work.GetWorksByUserId+_sudeSessionContext.CurrentUser.id);

             


            string CurrentWorkId = _sudeSessionContext.CurrentWorkId;
            string CurrentWorkName = _sudeSessionContext.CurrentWorkName;
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
                    _sudeSessionContext.CurrentWorkId= CurrentWorkId;
                    _sudeSessionContext.CurrentWorkName = CurrentWorkName;
                   _sudeSessionContext.CurrentWork= Worklist.Data.First();
                }

                selectLists = new SelectList(Worklist.Data as ICollection<WorkDetailDtoModel>, "WorkId", "Title", CurrentWorkId);

            }



            ViewData["Works"] = selectLists;
            ViewData["CurrentWorkName"] = CurrentWorkName;

            return View();
        }
    }
}
