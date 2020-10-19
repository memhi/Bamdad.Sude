using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Sude.Dto.DtoModels.Result;
using Sude.Dto.DtoModels.Work;
using Sude.Mvc.UI.ApiManagement;

namespace Sude.Mvc.UI.Controllers.BasicData.WorkTypeManagement
{
    public class WorkTypeController : Controller
    {
        // GET: WorkTypeController
        [HttpGet]
     //   [Authorize]
        public  ActionResult Index()
        {
            //ResultSetDto<IEnumerable<WorkTypeDetailDtoModel>> WorkTypelist = await Api.GetHandler
            //             .GetApiAsync<ResultSetDto<IEnumerable<WorkTypeDetailDtoModel>>>(ApiAddress.GetWorkTypes);

            return View();
        }

        [HttpGet]
       // [Authorize]
        public async Task<ActionResult> List()
        {
            //System.Threading.Thread.Sleep(1000);

            ResultSetDto<IEnumerable<WorkTypeDetailDtoModel>> WorkTypelist = await Api.GetHandler
                .GetApiAsync<ResultSetDto<IEnumerable<WorkTypeDetailDtoModel>>>(ApiAddress.WorkType.GetWorkTypes);

            return PartialView("WorkTypeList", WorkTypelist);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return PartialView();
        }

        [HttpPost]
        public async Task<ActionResult> Add(WorkTypeNewDtoModel request)
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

            ResultSetDto<WorkTypeNewDtoModel> result = await Api.GetHandler
                .GetApiAsync<ResultSetDto<WorkTypeNewDtoModel>>(ApiAddress.WorkType.AddWorkType, request);


            return Json(result);

        }

        [HttpGet]//("{WorkTypeId}")]
        public async Task<ActionResult> Edit(string id)
        {
            ResultSetDto<WorkTypeDetailDtoModel> result = await Api.GetHandler
                .GetApiAsync<ResultSetDto<WorkTypeDetailDtoModel>>(ApiAddress.WorkType.GetWorkTypeById + id);

            return PartialView(viewName: "Edit", model: new WorkTypeEditDtoModel()
            {
                WorkTypeId = result.Data.WorkTypeId,
                Title = result.Data.Title,
         
                Desc = result.Data.Desc
            });
        }
        //[Route("Edit/{request}")]
        [HttpPost]
        public async Task<ActionResult> Edit(WorkTypeEditDtoModel request)
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

            ResultSetDto<WorkTypeEditDtoModel> result = await Api.GetHandler
                .GetApiAsync<ResultSetDto<WorkTypeEditDtoModel>>(ApiAddress.WorkType.EditWorkType, request);

            return Json(result);

        }

        public async Task<ActionResult> Detail(string id)
        {
            ResultSetDto<WorkTypeDetailDtoModel> result = await Api.GetHandler
             .GetApiAsync<ResultSetDto<WorkTypeDetailDtoModel>>(ApiAddress.WorkType.GetWorkTypeById + id);

            var WorkTypeDetail = result.Data;

            return View(viewName: "Detail", model: WorkTypeDetail);
        }


        public async Task<ActionResult> Delete(string id)
        {
            ResultSetDto result = await Api.GetHandler
              .GetApiAsync<ResultSetDto>(ApiAddress.WorkType.DeleteWorkType, id);

            return Json(result);
        }
    }
}
