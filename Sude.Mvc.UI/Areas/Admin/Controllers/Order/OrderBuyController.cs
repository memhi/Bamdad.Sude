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
using Sude.Dto.DtoModels.Type;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace Sude.Mvc.UI.Admin.Controllers.Order
{
    public class OrderBuyController : BaseAdminController
    {

        public readonly SudeSessionContext _sudeSessionContext;
        private readonly IWebHostEnvironment _environment;
        public OrderBuyController(SudeSessionContext sudeSessionContext, IWebHostEnvironment environment)

        {
                      _environment = environment;
            _sudeSessionContext = sudeSessionContext;
        }

        private void DeleteOrderTempFiles()
        {
            string userDirectoryPath = Path.Combine(_environment.WebRootPath, "TempUserAttachmentFiles", _sudeSessionContext.CurrentUser.id);
            if (Directory.Exists(userDirectoryPath))
            {
                Directory.Delete(userDirectoryPath, true);

            }

        }
        // GET: WorkTypeController
        [HttpGet]
     //   [Authorize]
        public  ActionResult Index()
        {
            //ResultSetDto<IEnumerable<WorkTypeDetailDtoModel>> WorkTypelist = await Api.GetHandler
            //
              

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
            DeleteOrderTempFiles();
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

            var type = await Api.GetHandler
            .GetApiAsync<ResultSetDto<TypeDetailDtoModel>>(ApiAddress.Type.GetTypeByKey+ Constants.PaymenStatus.NotPaid);
            if (type != null && type.Data != null)
                request.PaymentStatusId = type.Data.TypeId;

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


            ResultSetDto<IEnumerable<TypeDetailDtoModel>> PaymentStatus = await Api.GetHandler
   .GetApiAsync<ResultSetDto<IEnumerable<TypeDetailDtoModel>>>(ApiAddress.Type.GetTypesByGroupKey + Constants.GroupType.PaymentStatus);
            ViewData[Constants.ViewBagNames.PaymentStatus] = PaymentStatus.Data;

            return PartialView(viewName: "Edit", model: new OrderEditDtoModel()
            {
                OrderId = result.Data.OrderId,              
                Description = result.Data.Description,
                 OrderDate=result.Data.OrderDate,
                  OrderNumber=result.Data.OrderNumber,
                   WorkId=result.Data.WorkId,
                PaymentStatusId = result.Data.PaymentStatusId,
                IsBuy = result.Data.IsBuy


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
