using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Sude.Dto.DtoModels.Result;
using Sude.Dto.DtoModels.Serving;
using Sude.Mvc.UI.ApiManagement;

namespace Sude.Mvc.UI.Admin.Controllers.BasicData.InventoryTypeManagement
{
    public class InventoryTypeController : BaseAdminController
    {
        // GET: InventoryTypeController
        [HttpGet]
     //   [Authorize]
        public  ActionResult Index()
        {
            //ResultSetDto<IEnumerable<InventoryTypeDetailDtoModel>> InventoryTypelist = await Api.GetHandler
            //             .GetApiAsync<ResultSetDto<IEnumerable<InventoryTypeDetailDtoModel>>>(ApiAddress.GetInventoryTypes);

            return View();
        }

        [HttpGet]
        //  [Authorize]
        public async Task<ActionResult> List(string title)
        {
            ResultSetDto<IEnumerable<InventoryTypeDetailDtoModel>> InventoryTypelist = null;
            if (string.IsNullOrEmpty(title) || title.Length < 3)
            {
                InventoryTypelist = await Api.GetHandler
                    .GetApiAsync<ResultSetDto<IEnumerable<InventoryTypeDetailDtoModel>>>(ApiAddress.InventoryType.GetInventoryTypes);

   
                    }
            else
            {
                 InventoryTypelist = await Api.GetHandler
                    .GetApiAsync<ResultSetDto<IEnumerable<InventoryTypeDetailDtoModel>>>(ApiAddress.InventoryType.SearchInventoryTypes+title);


            }
            return PartialView("InventoryTypeList", InventoryTypelist);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return PartialView();
        }

        [HttpPost]
        public async Task<ActionResult> Add(InventoryTypeNewDtoModel request)
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

            ResultSetDto<InventoryTypeNewDtoModel> result = await Api.GetHandler
                .GetApiAsync<ResultSetDto<InventoryTypeNewDtoModel>>(ApiAddress.InventoryType.AddInventoryType, request);


            return Json(result);

        }

        [HttpGet]//("{InventoryTypeId}")]
        public async Task<ActionResult> Edit(string id)
        {
            ResultSetDto<InventoryTypeDetailDtoModel> result = await Api.GetHandler
                .GetApiAsync<ResultSetDto<InventoryTypeDetailDtoModel>>(ApiAddress.InventoryType.GetInventoryTypeById + id);

            return PartialView(viewName: "Edit", model: new InventoryTypeEditDtoModel()
            {
                InventoryTypeId = result.Data.InventoryTypeId,
                Title = result.Data.Title,               
                Description = result.Data.Description
            });
        }
        //[Route("Edit/{request}")]
        [HttpPost]
        public async Task<ActionResult> Edit(InventoryTypeEditDtoModel request)
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

            ResultSetDto<InventoryTypeEditDtoModel> result = await Api.GetHandler
                .GetApiAsync<ResultSetDto<InventoryTypeEditDtoModel>>(ApiAddress.InventoryType.EditInventoryType, request);

            return Json(result);

        }

        public async Task<ActionResult> Detail(string id)
        {
            ResultSetDto<InventoryTypeDetailDtoModel> result = await Api.GetHandler
             .GetApiAsync<ResultSetDto<InventoryTypeDetailDtoModel>>(ApiAddress.InventoryType.GetInventoryTypeById + id);

            var InventoryTypeDetail = result.Data;

            return View(viewName: "Detail", model: InventoryTypeDetail);
        }


        public async Task<ActionResult> Delete(string id)
        {
            ResultSetDto result = await Api.GetHandler
              .GetApiAsync<ResultSetDto>(ApiAddress.InventoryType.DeleteInventoryType, id);

            return Json(result);
        }
    }
}
