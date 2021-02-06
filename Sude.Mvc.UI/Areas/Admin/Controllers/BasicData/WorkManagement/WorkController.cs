using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using Sude.Dto.DtoModels.Content;
using Sude.Dto.DtoModels.Result;
using Sude.Dto.DtoModels.Type;
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
        public async Task<ActionResult> GetExcelWork()
        {



            ResultSetDto<IEnumerable<WorkDetailDtoModel>> result = await Api.GetHandler
           .GetApiAsync<ResultSetDto<IEnumerable<WorkDetailDtoModel>>>(ApiAddress.Work.GetWorksByUserId+_sudeSessionContext.CurrentUser.id);


            var WorkDetails = result.Data;

            var newFile = @"newbook.core.xlsx";

            using (var fs = new FileStream(newFile, FileMode.Create, FileAccess.ReadWrite))
            {

                IWorkbook workbook = new XSSFWorkbook();

                ISheet workSheet = workbook.CreateSheet("کسب و کارها");
                var workSheetHeaderStyle = workbook.CreateCellStyle();
                workSheetHeaderStyle.FillForegroundColor = HSSFColor.Blue.Index2;
                workSheetHeaderStyle.FillPattern = FillPattern.SolidForeground;
                
                var cellRow = workSheet.CreateRow(0).CreateCell(0);
                cellRow.CellStyle = workSheetHeaderStyle;
                cellRow.SetCellValue("ردیف");

                 cellRow = workSheet.CreateRow(0).CreateCell(1);
                cellRow.CellStyle = workSheetHeaderStyle;
                cellRow.SetCellValue("عنوان کسب و کار");

                cellRow = workSheet.CreateRow(0).CreateCell(1);
                cellRow.CellStyle = workSheetHeaderStyle;
                cellRow.SetCellValue("نوع کسب و کار");

                int i = 1;
                foreach(WorkDetailDtoModel workDetail in WorkDetails)
                {
                    cellRow = workSheet.CreateRow(i).CreateCell(0);
                    cellRow.SetCellValue(i);
                    cellRow = workSheet.CreateRow(i).CreateCell(1);
                    cellRow.SetCellValue(workDetail.Title);
                    cellRow = workSheet.CreateRow(i).CreateCell(2);
                    cellRow.SetCellValue(workDetail.WorkTypeName);

                    i++;
                }

              

           
                workbook.Write(fs);
                return File(fs, "application/xlsx");
            }



       
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
            _sudeSessionContext.DeletePictureTempFiles();
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

            List<AttachmentNewDtoModel> attachmentNewDtoModels = _sudeSessionContext.CurrentAttachmentPictures;
            if (attachmentNewDtoModels != null && attachmentNewDtoModels.Any())
            {

                request.Attachments = attachmentNewDtoModels;
            }

            ResultSetDto<WorkNewDtoModel> result = await Api.GetHandler
                .GetApiAsync<ResultSetDto<WorkNewDtoModel>>(ApiAddress.Work.AddWork, request);
            if(result.IsSucceed)
            {
                var typeresult = await Api.GetHandler
             .GetApiAsync<ResultSetDto<TypeDetailDtoModel>>(ApiAddress.Type.GetTypeByKey + Constants.AttachmentType.AttachmentWorkLogo);

                _sudeSessionContext.MoveTempAttachmentFiles(result.Data.WorkId, typeresult.Data.TypeId, result.Data.WorkId, false);

                _sudeSessionContext.CurrentAttachmentPictures = null;
                _sudeSessionContext.UserWorks = null;

            }
           

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
            _sudeSessionContext.DeletePictureTempFiles(); ;

            if (result.Data.Attachments != null && result.Data.Attachments.Any())
            {

              _sudeSessionContext.CopyMainAttachmentFiles(result.Data.Attachments.ToList());

            }

            return PartialView(viewName: "Edit", model: new WorkEditDtoModel()
            {
                WorkId = result.Data.WorkId,
                Title = result.Data.Title,
                Desc = result.Data.Desc,
                WorkTypeId = result.Data.WorkTypeId,
                Address = result.Data.Address,
                Attachments = result.Data.Attachments
            }); ;
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
            List<AttachmentNewDtoModel> attachmentNewDtoModels = _sudeSessionContext.CurrentAttachmentPictures;
            if (attachmentNewDtoModels != null && attachmentNewDtoModels.Any())
            {

                request.Attachments = attachmentNewDtoModels;
            }

            request.UserId = _sudeSessionContext.CurrentUser.id;
            ResultSetDto<WorkEditDtoModel> result = await Api.GetHandler
                .GetApiAsync<ResultSetDto<WorkEditDtoModel>>(ApiAddress.Work.EditWork, request);
            if (result.IsSucceed)
            {
                var typeresult = await Api.GetHandler
              .GetApiAsync<ResultSetDto<TypeDetailDtoModel>>(ApiAddress.Type.GetTypeByKey + Constants.AttachmentType.AttachmentWorkLogo);

                _sudeSessionContext.MoveTempAttachmentFiles(result.Data.WorkId, typeresult.Data.TypeId, result.Data.WorkId, true);
       
            _sudeSessionContext.CurrentAttachmentPictures = null;
                _sudeSessionContext.UserWorks = null;
            }
          
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
