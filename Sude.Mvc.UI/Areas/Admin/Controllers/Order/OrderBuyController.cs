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
using Microsoft.AspNetCore.Localization;
using System.Globalization;

namespace Sude.Mvc.UI.Admin.Controllers.Order
{
    public class OrderBuyController : BaseAdminController
    {

        public readonly SudeSessionContext _sudeSessionContext;
        public OrderBuyController(SudeSessionContext sudeSessionContext)

        {

            _sudeSessionContext = sudeSessionContext;
        }
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
        public async Task<ActionResult> AddDetail()
        {

            OrderDetailDetailDtoModel orderDetailNewDto = new OrderDetailDetailDtoModel();
            return PartialView(orderDetailNewDto);
        }

    

        [HttpPost]
        public async Task<ActionResult> AddDetail(OrderDetailDetailDtoModel request )
        {

            string CurrentWorkId = _sudeSessionContext.CurrentWorkId;



            ResultSetDto<IEnumerable<ServingDetailDtoModel>> servinglist = await Api.GetHandler
      .GetApiAsync<ResultSetDto<IEnumerable<ServingDetailDtoModel>>>(ApiAddress.Serving.GetServingsByWorkId + CurrentWorkId);

            SelectList selectLists = new SelectList(servinglist.Data as ICollection<CustomerDetailDtoModel>, "ServingId", "Title", CurrentWorkId);
            ViewData[Constants.ViewBagNames.Servings] = selectLists;

            return PartialView();
        }



        [HttpGet]
        public async Task<ActionResult> Add()
        {
      
            string CurrentWorkId = _sudeSessionContext.CurrentWorkId;
            _sudeSessionContext.CurrentOrderDetails= null;
            OrderNewDtoModel order = new OrderNewDtoModel();
            order.WorkId = CurrentWorkId;
            order.IsBuy = true;
            if (!string.IsNullOrEmpty(CurrentWorkId))
            {
                order.OrderDate = DateTime.Now;
                ResultSetDto<IEnumerable<CustomerDetailDtoModel>> Customerslist = await Api.GetHandler
          .GetApiAsync<ResultSetDto<IEnumerable<CustomerDetailDtoModel>>>(ApiAddress.Customer.GetCustomersByWorkId + CurrentWorkId);

                SelectList selectLists = null;
                if(Customerslist!=null && Customerslist.Data!=null)
                    selectLists=new  SelectList(Customerslist.Data as ICollection<CustomerDetailDtoModel>, "CustomerId", "Title");
                ViewData[Constants.ViewBagNames.Customers] = selectLists;
            }
            return PartialView(order);
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


            IEnumerable<OrderDetailDetailDtoModel> orderDetailNewDtoSession = _sudeSessionContext.CurrentOrderDetails;
            if(orderDetailNewDtoSession==null || orderDetailNewDtoSession.Count() <= 0)
            {
                return Json(new ResultSetDto()
                {
                    IsSucceed = false,
                    Message = "لطفا اطلاعات جزئیات سفارش را وارد کنید."
                });

            }

            List<OrderDetailDetailDtoModel> orderDetails = orderDetailNewDtoSession.ToList();
            if (orderDetails != null)
            {
                request.OrderDetails = orderDetails;
            }
            string CurrentWorkId = _sudeSessionContext.CurrentWorkId;
            request.WorkId = CurrentWorkId;
            request.IsBuy = true;
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

            _sudeSessionContext.CurrentOrderDetails = null;

            return Json(result);

        }

        [HttpGet]//("{WorkTypeId}")]
        public async Task<ActionResult> Edit(string id)
        {

            _sudeSessionContext.CurrentOrderDetails = null;
          


            ResultSetDto<OrderDetailDtoModel> result = await Api.GetHandler
                .GetApiAsync<ResultSetDto<OrderDetailDtoModel>>(ApiAddress.Order.GetOrderById + id);
            if (result.Data.OrderDetails != null)
                _sudeSessionContext.CurrentOrderDetails= result.Data.OrderDetails.AsEnumerable();

            ResultSetDto<IEnumerable<CustomerDetailDtoModel>> Customerslist = await Api.GetHandler
    .GetApiAsync<ResultSetDto<IEnumerable<CustomerDetailDtoModel>>>(ApiAddress.Customer.GetCustomersByWorkId + result.Data.WorkId);

            SelectList selectLists = new SelectList(Customerslist.Data as ICollection<CustomerDetailDtoModel>, "CustomerId", "Title");
            ViewData[Constants.ViewBagNames.Customers] = selectLists;

            return PartialView(viewName: "Edit", model: new OrderEditDtoModel()
            {
                OrderId = result.Data.OrderId,              
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

            IEnumerable<OrderDetailDetailDtoModel> orderDetailNewDtoSession = _sudeSessionContext.CurrentOrderDetails;
            if (orderDetailNewDtoSession == null || orderDetailNewDtoSession.Count()<=0)
            {
                return Json(new ResultSetDto()
                {
                    IsSucceed = false,
                    Message = "لطفا اطلاعات جزئیات سفارش را وارد کنید."
                });

            }

            List<OrderDetailDetailDtoModel> orderDetails = orderDetailNewDtoSession.ToList();
            if (orderDetails != null)
            {
                request.OrderDetails = orderDetails;
            }
            //string CurrentWorkId = HttpContext.Session.GetString("CurrentWorkId");
            //request.WorkId = CurrentWorkId;

            request.IsBuy = true;

            ResultSetDto<OrderEditDtoModel> result = await Api.GetHandler
                .GetApiAsync<ResultSetDto<OrderEditDtoModel>>(ApiAddress.Order.EditOrder, request);
            _sudeSessionContext.CurrentOrderDetails= null;
            return Json(result);

        }
 
    }
}
