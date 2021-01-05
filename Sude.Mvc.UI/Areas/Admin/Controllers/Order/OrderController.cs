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
using Sude.Dto.DtoModels.Type;
using Sude.Mvc.UI.ApiManagement;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using Sude.Dto.DtoModels.Account;
using Sude.Dto.DtoModels.Serving;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using System.Reflection.Metadata;

namespace Sude.Mvc.UI.Admin.Controllers.Order
{
    public class OrderController : BaseAdminController
    {
        public readonly SudeSessionContext _sudeSessionContext;
        public OrderController(SudeSessionContext sudeSessionContext)

        {

            _sudeSessionContext = sudeSessionContext;
        }
        // GET: WorkTypeController
        [HttpGet]
        //   [Authorize]
        public ActionResult Index()
        {
            //ResultSetDto<IEnumerable<WorkTypeDetailDtoModel>> WorkTypelist = await Api.GetHandler
            //             .GetApiAsync<ResultSetDto<IEnumerable<WorkTypeDetailDtoModel>>>(ApiAddress.GetWorkTypes);

            return View();
        }



        [HttpGet]

        public async Task<ActionResult> ViewPayments(string id)
        {
         

            ResultSetDto<OrderDetailDtoModel> orderDetailDto = await Api.GetHandler
                .GetApiAsync<ResultSetDto<OrderDetailDtoModel>>(ApiAddress.Order.GetOrderById + id);
            if (!orderDetailDto.IsSucceed || orderDetailDto == null)
            {
                return Json(new ResultSetDto()
                {
                    IsSucceed = false,
                    Message = Constants.Messages.OrderNotFound
                });



            }
            ViewBag.OrderId = orderDetailDto.Data.OrderId;
            ViewBag.PaymentStatusId = orderDetailDto.Data.PaymentStatusId;

            ResultSetDto<IEnumerable<TypeDetailDtoModel>> paymentStatus = await Api.GetHandler
                        .GetApiAsync<ResultSetDto<IEnumerable<TypeDetailDtoModel>>>(ApiAddress.Type.GetTypesByGroupKey +Constants.GroupType.PaymentStatus);
            
            List<TypeDetailDtoModel> list = new List<TypeDetailDtoModel>();
            if (paymentStatus != null && paymentStatus.Data != null && paymentStatus.Data.Any())
                foreach(TypeDetailDtoModel typeDetailDto in paymentStatus.Data)
                list.Add(typeDetailDto);
            return View(list);
        }


        [HttpGet]
        // [Authorize]
        public async Task<ActionResult> List()
        {
            //System.Threading.Thread.Sleep(1000);

            string CurrentWorkId = _sudeSessionContext.CurrentWorkId;
            ResultSetDto<IEnumerable<OrderDetailDtoModel>> Orderlist = new ResultSetDto<IEnumerable<OrderDetailDtoModel>>();
            if (!string.IsNullOrEmpty(CurrentWorkId))
                Orderlist = await Api.GetHandler
                 .GetApiAsync<ResultSetDto<IEnumerable<OrderDetailDtoModel>>>(ApiAddress.Order.GetOrdersByWorkId + CurrentWorkId);

            return PartialView("OrderList", Orderlist);
        }


        [HttpGet]
        public async Task<ActionResult> AddDetail()
        {

            OrderDetailDetailDtoModel orderDetailNewDto = new OrderDetailDetailDtoModel();
            return PartialView(orderDetailNewDto);
        }

