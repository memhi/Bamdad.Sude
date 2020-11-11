using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sude.Dto.DtoModels.Order;
using Sude.Dto.DtoModels.Result;
using Sude.Dto.DtoModels.Serving;
using Sude.Dto.DtoModels.Work;
using Sude.Mvc.UI.ApiManagement;

namespace Sude.Mvc.UI.Components
{
    public class AddDetailViewComponent: ViewComponent
    {


        public AddDetailViewComponent()
        {
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {

    
            string CurrentWorkId = HttpContext.Session.GetString("CurrentWorkId");



            ResultSetDto<IEnumerable<ServingDetailDtoModel>> servinglist = await Api.GetHandler
      .GetApiAsync<ResultSetDto<IEnumerable<ServingDetailDtoModel>>>(ApiAddress.Serving.GetServingsByWorkId + CurrentWorkId);

            SelectList selectLists = new SelectList(servinglist.Data as ICollection<ServingDetailDtoModel>, "ServingId", "Title", CurrentWorkId);
            ViewData["Servings"] = selectLists;

            IEnumerable<OrderDetailNewDtoModel> orderDetailNewDtos = HttpContext.Session.GetObject<IEnumerable<OrderDetailNewDtoModel>>("OrderDetails");

            ViewData["OrderDetails"] = orderDetailNewDtos;

            return View();
        }
    }
}
