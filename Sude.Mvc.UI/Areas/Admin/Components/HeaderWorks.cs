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
         
            string CurrentWorkId = _sudeSessionContext.CurrentWorkId;
            string CurrentWorkName = _sudeSessionContext.CurrentWorkName;
            if (_sudeSessionContext.UserWorks == null || !_sudeSessionContext.UserWorks.Any() || !_sudeSessionContext.UserWorks.Any(w => w.WorkId == CurrentWorkId))
            {
                CurrentWorkId = "";
                CurrentWorkName = "";
                _sudeSessionContext.CurrentWorkId = null;
                _sudeSessionContext.CurrentWorkName = null;
                _sudeSessionContext.CurrentWork = null;
            }


            if (string.IsNullOrEmpty(CurrentWorkId))
            {
                CurrentWorkId = "";
                CurrentWorkName = "یک کسب و کار انتخاب کنید";
            }
            SelectList selectLists = null;
            if (_sudeSessionContext.UserWorks != null)
            {

                if (_sudeSessionContext.UserWorks.Count() == 1)
                {
                    CurrentWorkId = _sudeSessionContext.UserWorks.First().WorkId;
                    CurrentWorkName = _sudeSessionContext.UserWorks.First().Title;
                    _sudeSessionContext.CurrentWorkId = CurrentWorkId;
                    _sudeSessionContext.CurrentWorkName = CurrentWorkName;
                    _sudeSessionContext.CurrentWork = _sudeSessionContext.UserWorks.First();
                }

                selectLists = new SelectList(_sudeSessionContext.UserWorks as ICollection<WorkDetailDtoModel>, "WorkId", "Title", CurrentWorkId);

            }



            ViewData[Constants.ViewBagNames.Works] = selectLists;
            ViewData[Constants.ViewBagNames.CurrentWorkName] = CurrentWorkName;

            return View();
        }
    }
}