        [HttpGet]
        public async Task<ActionResult> GetOrdersStatistics(string statisticsType)
        {
            DateTime nowDt = DateTime.Now;
            PersianCalendar persianCalendar = new PersianCalendar();
            var result = new List<object>();
            switch (statisticsType)
            {
                case "year":
                    //year statistics
                    var yearAgoDt = persianCalendar.AddYears(nowDt, -1).AddMonths(1);
                    int agoDay = persianCalendar.GetDayOfMonth(yearAgoDt), agoMonth = persianCalendar.GetMonth(yearAgoDt), agoYear = persianCalendar.GetYear(yearAgoDt);
                    var yearagoctopersian = persianCalendar.ToDateTime(agoYear, agoMonth, 1, 0, 0, 0, 0);

                    for (var i = 0; i <= 12; i++)
                    {

                        OrderStatisticsDtoModel orderStatistics = new OrderStatisticsDtoModel()
                        {
                            DateFrom = yearagoctopersian,
                            DateTo = yearagoctopersian.AddMonths(1),
                            WorkId = "",
                            SearchCount = 0

                        };
                        int count = 0;
                        ResultSetDto<OrderStatisticsDtoModel> resultGetStatistics = await Api.GetHandler
        .GetApiAsync<ResultSetDto<OrderStatisticsDtoModel>>(ApiAddress.Order.GetOrdersStatistics, orderStatistics);
                        if (resultGetStatistics.IsSucceed && resultGetStatistics.Data != null)
                            count = resultGetStatistics.Data.SearchCount;


                        result.Add(new
                        {
                            date = yearagoctopersian.ToPersianMonthName(),
                            value = count.ToString()
                        });

                        yearagoctopersian = yearagoctopersian.AddMonths(1);
                    }

                    break;
                case "month":
                    //month statistics
                    var monthAgoDt = nowDt.AddDays(-30);
                    var searchMonthDateUser = new DateTime(monthAgoDt.Year, monthAgoDt.Month, monthAgoDt.Day);
                    for (var i = 0; i <= 30; i++)
                    {


                        OrderStatisticsDtoModel orderStatistics = new OrderStatisticsDtoModel()
                        {
                            DateFrom = searchMonthDateUser,
                            DateTo = searchMonthDateUser.AddDays(1),
                            WorkId = "",
                            SearchCount = 0

                        };
                        int count = 0;
                        ResultSetDto<OrderStatisticsDtoModel> resultGetStatistics = await Api.GetHandler
        .GetApiAsync<ResultSetDto<OrderStatisticsDtoModel>>(ApiAddress.Order.GetOrdersStatistics, orderStatistics);
                        if (resultGetStatistics.IsSucceed && resultGetStatistics.Data != null)
                            count = resultGetStatistics.Data.SearchCount;


                        result.Add(new
                        {
                            date = searchMonthDateUser.ToPersianDay().ToString() + " " + searchMonthDateUser.ToPersianMonthName(),
                            value = count.ToString()
                        });

                        searchMonthDateUser = searchMonthDateUser.AddDays(1);
                    }

                    break;
                case "week":
                default:
                    //week statistics
                    var weekAgoDt = nowDt.AddDays(-7);
                    var searchWeekDateUser = new DateTime(weekAgoDt.Year, weekAgoDt.Month, weekAgoDt.Day);
                    for (var i = 0; i <= 7; i++)
                    {
                        OrderStatisticsDtoModel orderStatistics = new OrderStatisticsDtoModel()
                        {
                            DateFrom = searchWeekDateUser,
                            DateTo = searchWeekDateUser.AddDays(1),
                            WorkId = "",
                            SearchCount = 0

                        };
                        int count = 0;
                        ResultSetDto<OrderStatisticsDtoModel> resultGetStatistics = await Api.GetHandler
        .GetApiAsync<ResultSetDto<OrderStatisticsDtoModel>>(ApiAddress.Order.GetOrdersStatistics, orderStatistics);
                        if (resultGetStatistics.IsSucceed && resultGetStatistics.Data != null)
                            count = resultGetStatistics.Data.SearchCount;



                        result.Add(new
                        {
                            date = searchWeekDateUser.ToPersianWeekDayName() + " " + searchWeekDateUser.ToPersianDay().ToString(),
                            value = count.ToString()
                        });

                        searchWeekDateUser = searchWeekDateUser.AddDays(1);
                    }

                    break;
            }


            return Json(result);
        }



        [HttpPost]
        public async Task<ActionResult> AddDetail(OrderDetailDetailDtoModel request)
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
            _sudeSessionContext.CurrentOrderDetails=null;
            OrderNewDtoModel order = new OrderNewDtoModel();
            order.WorkId = CurrentWorkId;
            order.IsBuy = false;
            if (!string.IsNullOrEmpty(CurrentWorkId))
            {
                order.OrderDate = DateTime.Now;
                ResultSetDto<IEnumerable<CustomerDetailDtoModel>> Customerslist = await Api.GetHandler
          .GetApiAsync<ResultSetDto<IEnumerable<CustomerDetailDtoModel>>>(ApiAddress.Customer.GetCustomersByWorkId + CurrentWorkId);

                SelectList selectLists = null;
                if (Customerslist != null && Customerslist.Data != null)
                    selectLists = new SelectList(Customerslist.Data as ICollection<CustomerDetailDtoModel>, "CustomerId", "Title");
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
            if (orderDetailNewDtoSession == null || orderDetailNewDtoSession.Count() <= 0)
            {
                return Json(new ResultSetDto()
                {
                    IsSucceed = false,
                    Message = Constants.Messages.InsertOrderDetail
                });

            }

            List<OrderDetailDetailDtoModel> orderDetails = orderDetailNewDtoSession.ToList();
            if (orderDetails != null)
            {
                request.OrderDetails = orderDetails;
            }

          
           var type= await Api.GetHandler
             .GetApiAsync<ResultSetDto<TypeDetailDtoModel>>(ApiAddress.Type.GetTypeByKey+ Constants.PaymenStatus.NotPaid);
            if (type != null && type.Data != null)
                request.PaymentStatusId = type.Data.TypeId;
            string CurrentWorkId = _sudeSessionContext.CurrentWorkId;
            request.WorkId = CurrentWorkId;
            request.IsBuy = false;
            ResultSetDto<OrderNewDtoModel> result = await Api.GetHandler
             .GetApiAsync<ResultSetDto<OrderNewDtoModel>>(ApiAddress.Order.AddOrder, request);
            if (!result.IsSucceed)
            {
                return Json(new ResultSetDto()
                {
                    IsSucceed = false,
                    Message = result.Message
                });


            }

            _sudeSessionContext.CurrentOrderDetails= null;

            return Json(result);

        }

