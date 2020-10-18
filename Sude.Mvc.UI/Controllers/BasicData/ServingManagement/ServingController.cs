using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Sude.Dto.DtoModels.Result;
using Sude.Dto.DtoModels.Serving;
using Sude.Mvc.UI.ApiManagement;

namespace Sude.Mvc.UI.Controllers.BasicData.ServingManagement
{
    public class ServingController : Controller
    {
        // GET: ServingController
        [HttpGet]
        [Authorize]
        public  ActionResult Index()
        {
            //ResultSetDto<IEnumerable<ServingDetailDtoModel>> servinglist = await Api.GetHandler
            //             .GetApiAsync<ResultSetDto<IEnumerable<ServingDetailDtoModel>>>(ApiAddress.GetServings);

            return View();
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> List()
        {
            //System.Threading.Thread.Sleep(1000);

            ResultSetDto<IEnumerable<ServingDetailDtoModel>> servinglist = await Api.GetHandler
                .GetApiAsync<ResultSetDto<IEnumerable<ServingDetailDtoModel>>>(ApiAddress.GetServings);

            return PartialView("ServingList", servinglist);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return PartialView();
        }

        [HttpPost]
        public async Task<ActionResult> Add(ServingNewDtoModel request)
        {
            if (!ModelState.IsValid)
            {
                string message = "";
                foreach (var er in ModelState.Values.SelectMany(modelstate => modelstate.Errors))
                    message += er.ErrorMessage + " \n";

                return Json(new ResultSetDto()
                {
                    IsSucceed = false,
                    Message = message
                });
            }

            ResultSetDto<ServingNewDtoModel> result = await Api.GetHandler
                .GetApiAsync<ResultSetDto<ServingNewDtoModel>>(ApiAddress.AddServing, request);


            return Json(result);

        }

        [HttpGet]//("{servingId}")]
        public async Task<ActionResult> Edit(string id)
        {
            ResultSetDto<ServingDetailDtoModel> result = await Api.GetHandler
                .GetApiAsync<ResultSetDto<ServingDetailDtoModel>>(ApiAddress.GetServingById + id);

            return PartialView(viewName: "Edit", model: new ServingEditDtoModel()
            {
                ServingId = result.Data.ServingId,
                Title = result.Data.Title,
                Price = result.Data.Price,
                Desc = result.Data.Desc
            });
        }
        //[Route("Edit/{request}")]
        [HttpPost]
        public async Task<ActionResult> Edit(ServingEditDtoModel request)
        {
           if (!ModelState.IsValid)
            {

                string message = "";
                foreach (var er in ModelState.Values.SelectMany(modelstate => modelstate.Errors))
                    message += er.ErrorMessage + " \n";

                return Ok(new ResultSetDto()
                {
                    IsSucceed = false,
                    Message = message
                });
            }

            ResultSetDto<ServingEditDtoModel> result = await Api.GetHandler
                .GetApiAsync<ResultSetDto<ServingEditDtoModel>>(ApiAddress.EditServing, request);

            return Json(result);

        }

        public async Task<ActionResult> Detail(string id)
        {
            ResultSetDto<ServingDetailDtoModel> result = await Api.GetHandler
             .GetApiAsync<ResultSetDto<ServingDetailDtoModel>>(ApiAddress.GetServingById + id);

            var servingDetail = result.Data;

            return View(viewName: "Detail", model: servingDetail);
        }


        public async Task<ActionResult> Delete(string id)
        {
            ResultSetDto result = await Api.GetHandler
              .GetApiAsync<ResultSetDto>(ApiAddress.DeleteServing, id);

            return Json(result);
        }
    }
}
