using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Sude.Dto.DtoModels.Result;
using Sude.Dto.DtoModels.Serving;
using Sude.Mvc.UI.ApiManagement;

namespace Sude.Mvc.UI.ViewComponents
{
    public class EditServing : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(string servingId)
        {
            ResultSetDto<ServingDetailDtoModel> result = await Api.GetHandler
                .GetApiAsync<ResultSetDto<ServingDetailDtoModel>>(ApiAddress.GetServingById+ servingId);
     
            return View(viewName:"Edit",model: result.Data);
        }

   
    }
}
