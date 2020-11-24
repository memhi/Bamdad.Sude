using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using Sude.Dto.DtoModels.Result;
using Sude.Dto.DtoModels.Serving;
using Sude.Dto.DtoModels.Work;
using Sude.Mvc.UI.ApiManagement;

namespace Sude.Mvc.UI.Admin.Controllers.BasicData.ServingManagement
{
    public class ServingController : BaseAdminController
    {
        // GET: ServingController
        [HttpGet]
     //   [Authorize]
        public  ActionResult Index()
        {
            //ResultSetDto<IEnumerable<ServingDetailDtoModel>> servinglist = await Api.GetHandler
            //             .GetApiAsync<ResultSetDto<IEnumerable<ServingDetailDtoModel>>>(ApiAddress.GetServings);

            return View();
        }

        [HttpGet]
      //  [Authorize]
        public async Task<ActionResult> List()
        {
          
           
            ResultSetDto<IEnumerable<ServingDetailDtoModel>> servinglist = await Api.GetHandler
                .GetApiAsync<ResultSetDto<IEnumerable<ServingDetailDtoModel>>>(ApiAddress.Serving.GetServings);

            return PartialView("ServingList", servinglist);
        }

        [HttpGet]
        public async Task<ActionResult> Add()
        { 
            ResultSetDto<IEnumerable<WorkDetailDtoModel>> Worklist = await Api.GetHandler
           .GetApiAsync<ResultSetDto<IEnumerable<WorkDetailDtoModel>>>(ApiAddress.Work.GetWorks);
            ServingNewDtoModel  servingNewDtoModel  = new ServingNewDtoModel();

            servingNewDtoModel.Desc = "";
            servingNewDtoModel.Title = "";
            servingNewDtoModel.WorkId = "";
            servingNewDtoModel.HasInventoryTracking = false;
            servingNewDtoModel.IsActive = true;
            SelectList selectLists = new SelectList(Worklist.Data as ICollection<Sude.Dto.DtoModels.Work.WorkDetailDtoModel>, "WorkId", "Title");

            ViewData["Works"] = selectLists;

            return PartialView("Add", servingNewDtoModel);

           
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
                .GetApiAsync<ResultSetDto<ServingNewDtoModel>>(ApiAddress.Serving.AddServing, request);


            return Json(result);

        }

        [HttpGet]//("{servingId}")]
        public async Task<ActionResult> Edit(string id)
        {
            ResultSetDto<ServingDetailDtoModel> result = await Api.GetHandler
                .GetApiAsync<ResultSetDto<ServingDetailDtoModel>>(ApiAddress.Serving.GetServingById + id);

            ResultSetDto<IEnumerable<WorkDetailDtoModel>> Worklist = await Api.GetHandler
          .GetApiAsync<ResultSetDto<IEnumerable<WorkDetailDtoModel>>>(ApiAddress.Work.GetWorks);
         
            SelectList selectLists = new SelectList(Worklist.Data as ICollection<Sude.Dto.DtoModels.Work.WorkDetailDtoModel>, "WorkId", "Title",result.Data.WorkId);

            ViewData["Works"] = selectLists;




            return PartialView(viewName: "Edit", model: new ServingEditDtoModel()
            {
                ServingId = result.Data.ServingId,
                Title = result.Data.Title,
                Price = result.Data.Price,
                Desc = result.Data.Desc,
                 WorkId=result.Data.WorkId,
                  IsActive=result.Data.IsActive,
                   HasInventoryTracking=result.Data.HasInventoryTracking
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
                .GetApiAsync<ResultSetDto<ServingEditDtoModel>>(ApiAddress.Serving.EditServing, request);

            return Json(result);

        }

        public async Task<ActionResult> Detail(string id)
        {
            ResultSetDto<ServingDetailDtoModel> result = await Api.GetHandler
             .GetApiAsync<ResultSetDto<ServingDetailDtoModel>>(ApiAddress.Serving.GetServingById + id);

            var servingDetail = result.Data;

            return View(viewName: "Detail", model: servingDetail);
        }


        public async Task<ActionResult> Delete(string id)
        {
            ResultSetDto result = await Api.GetHandler
              .GetApiAsync<ResultSetDto>(ApiAddress.Serving.DeleteServing, id);

            return Json(result);
        }
    }
}
