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
        public async Task<ActionResult> SearchReport(DateTime DateFrom, DateTime DateTo, bool? IsBuy)
        {
            ResultSetDto<IEnumerable<SearchOrderDtoModel>> result = new ResultSetDto<IEnumerable<SearchOrderDtoModel>>();
            if (string.IsNullOrEmpty(_sudeSessionContext.CurrentWorkId))
            {
                return PartialView( viewName:"SearchReport",result.Data);

            }
            SearchOrderDtoModel searchOrderDto = new SearchOrderDtoModel();
            searchOrderDto.DateFrom = DateFrom;
            searchOrderDto.DateTo = DateTo;
            searchOrderDto.WorkId = _sudeSessionContext.CurrentWorkId;
            searchOrderDto.IsBuy = IsBuy;
            searchOrderDto.PageSize = 1000;
            result = await Api.GetHandler
                                 .GetApiAsync<ResultSetDto<IEnumerable<SearchOrderDtoModel>>>(ApiAddress.Order.GetSearchOrders, searchOrderDto);

            return PartialView(viewName: "SearchReport",result.Data);



        }

    }
}
