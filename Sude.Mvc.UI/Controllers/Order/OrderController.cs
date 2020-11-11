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
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace Sude.Mvc.UI.Controllers.Order
{
    public class OrderController : Controller
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

            ResultSetDto<IEnumerable<OrderDetailDtoModel>> Orderlist = await Api.GetHandler
                .GetApiAsync<ResultSetDto<IEnumerable<OrderDetailDtoModel>>>(ApiAddress.Order.GetOrders);

            return PartialView("OrderList", Orderlist);
        }


        [HttpGet]
        public async Task<ActionResult> AddDetail()
        {

            OrderDetailNewDtoModel orderDetailNewDto = new OrderDetailNewDtoModel();
            return PartialView(orderDetailNewDto);
        }



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
     

        
            ResultSetDto<IEnumerable<CustomerDetailDtoModel>> Customerslist = await Api.GetHandler
      .GetApiAsync<ResultSetDto<IEnumerable<CustomerDetailDtoModel>>>(ApiAddress.Customer.GetCustomersByWorkId + CurrentWorkId);

            SelectList selectLists = new SelectList(Customerslist.Data as ICollection<CustomerDetailDtoModel>, "CustomerId", "Title");
            ViewData["Customers"] = selectLists;

            return PartialView();
        }

        [HttpPost]
        public async Task<ActionResult> Add(OrderNewDtoModel request)
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
            if(orderDetailNewDtoSession==null)
            {
                return Json(new ResultSetDto()
                {
                    IsSucceed = false,
                    Message = "لطفا اطلاعات جزئیات سفارش را وارد کنید."
                });

            }

            List<OrderDetailNewDtoModel> orderDetails = orderDetailNewDtoSession.ToList();
            if (orderDetails != null)
            {
                request.OrderDetails = orderDetails;
            }
            string CurrentWorkId = HttpContext.Session.GetString("CurrentWorkId");
            request.WorkId = CurrentWorkId;

            ResultSetDto<OrderNewDtoModel> result = await Api.GetHandler
             .GetApiAsync<ResultSetDto<OrderNewDtoModel>>(ApiAddress.Order.AddOrder, request);
            if(!result.IsSucceed)
            {
                return Json(new ResultSetDto()
                {
                    IsSucceed = false,
                    Message = result.Message
                });


            }
            return Json(result);

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


        public async Task<ActionResult> Delete(string id)
        {
            ResultSetDto result = await Api.GetHandler
              .GetApiAsync<ResultSetDto>(ApiAddress.Order.DeleteOrder, id);

            return Json(result);
        }
    }
}
