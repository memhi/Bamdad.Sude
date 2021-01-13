using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using Sude.Dto.DtoModels.Result;
using Sude.Dto.DtoModels.Work;
using Sude.Mvc.UI.ApiManagement;


namespace Sude.Mvc.UI.Admin.Controllers.BasicData.WorkManagement
{
    public class WorkController : BaseAdminController
    {
        public readonly SudeSessionContext _sudeSessionContext;
        public WorkController (SudeSessionContext sudeSessionContext)
        {
            _sudeSessionContext = sudeSessionContext;


        }
        // GET: WorkController
        [HttpGet]
     //   [Authorize]
        public  ActionResult Index()
        {
            //ResultSetDto<IEnumerable<WorkDetailDtoModel>> Worklist = await Api.GetHandler
            //             .GetApiAsync<ResultSetDto<IEnumerable<WorkDetailDtoModel>>>(ApiAddress.GetWorks);

        
            return View();
        }


        [HttpGet]
 
        public async Task<ActionResult> ViewWorks()
        {

            //ResultSetDto<IEnumerable<WorkDetailDtoModel>> Worklist = await Api.GetHandler
            //            .GetApiAsync<ResultSetDto<IEnumerable<WorkDetailDtoModel>>>(ApiAddress.Work.GetWorksByUserId+_sudeSessionContext.CurrentUser.id);





            return View(_sudeSessionContext.UserWorks.AsEnumerable());
        }

        [HttpGet]
        // [Authorize]
        public async Task<ActionResult> SetDefaultWork(string workId)
        {
            ResultSetDto<WorkDetailDtoModel> result = await Api.GetHandler
             .GetApiAsync<ResultSetDto<WorkDetailDtoModel>>(ApiAddress.Work.GetWorkById + workId);

            
              _sudeSessionContext.CurrentWorkId= result.Data.WorkId;
            _sudeSessionContext.CurrentWorkName= result.Data.Title;
            _sudeSessionContext.CurrentWork= result.Data;

            return Json(workId);
        }


        [HttpGet]
       // [Authorize]
        public async Task<ActionResult> List()
        {
            //System.Threading.Thread.Sleep(1000);

            //ResultSetDto<IEnumerable<WorkDetailDtoModel>> Worklist = await Api.GetHandler
            //    .GetApiAsync<ResultSetDto<IEnumerable<WorkDetailDtoModel>>>(ApiAddress.Work.GetWorksByUserId + _sudeSessionContext.CurrentUser.id);


            return PartialView("WorkList", _sudeSessionContext.UserWorks.AsEnumerable());
        }

        [HttpGet]
        public async Task<ActionResult> Add()
        {
            ResultSetDto<IEnumerable<WorkTypeDetailDtoModel>> Worktypelist = await Api.GetHandler
             .GetApiAsync<ResultSetDto<IEnumerable<WorkTypeDetailDtoModel>>>(ApiAddress.WorkType.GetWorkTypes);
            WorkNewDtoModel workNewDtoModel = new WorkNewDtoModel();
            
            workNewDtoModel.Desc = "";
            workNewDtoModel.Title = "";
            workNewDtoModel.WorkId = "";
            workNewDtoModel.WorkTypeId = "";
            SelectList selectLists = new SelectList(Worktypelist.Data as ICollection<Sude.Dto.DtoModels.Work.WorkTypeDetailDtoModel>, "WorkTypeId", "Title");

            ViewData  ["WorkTypes"] = selectLists;
            



            return PartialView("Add",workNewDtoModel);
        }

        [HttpPost]
        public async Task<ActionResult> Add(WorkNewDtoModel request)
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
            request.UserId = _sudeSessionContext.CurrentUser.id;

            ResultSetDto<WorkNewDtoModel> result = await Api.GetHandler
                .GetApiAsync<ResultSetDto<WorkNewDtoModel>>(ApiAddress.Work.AddWork, request);

            _sudeSessionContext.UserWorks = null;

            return Json(result);

        }

        [HttpGet]//("{WorkId}")]
        public async Task<ActionResult> Edit(string id)
        {
            ResultSetDto<WorkDetailDtoModel> result = await Api.GetHandler
                .GetApiAsync<ResultSetDto<WorkDetailDtoModel>>(ApiAddress.Work.GetWorkById + id);
            ResultSetDto<IEnumerable<WorkTypeDetailDtoModel>> Worktypelist = await Api.GetHandler
         .GetApiAsync<ResultSetDto<IEnumerable<WorkTypeDetailDtoModel>>>(ApiAddress.WorkType.GetWorkTypes);

            SelectList selectLists = new SelectList(Worktypelist.Data as ICollection<Sude.Dto.DtoModels.Work.WorkTypeDetailDtoModel>, "WorkTypeId", "Title",result.Data.WorkTypeId);

            ViewData["WorkTypes"] = selectLists;

            return PartialView(viewName: "Edit", model: new WorkEditDtoModel()
            {
                WorkId = result.Data.WorkId,
                Title = result.Data.Title,         
                Desc = result.Data.Desc,
                WorkTypeId=result.Data.WorkTypeId
            });
        }
        //[Route("Edit/{request}")]
        [HttpPost]
        public async Task<ActionResult> Edit(WorkEditDtoModel request)
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
            request.UserId = _sudeSessionContext.CurrentUser.id;
            ResultSetDto<WorkEditDtoModel> result = await Api.GetHandler
                .GetApiAsync<ResultSetDto<WorkEditDtoModel>>(ApiAddress.Work.EditWork, request);
            _sudeSessionContext.UserWorks = null;
            return Json(result);

        }

        public async Task<ActionResult> Detail(string id)
        {
            ResultSetDto<WorkDetailDtoModel> result = await Api.GetHandler
             .GetApiAsync<ResultSetDto<WorkDetailDtoModel>>(ApiAddress.Work.GetWorkById + id);

            var WorkDetail = result.Data;

            return View(viewName: "Detail", model: WorkDetail);
        }


        public async Task<ActionResult> Delete(string id)
        {
            ResultSetDto result = await Api.GetHandler
              .GetApiAsync<ResultSetDto>(ApiAddress.Work.DeleteWork, id);
            _sudeSessionContext.UserWorks = null;
            return Json(result);
        }
    }
}
