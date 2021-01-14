using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sude.Dto.DtoModels.Result;
using Sude.Dto.DtoModels.Serving;
using Sude.Dto.DtoModels.Order;
using Sude.Dto.DtoModels.Work;
using Sude.Mvc.UI.ApiManagement;
using Microsoft.AspNetCore.Http;

namespace Sude.Mvc.UI.Admin.Controllers
{
    public class ReportWorkController : BaseAdminController
    {
        public readonly SudeSessionContext _sudeSessionContext;

        public ReportWorkController(SudeSessionContext sudeSessionContext)
        {
            _sudeSessionContext = sudeSessionContext;
     

        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {


            ViewData[Constants.ViewBagNames.OrderDateTo] = DateTime.Now.Date.AddDays(1);
            ViewData[Constants.ViewBagNames.OrderDateFrom] = DateTime.Now.Date.AddDays(-30);
            ViewData[Constants.ViewBagNames.OrderSearchType] = "0";

            return View();
        }

        [HttpGet]
        public async Task<ActionResult> SearchReport(DateTime DateFrom, DateTime DateTo, bool? IsBuy ,int PageIndex)
        {
            ResultSetDto<IEnumerable<ReportOrderDtoModel>> result = new ResultSetDto<IEnumerable<ReportOrderDtoModel>>();
            if (string.IsNullOrEmpty(_sudeSessionContext.CurrentWorkId))
            {
                return PartialView( viewName:"SearchReport",result.Data);

            }
            ReportOrderDtoModel searchOrderDto = new ReportOrderDtoModel();
            searchOrderDto.DateFrom = DateFrom;
            searchOrderDto.DateTo = DateTo;
            searchOrderDto.WorkId = _sudeSessionContext.CurrentWorkId;
            searchOrderDto.IsBuy = IsBuy;
            searchOrderDto.PageSize = Constants.PageSize;
            searchOrderDto.PageIndex = PageIndex;
            ViewBag.PageID = PageIndex;
            ViewBag.DateFrom = DateFrom;
            ViewBag.DateTo = DateTo;
            ViewBag.IsBuy = (IsBuy==null ?"" : IsBuy.ToString());

            result = await Api.GetHandler
                                 .GetApiAsync<ResultSetDto<IEnumerable<ReportOrderDtoModel>>>(ApiAddress.Order.GetReportOrders, searchOrderDto);

            return PartialView(viewName: "SearchReport",result);



        }

        public async Task<ActionResult> OrderDetailReport(string id)
        {
            ResultSetDto<OrderDetailDtoModel> result = await Api.GetHandler
             .GetApiAsync<ResultSetDto<OrderDetailDtoModel>>(ApiAddress.Order.GetOrderById + id);

            var orderDetail = result.Data;

            return View(viewName: "OrderDetailReport", model: orderDetail);
        }

        [HttpGet]
        public async Task<ActionResult> ViewDetailsOrdersReport(DateTime Date , bool? IsBuy)
        {

            ResultSetDto<IEnumerable<OrderDetailDtoModel>> result = new ResultSetDto<IEnumerable<OrderDetailDtoModel>>();
            if (string.IsNullOrEmpty(_sudeSessionContext.CurrentWorkId))
            {
                return PartialView(viewName: "ViewDetailsOrdersReport", result.Data);

            }
            string parameters = string.Format("?workId={0}&orderDateFrom={1}&orderDateTo={2}&isBuy={3}" ,
                _sudeSessionContext.CurrentWorkId,Date.Date.ToString(),Date.Date.AddDays(1).ToString(), (IsBuy != null ? IsBuy : ""));
                ;

            result = await Api.GetHandler
                                 .GetApiAsync<ResultSetDto<IEnumerable<OrderDetailDtoModel>>>(ApiAddress.Order.GetOrdersWithDetails+parameters);

            return PartialView(viewName: "ViewDetailsOrdersReport", result);



        }


    }
}
