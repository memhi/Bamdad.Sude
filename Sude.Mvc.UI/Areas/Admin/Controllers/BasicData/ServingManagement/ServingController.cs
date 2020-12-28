using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        public readonly SudeSessionContext _sudeSessionContext;
        public ServingController(SudeSessionContext sudeSessionContext)

        {

            _sudeSessionContext = sudeSessionContext;
        }
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

            string CurrentWorkId = _sudeSessionContext.CurrentWorkId;
            ResultSetDto<IEnumerable<ServingDetailDtoModel>> servinglist = new ResultSetDto<IEnumerable<ServingDetailDtoModel>>();
            if (!string.IsNullOrEmpty(CurrentWorkId ))
                servinglist = await Api.GetHandler
                .GetApiAsync<ResultSetDto<IEnumerable<ServingDetailDtoModel>>>(ApiAddress.Serving.GetServingsByWorkId+CurrentWorkId);

            return PartialView("ServingList", servinglist);
        }

        [HttpGet]
        public async Task<ActionResult> Add()
        {

            WorkDetailDtoModel CurrentWork = _sudeSessionContext.CurrentWork;
         
            
            ServingNewDtoModel  servingNewDtoModel  = new ServingNewDtoModel();

            servingNewDtoModel.Desc = "";
            servingNewDtoModel.Title = "";
            if (CurrentWork!=null)
            {
                servingNewDtoModel.WorkId = CurrentWork.WorkId;
                ViewData[Constants.ViewBagNames.CurrentWorkName] = CurrentWork.Title;
            }
            servingNewDtoModel.HasInventoryTracking = false;
            servingNewDtoModel.IsActive = true;   
          

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

            WorkDetailDtoModel CurrentWork = _sudeSessionContext.CurrentWork;
            ResultSetDto<ServingDetailDtoModel> result = await Api.GetHandler
                .GetApiAsync<ResultSetDto<ServingDetailDtoModel>>(ApiAddress.Serving.GetServingById + id);

          
            ViewData[Constants.ViewBagNames.CurrentWorkName] = CurrentWork.Title;




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
