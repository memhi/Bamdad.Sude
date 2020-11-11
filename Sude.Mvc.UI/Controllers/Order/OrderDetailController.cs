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

namespace Sude.Mvc.UI.Controllers.Order
{
    public class OrderDetailController : Controller
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
            IEnumerable<OrderDetailNewDtoModel> CurrentDetails = HttpContext.Session.GetObject<IEnumerable<OrderDetailNewDtoModel>>("OrderDetails");

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
        public async Task<ActionResult> AddDetail( OrderDetailNewDtoModel request )
        {

            string CurrentWorkId = HttpContext.Session.GetString("CurrentWorkId");



            ResultSetDto<IEnumerable<ServingDetailDtoModel>> servinglist = await Api.GetHandler
      .GetApiAsync<ResultSetDto<IEnumerable<ServingDetailDtoModel>>>(ApiAddress.Serving.GetServingsByWorkId + CurrentWorkId);

            SelectList selectLists = new SelectList(servinglist.Data as ICollection<CustomerDetailDtoModel>, "ServingId", "Title", CurrentWorkId);
            ViewData["Customers"] = selectLists;

            return PartialView();
        }



        [HttpGet]
        public async Task<ActionResult> Add()
        {
      
            string CurrentWorkId = HttpContext.Session.GetString("CurrentWorkId");



            ResultSetDto<IEnumerable<ServingDetailDtoModel>> servinglist = await Api.GetHandler
      .GetApiAsync<ResultSetDto<IEnumerable<ServingDetailDtoModel>>>(ApiAddress.Serving.GetServingsByWorkId + CurrentWorkId);
            foreach(ServingDetailDtoModel servingDetailDto in servinglist.Data)
            {
                servingDetailDto.ServingId = servingDetailDto.ServingId + "##" + servingDetailDto.Title + "##" + servingDetailDto.Price.ToString();

            }

            SelectList selectServingsLists = new SelectList(servinglist.Data as ICollection<ServingDetailDtoModel>, "ServingId", "Title");
            ViewData["Servings"] = selectServingsLists;


            ResultSetDto<IEnumerable<CustomerDetailDtoModel>> Customerslist = await Api.GetHandler
      .GetApiAsync<ResultSetDto<IEnumerable<CustomerDetailDtoModel>>>(ApiAddress.Customer.GetCustomersByWorkId + CurrentWorkId);

            SelectList selectLists = new SelectList(Customerslist.Data as ICollection<CustomerDetailDtoModel>, "CustomerId", "Title");
            ViewData["Customers"] = selectLists;

            return PartialView();
        }

        [HttpPost]
        public async Task<ActionResult> Add(OrderDetailNewDtoModel request)
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

            IEnumerable<OrderDetailNewDtoModel> orderDetailNewDtoSession = HttpContext.Session.GetObject<IEnumerable<OrderDetailNewDtoModel>>("OrderDetails");
            
            if (orderDetailNewDtoSession == null)
            {
                List<OrderDetailNewDtoModel> orderDetailNewDtos = new List<OrderDetailNewDtoModel>();
                OrderDetailNewDtoModel orderDetailSession = new OrderDetailNewDtoModel();
                orderDetailSession.OrderDetailId = string.IsNullOrEmpty(request.ServingId) ==true ? "" : request.ServingId;
                orderDetailSession.OrderId = string.IsNullOrEmpty(request.OrderId) == true ? "" : request.OrderId;
                orderDetailSession.Price = request.Price;
                orderDetailSession.ServingId = string.IsNullOrEmpty(request.ServingId) == true ? "" : request.ServingId;
                orderDetailSession.Count = request.Count;
                orderDetailSession.ServingName = string.IsNullOrEmpty(request.ServingName) == true ? "" : request.ServingName;
                orderDetailNewDtos.Add(orderDetailSession);
                HttpContext.Session.SetObject("OrderDetails", orderDetailNewDtos.AsEnumerable<OrderDetailNewDtoModel>()); ;
      
            }
          
            else
            {
                List<OrderDetailNewDtoModel> orderDetailNewDtos = orderDetailNewDtoSession.ToList();
                OrderDetailNewDtoModel orderDetailSession = orderDetailNewDtos.Where(o => o.OrderDetailId == request.ServingId).FirstOrDefault();
                bool isNew = false;
                if(orderDetailSession==null)
                {
                    orderDetailSession = new OrderDetailNewDtoModel();
                     isNew = true; 

                }
                orderDetailSession.OrderDetailId = string.IsNullOrEmpty(request.ServingId) == true ? "" : request.ServingId;
                orderDetailSession.OrderId = string.IsNullOrEmpty(request.OrderId) == true ? "" : request.OrderId;
                orderDetailSession.Price = request.Price;
                orderDetailSession.ServingId = string.IsNullOrEmpty(request.ServingId) == true ? "" : request.ServingId;
                orderDetailSession.Count = request.Count;
                orderDetailSession.ServingName = string.IsNullOrEmpty(request.ServingName) == true ? "" : request.ServingName;
                if (isNew)
                orderDetailNewDtos.Add(orderDetailSession);
                HttpContext.Session.SetObject("OrderDetails", orderDetailNewDtos.AsEnumerable<OrderDetailNewDtoModel>()); ;
            }

            //ResultSetDto<OrderNewDtoModel> result = await Api.GetHandler
            //    .GetApiAsync<ResultSetDto<OrderNewDtoModel>>(ApiAddress.Order.AddOrder, request);
            return Json(new ResultSetDto<IEnumerable<OrderDetailNewDtoModel>>()
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

            if(servingId==id)
            {
                List<OrderDetailNewDtoModel> orderDetailNewDtoSession = HttpContext.Session.GetObject<IEnumerable<OrderDetailNewDtoModel>>("OrderDetails").ToList();
                OrderDetailNewDtoModel orderDetailNewDto = orderDetailNewDtoSession.Where(o => o.OrderDetailId == id && o.ServingId == servingId).FirstOrDefault();
                if (orderDetailNewDto != null)
                {
                    orderDetailNewDtoSession.Remove(orderDetailNewDto);
                    HttpContext.Session.SetObject("OrderDetails", orderDetailNewDtoSession.AsEnumerable<OrderDetailNewDtoModel>()); ;
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
            else
            {
                ResultSetDto result = await Api.GetHandler
              .GetApiAsync<ResultSetDto>(ApiAddress.OrderDetail.DeleteOrderDetail, id);

                return Json(result);
           
            
            }


           
        }
    }
}
