using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using ClosedXML.Report;
using System.IO;
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
using Sude.Dto.DtoModels.Order;
using Sude.Dto.DtoModels.Result;
using Sude.Dto.DtoModels.Serving;
using Sude.Dto.DtoModels.Type;
using Sude.Dto.DtoModels.Work;
using Sude.Mvc.UI.ApiManagement;
using ClosedXML.Excel;

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

        [HttpGet]
        public async Task<IActionResult> WorkExcelList(string workid)
        {

            ResultSetDto<WorkDetailDtoModel> result = await Api.GetHandler
                                            .GetApiAsync<ResultSetDto<WorkDetailDtoModel>>(ApiAddress.Work.GetWorkById + workid);

            ResultSetDto<IEnumerable<ServingDetailDtoModel>> servingList = await Api.GetHandler
                .GetApiAsync<ResultSetDto<IEnumerable<ServingDetailDtoModel>>>(ApiAddress.Serving.GetServingsByWorkId + workid);


            ResultSetDto<IEnumerable<OrderDetailDtoModel>> orderList = await Api.GetHandler
                         .GetApiAsync<ResultSetDto<IEnumerable<OrderDetailDtoModel>>>(ApiAddress.Order.GetOrdersByWorkId + workid);


            List<ServingDetailDtoModel> _lstServingDetail = servingList.Data.ToList();
            List<OrderDetailDtoModel> OrderList = orderList.Data.ToList();

            #region template code
            //string tempAddress = Path.Combine(_sudeSessionContext.GetExcellPathTemplateAddress, "WorkerList.xlsx");
            //string outputFile = Path.Combine(_sudeSessionContext.GetExcellPathTemplateAddress, $"WorkerList_{DateTime.Now.Ticks}.xlsx");

            //var template = new  (tempAddress);

            //ItemCollection items = new ItemCollection();
            //items.lstServingDetail = _lstServingDetail;
            //items.OrderList = OrderList;
            ////          template.AddVariable(result.Data);
            ////template.AddVariable("lstServingDetail", _lstServingDetail);
            ////template.AddVariable("orderlist", OrderList);

            //template.AddVariable(items);

            //template.Generate();
            //template.SaveAs(outputFile);
            //var stream = new MemoryStream();

            //template.SaveAs(stream);

            //var excleByteArray = stream.ToArray();
            //stream.Position = 0;

            #endregion

            var workBook = new XLWorkbook();
            var workerSheet = workBook.Worksheets.Add("worker");

            workerSheet.Cell(1, 1).Value = "worker Info";
            workerSheet.Range(1, 1, 1, 3).Merge();




            List<WorkDetailDtoModel> workerList = new List<WorkDetailDtoModel>();
            workerList.Add(result.Data);


            workerSheet.Cell(2, 1).SetValue("WorkId");
            workerSheet.Cell(2, 2).SetValue("WorkTypeId");
            workerSheet.Cell(2, 3).SetValue("WorkTypeName");
            workerSheet.Cell(2, 4).SetValue("Title");
            workerSheet.Cell(2, 5).SetValue("Desc"); 

            workerSheet.Cell(2, 6).SetValue("AddressDtoModel.AddressId");
            workerSheet.Cell(2, 7).SetValue("AddressDtoModel.PostalCode");
            workerSheet.Cell(2, 8).SetValue("AddressDtoModel.Address");
            workerSheet.Cell(2, 9).SetValue("AddressDtoModel.Phone1");
            workerSheet.Cell(2, 10).SetValue("AddressDtoModel.Phone2");


            workerSheet.Cell(3, 1).SetValue(result.Data.WorkId);
            workerSheet.Cell(3, 2).SetValue(result.Data.WorkTypeId);
            workerSheet.Cell(3, 3).SetValue(result.Data.WorkTypeName);
            workerSheet.Cell(3, 4).SetValue(result.Data.Title);
            workerSheet.Cell(3, 5).SetValue(result.Data.Desc);
            workerSheet.Cell(3, 6).SetValue(result.Data.Address.AddressId);
            workerSheet.Cell(3, 7).SetValue(result.Data.Address.PostalCode);
            workerSheet.Cell(3, 8).SetValue(result.Data.Address.Address);
            workerSheet.Cell(3, 9).SetValue(result.Data.Address.Phone1);
            workerSheet.Cell(3, 10).SetValue(result.Data.Address.Phone2);


            workerSheet.Cell(4, 1).SetValue("AttachmentId");
            workerSheet.Cell(4, 2).SetValue("Title");
            workerSheet.Cell(4, 3).SetValue("EntityId");
            workerSheet.Cell(4, 4).SetValue("EntityTypeId");
            workerSheet.Cell(4, 5).SetValue("AttachmenTypetId");
            workerSheet.Cell(4, 6).SetValue("AttachmentFileAddress");
            workerSheet.Cell(4, 7).SetValue("AttachmentFileType");
            workerSheet.Cell(4, 8).SetValue("AttachmentContent");


            foreach (var item in result.Data.Attachments)
            {
                workerSheet.Cell(5, 1).SetValue(item.AttachmentId);
                workerSheet.Cell(5, 2).SetValue(item.Title);
                workerSheet.Cell(5, 3).SetValue(item.EntityId);
                workerSheet.Cell(5, 4).SetValue(item.EntityTypeId);
                workerSheet.Cell(5, 5).SetValue(item.AttachmenTypetId);
                workerSheet.Cell(5, 6).SetValue(item.AttachmentFileAddress);
                workerSheet.Cell(5, 7).SetValue(item.AttachmentFileType);
                workerSheet.Cell(5, 8).SetValue(item.AttachmentContent);
            }





            var servingSheet = workBook.Worksheets.Add("serving");
            servingSheet.Cell(1, 1).Value = "serving Info for worker" + result.Data.Title;
            servingSheet.Range(1, 1, 1, 6).Merge();


            servingSheet.Cell(2, 1).SetValue("ServingId");
            servingSheet.Cell(2, 2).SetValue("Title");
            servingSheet.Cell(2, 3).SetValue("Price");
            servingSheet.Cell(2, 4).SetValue("Desc");
            servingSheet.Cell(2, 5).SetValue("IsActive");

            servingSheet.Cell(2, 6).SetValue("WorkId");
            servingSheet.Cell(2, 7).SetValue("WorkName");
            servingSheet.Cell(2, 8).SetValue("HasInventoryTracking");
            servingSheet.Cell(2, 9).SetValue("InventoryCount");

            servingSheet.Cell(3, 1).InsertData(_lstServingDetail);            
            
            
            var OrderSheet = workBook.Worksheets.Add("Order");
            OrderSheet.Cell(1, 1).Value = "Order Info for worker" + result.Data.Title;
            OrderSheet.Range(1, 1, 1, 6).Merge();



            OrderSheet.Cell(2, 1).SetValue("OrderId");
            OrderSheet.Cell(2, 2).SetValue("WorkId");
            OrderSheet.Cell(2, 3).SetValue("WorkName");
            OrderSheet.Cell(2, 4).SetValue("OrderNumber");
            OrderSheet.Cell(2, 5).SetValue("Description");
            OrderSheet.Cell(2, 6).SetValue("SumPrice");
            OrderSheet.Cell(2, 7).SetValue("IsBuy");
            OrderSheet.Cell(2, 8).SetValue("PaymentStatusTitle");
            OrderSheet.Cell(2, 9).SetValue("PaymentStatusId");
            OrderSheet.Cell(2, 10).SetValue("OrderDate");

            OrderSheet.Cell(3, 1).InsertData(OrderList);


            var OrderDetails = workBook.Worksheets.Add("OrderDetails");

            OrderDetails.Cell(2, 1).SetValue("OrderId");
            OrderDetails.Cell(2, 2).SetValue("OrderDetailId");
            OrderDetails.Cell(2, 3).SetValue("ServingId");
            OrderDetails.Cell(2, 4).SetValue("ServingName");
            OrderDetails.Cell(2, 5).SetValue("Price");
            OrderDetails.Cell(2, 6).SetValue("Count");

            int row = 3;
            for (int i=0;i< OrderList.Count;i++)
            {
                OrderDetails.Cell(row, 1).InsertData(OrderList[i].OrderDetails);
                if(OrderList[i].OrderDetails!=null)
                    row += OrderList[i].OrderDetails.Count;
            }


          


            var OrderPayments = workBook.Worksheets.Add("OrderPayments");

            OrderPayments.Cell(2, 1).SetValue("OrderPaymentId");
            OrderPayments.Cell(2, 2).SetValue("OrderId");
            OrderPayments.Cell(2, 3).SetValue("PaymentPrice");
            OrderPayments.Cell(2, 4).SetValue("PaymentModeTitle");
            OrderPayments.Cell(2, 5).SetValue("Description");

            row = 3;
            for (int i = 0; i < OrderList.Count; i++)
            {
                OrderDetails.Cell(row, 1).InsertData(OrderList[i].OrderPayments);
                if (OrderList[i].OrderPayments != null)
                    row += OrderList[i].OrderPayments.Count;
            }

            var OrderAttachments = workBook.Worksheets.Add("OrderAttachments");
                OrderAttachments.Cell(2, 1).SetValue("AttachmentId");
                OrderAttachments.Cell(2, 2).SetValue("Title");
                OrderAttachments.Cell(2, 3).SetValue("EntityId");
                OrderAttachments.Cell(2, 4).SetValue("EntityTypeId");
                OrderAttachments.Cell(2, 5).SetValue("AttachmenTypetId");
                OrderAttachments.Cell(2, 6).SetValue("AttachmentFileAddress");
                OrderAttachments.Cell(2, 7).SetValue("AttachmentFileType");
                OrderAttachments.Cell(2, 8).SetValue("AttachmentContent");

            row = 3;
            for (int i = 0; i < OrderList.Count; i++)
            {
                OrderDetails.Cell(row, 1).InsertData(OrderList[i].Attachments);
                if (OrderList[i].Attachments != null)
                    row += OrderList[i].Attachments.Count;
            }


            string outputFile = Path.Combine(_sudeSessionContext.GetExcellPathTemplateAddress, $"WorkerList_{DateTime.Now.Ticks}.xlsx");

            var stream = new MemoryStream();


            workBook.SaveAs(stream);


            //var dataToReturn = File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{id}.xlsx");
            //  return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{id}.xlsx");
            //return File(stream.ToArray(), "application/vnd.ms-excel", $"{id}.xlsx");

            //  return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{id}.xlsx");

            stream.Position = 0;
            //return new FileStreamResult(stream, "application/ms-excel")
            //{ 
            //        FileDownloadName=$"{id}.xlsx"
            //};

            //return new FileStreamResult(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
            //{ 
            //        FileDownloadName=$"{workid}.xlsx"
            //};

          var  robj = File(stream.ToArray(), System.Net.Mime.MediaTypeNames.Application.Octet, $"{workid}.xlsx");
            return Json(robj);
            //return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{workid}.xlsx");
            
            //return Ok(); // Json(dataToReturn);

        }

    }
    public class ItemCollection
    {
        public List<ServingDetailDtoModel> lstServingDetail;
        public List<OrderDetailDtoModel> OrderList;
    }

}