        [HttpGet]//("{WorkTypeId}")]
        public async Task<ActionResult> Edit(string id)
        {


            _sudeSessionContext.CurrentOrderDetails= null;



            ResultSetDto<OrderDetailDtoModel> result = await Api.GetHandler
                .GetApiAsync<ResultSetDto<OrderDetailDtoModel>>(ApiAddress.Order.GetOrderById + id);
            if (result.Data.OrderDetails != null)
                _sudeSessionContext.CurrentOrderDetails= result.Data.OrderDetails.AsEnumerable();

            ResultSetDto<IEnumerable<CustomerDetailDtoModel>> Customerslist = await Api.GetHandler
    .GetApiAsync<ResultSetDto<IEnumerable<CustomerDetailDtoModel>>>(ApiAddress.Customer.GetCustomersByWorkId + result.Data.WorkId);


            ResultSetDto<IEnumerable<TypeDetailDtoModel>> PaymentStatus = await Api.GetHandler
   .GetApiAsync<ResultSetDto<IEnumerable<TypeDetailDtoModel>>>(ApiAddress.Type.GetTypesByGroupKey+Constants.GroupType.PaymentStatus);


            SelectList selectLists = new SelectList(Customerslist.Data as ICollection<CustomerDetailDtoModel>, "CustomerId", "Title");

    
                        ViewData[Constants.ViewBagNames.PaymentStatus] = PaymentStatus.Data;
            ViewData[Constants.ViewBagNames.Customers] = selectLists;
            return PartialView(viewName: "Edit", model: new OrderEditDtoModel()
            {
                OrderId = result.Data.OrderId,
                CustomerId = result.Data.Customer.CustomerId,
                CustomerName = result.Data.Customer.Title,
                Description = result.Data.Description,
                OrderDate = result.Data.OrderDate,
                OrderNumber = result.Data.OrderNumber,
                WorkId = result.Data.WorkId,
                 PaymentStatusId=result.Data.PaymentStatusId,
                  IsBuy=result.Data.IsBuy
                  


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
            if (orderDetailNewDtoSession == null || orderDetailNewDtoSession.Count() <= 0)
            {
                return Json(new ResultSetDto()
                {
                    IsSucceed = false,
                    Message = Constants.Messages.InsertOrderDetail
                });

            }

            List<OrderDetailDetailDtoModel> orderDetails = orderDetailNewDtoSession.ToList();
            if (orderDetails != null)
            {
                request.OrderDetails = orderDetails;
            }
            //string CurrentWorkId = HttpContext.Session.GetString("CurrentWorkId");
            //request.WorkId = CurrentWorkId;

            request.IsBuy = false;

            ResultSetDto<OrderEditDtoModel> result = await Api.GetHandler
                .GetApiAsync<ResultSetDto<OrderEditDtoModel>>(ApiAddress.Order.EditOrder, request);
            if(result.IsSucceed)
            _sudeSessionContext.CurrentOrderDetails= null;
            return Json(result);

        }

        [HttpGet]
        public async Task<ActionResult> Detail(string id)
        {
            ResultSetDto<OrderDetailDtoModel> result = await Api.GetHandler
             .GetApiAsync<ResultSetDto<OrderDetailDtoModel>>(ApiAddress.Order.GetOrderById + id);

            var orderDetail = result.Data;

            return View(viewName: "Detail", model: orderDetail);
        }

        public async Task<ActionResult> SetPayment(string orderId,string typeId)
        {

            ResultSetDto<OrderDetailDtoModel> orderDetailDto = await Api.GetHandler
                .GetApiAsync<ResultSetDto<OrderDetailDtoModel>>(ApiAddress.Order.GetOrderById+ orderId);
            if(!orderDetailDto.IsSucceed && orderDetailDto.Data==null)
            {
                return Json(new ResultSetDto()
                {
                    IsSucceed = false,
                    Message = Constants.Messages.OrderNotFound
                });



            }


            OrderEditDtoModel request = new OrderEditDtoModel();
            request.OrderId = orderDetailDto.Data.OrderId;
            request.PaymentStatusId = typeId;




            ResultSetDto<WorkNewDtoModel> result = await Api.GetHandler
                .GetApiAsync<ResultSetDto<WorkNewDtoModel>>(ApiAddress.Order.SetPayment, request);
            if (!result.IsSucceed)
            {
                return Json(new ResultSetDto()
                {
                    IsSucceed = false,
                    Message = result.Message
                });


            }

            return Json(result);


         
        }
        public async Task<ActionResult> Delete(string id)
        {
            ResultSetDto result = await Api.GetHandler
              .GetApiAsync<ResultSetDto>(ApiAddress.Order.DeleteOrder, id);

            return Json(result);
        }
    }
}
