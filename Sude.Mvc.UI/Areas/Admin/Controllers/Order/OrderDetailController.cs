using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Sude.Dto.DtoModels.Result;
using Sude.Dto.DtoModels.Order;
using Sude.Dto.DtoModels.Work;
using Sude.Mvc.UI.ApiManagement;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using Sude.Dto.DtoModels.Account;
using Sude.Dto.DtoModels.Serving;

namespace Sude.Mvc.UI.Admin.Controllers.Order
{
    public class OrderDetailController : BaseAdminController
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
            IEnumerable<OrderDetailDetailDtoModel> CurrentDetails = HttpContext.Session.GetObject<IEnumerable<OrderDetailDetailDtoModel>>(Constants.SessionNames.OrderDetails);

            ViewData["OrderDetails"] = CurrentDetails;
        

            return PartialView("OrderDetailList", CurrentDetails);
        }


        //[HttpGet]
        //public async Task<ActionResult> Add()
        //{

        //    OrderDetailNewDtoModel orderDetailNewDto = new OrderDetailNewDtoModel();
        //    return PartialView(orderDetailNewDto);
        //}



        [HttpPost]
        public async Task<ActionResult> AddDetail(OrderDetailDetailDtoModel request )
        {

            string CurrentWorkId = HttpContext.Session.GetString(Constants.SessionNames.CurrentWorkId);



            ResultSetDto<IEnumerable<ServingDetailDtoModel>> servinglist = await Api.GetHandler
      .GetApiAsync<ResultSetDto<IEnumerable<ServingDetailDtoModel>>>(ApiAddress.Serving.GetServingsByWorkId + CurrentWorkId);

            SelectList selectLists = new SelectList(servinglist.Data as ICollection<CustomerDetailDtoModel>, "ServingId", "Title", CurrentWorkId);
            ViewData[Constants.ViewBagNames.Servings] = selectLists;

            return PartialView();
        }



        [HttpGet]
        public async Task<ActionResult> Add(string servingHasTracking,string isActive)
        {
      
            string CurrentWorkId = HttpContext.Session.GetString(Constants.SessionNames.CurrentWorkId);



            ResultSetDto<IEnumerable<ServingDetailDtoModel>> servinglist = await Api.GetHandler
      .GetApiAsync<ResultSetDto<IEnumerable<ServingDetailDtoModel>>>(ApiAddress.Serving.GetServingsByWorkId + CurrentWorkId);
           
            if(servinglist!=null && servinglist.Data!=null && servingHasTracking=="1")
            {
                servinglist.Data = servinglist.Data.Where(s => s.HasInventoryTracking == true).AsEnumerable<ServingDetailDtoModel>();

            }
            if (servinglist != null && servinglist.Data != null && isActive == "1")
            {
                servinglist.Data = servinglist.Data.Where(s => s.IsActive == true).AsEnumerable<ServingDetailDtoModel>();

            }
            foreach (ServingDetailDtoModel servingDetailDto in servinglist.Data)
            {
                
                servingDetailDto.ServingId = servingDetailDto.ServingId + "##" + servingDetailDto.Title + "##" + servingDetailDto.Price.ToString();

            }

            SelectList selectServingsLists = new SelectList(servinglist.Data.ToList() as ICollection<ServingDetailDtoModel>, "ServingId", "Title");
            ViewData[Constants.ViewBagNames.Servings] = selectServingsLists;


            ResultSetDto<IEnumerable<CustomerDetailDtoModel>> Customerslist = await Api.GetHandler
      .GetApiAsync<ResultSetDto<IEnumerable<CustomerDetailDtoModel>>>(ApiAddress.Customer.GetCustomersByWorkId + CurrentWorkId);

            SelectList selectLists = null;
            if(Customerslist!=null && Customerslist.Data!=null)
                selectLists= new SelectList(Customerslist.Data as ICollection<CustomerDetailDtoModel>, "CustomerId", "Title");
            ViewData[Constants.ViewBagNames.Customers] = selectLists;

            return PartialView();
        }

        [HttpPost]
        public async Task<ActionResult> Add(OrderDetailDetailDtoModel request)
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

            IEnumerable<OrderDetailDetailDtoModel> orderDetailNewDtoSession = HttpContext.Session.GetObject<IEnumerable<OrderDetailDetailDtoModel>>("OrderDetails");
            
            if (orderDetailNewDtoSession == null)
            {
                List<OrderDetailDetailDtoModel> orderDetailNewDtos = new List<OrderDetailDetailDtoModel>();
                OrderDetailDetailDtoModel orderDetailSession = new OrderDetailDetailDtoModel();
                orderDetailSession.OrderDetailId = string.IsNullOrEmpty(request.ServingId) ==true ? "" : request.ServingId;
                orderDetailSession.OrderId = string.IsNullOrEmpty(request.OrderId) == true ? "" : request.OrderId;
                orderDetailSession.Price = request.Price;
                orderDetailSession.ServingId = string.IsNullOrEmpty(request.ServingId) == true ? "" : request.ServingId;
                orderDetailSession.Count = request.Count;
                orderDetailSession.ServingName = string.IsNullOrEmpty(request.ServingName) == true ? "" : request.ServingName;
                orderDetailNewDtos.Add(orderDetailSession);
                HttpContext.Session.SetObject(Constants.SessionNames.OrderDetails, orderDetailNewDtos.AsEnumerable<OrderDetailDetailDtoModel>()); ;
      
            }
          
            else
            {
                List<OrderDetailDetailDtoModel> orderDetailNewDtos = orderDetailNewDtoSession.ToList();
                OrderDetailDetailDtoModel orderDetailSession = orderDetailNewDtos.Where(o => o.ServingId == request.ServingId).FirstOrDefault();
             
                if (orderDetailSession == null)
                {
                    orderDetailSession = new OrderDetailDetailDtoModel();
               

                }
                else
                    orderDetailNewDtos.Remove(orderDetailSession);
                orderDetailSession = new OrderDetailDetailDtoModel();
                orderDetailSession.OrderDetailId = string.IsNullOrEmpty(request.ServingId) == true ? "" : request.ServingId;
                orderDetailSession.OrderId = string.IsNullOrEmpty(request.OrderId) == true ? "" : request.OrderId;
                orderDetailSession.Price = request.Price;
                orderDetailSession.ServingId = string.IsNullOrEmpty(request.ServingId) == true ? "" : request.ServingId;
                orderDetailSession.Count = request.Count;
                orderDetailSession.ServingName = string.IsNullOrEmpty(request.ServingName) == true ? "" : request.ServingName;
   
                orderDetailNewDtos.Add(orderDetailSession);
                HttpContext.Session.SetObject(Constants.SessionNames.OrderDetails, orderDetailNewDtos.AsEnumerable<OrderDetailDetailDtoModel>()); ;
            }

            //ResultSetDto<OrderNewDtoModel> result = await Api.GetHandler
            //    .GetApiAsync<ResultSetDto<OrderNewDtoModel>>(ApiAddress.Order.AddOrder, request);
            return Json(new ResultSetDto<IEnumerable<OrderDetailDetailDtoModel>>()
            {
                IsSucceed = true,
                Message = null, 
                 Data= orderDetailNewDtoSession

            });

        //    return Json(orderDetailSession);

        }

        [HttpGet]//("{WorkTypeId}")]
        public async Task<ActionResult> Edit(string id)
        {
            ResultSetDto<OrderDetailDtoModel> result = await Api.GetHandler
                .GetApiAsync<ResultSetDto<OrderDetailDtoModel>>(ApiAddress.Order.GetOrderById + id);

            return PartialView(viewName: "Edit", model: new OrderEditDtoModel()
            {
                OrderId = result.Data.OrderId,
                CustomerId = result.Data.Customer.CustomerId,
                CustomerName = result.Data.Customer.Title,
                Description = result.Data.Description,
                 OrderDate=result.Data.OrderDate,
                  OrderNumber=result.Data.OrderNumber,
                   WorkId=result.Data.WorkId
 
            });
        }
        //[Route("Edit/{request}")]
        [HttpPost]
        public async Task<ActionResult> Edit(OrderEditDtoModel request)
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

            ResultSetDto<OrderEditDtoModel> result = await Api.GetHandler
                .GetApiAsync<ResultSetDto<OrderEditDtoModel>>(ApiAddress.Order.EditOrder, request);

            return Json(result);

        }

        public async Task<ActionResult> Detail(string id)
        {
            ResultSetDto<OrderDetailDtoModel> result = await Api.GetHandler
             .GetApiAsync<ResultSetDto<OrderDetailDtoModel>>(ApiAddress.Order.GetOrderById+ id);

            var orderDetail = result.Data;

            return View(viewName: "Detail", model: orderDetail);
        }


        public async Task<ActionResult> Delete(string id,string servingId)
        {

         
                List<OrderDetailDetailDtoModel> orderDetailNewDtoSession = HttpContext.Session.GetObject<IEnumerable<OrderDetailDetailDtoModel>>(Constants.SessionNames.OrderDetails).ToList();
                OrderDetailDetailDtoModel orderDetailNewDto = orderDetailNewDtoSession.Where(o => o.OrderDetailId == id && o.ServingId == servingId).FirstOrDefault();
                if (orderDetailNewDto != null)
                {
                    orderDetailNewDtoSession.Remove(orderDetailNewDto);
                    HttpContext.Session.SetObject(Constants.SessionNames.OrderDetails, orderDetailNewDtoSession.AsEnumerable<OrderDetailDetailDtoModel>()); ;
                    return Ok(new ResultSetDto()
                    {
                        IsSucceed = true,
                        Message = ""
                    });

                }
                else
                {
                    return Ok(new ResultSetDto()
                    {
                        IsSucceed = false,
                        Message = "Item not found"
                    });


                }


            
           
        }
    }
}
