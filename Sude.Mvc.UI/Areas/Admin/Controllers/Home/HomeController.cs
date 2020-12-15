﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sude.Dto.DtoModels.Result;
using Sude.Dto.DtoModels.Serving;
using Sude.Dto.DtoModels.Order;
using Sude.Dto.DtoModels.Work;
using Sude.Mvc.UI.ApiManagement;

namespace Sude.Mvc.UI.Admin.Controllers
{
    public class HomeController : BaseAdminController
    {
        public async Task<ActionResult> Index()
        {

            ResultSetDto<IEnumerable<ServingDetailDtoModel>> servinglist = await Api.GetHandler
    .GetApiAsync<ResultSetDto<IEnumerable<ServingDetailDtoModel>>>(ApiAddress.Serving.GetServings);


            ResultSetDto<IEnumerable<WorkDetailDtoModel>> worklist = await Api.GetHandler
    .GetApiAsync<ResultSetDto<IEnumerable<WorkDetailDtoModel>>>(ApiAddress.Work.GetWorks);



            ResultSetDto<IEnumerable<OrderDetailDtoModel>> orderlist = await Api.GetHandler
    .GetApiAsync<ResultSetDto<IEnumerable<OrderDetailDtoModel>>>(ApiAddress.Order.GetOrders);



            ViewData[Constants.ViewBagNames.WorkCount] = (worklist != null  && worklist.Data!=null ? worklist.Data.Count().ToString() : "0");
            ViewData[Constants.ViewBagNames.ServingCount] = (servinglist!= null && servinglist.Data != null ? servinglist.Data.Count().ToString() :"0");
            ViewData[Constants.ViewBagNames.OrderCount] = (orderlist != null && orderlist.Data != null ? orderlist.Data.Where(o=>o.IsBuy==false).ToList().Count().ToString() : "0");
            ViewData[Constants.ViewBagNames.OrderBuyCount] = (orderlist != null && orderlist.Data != null ? orderlist.Data.Where(o => o.IsBuy == true).ToList().Count().ToString() :"0");
            ViewData[Constants.ViewBagNames.SumOrderPrice] = (orderlist != null && orderlist.Data != null ? orderlist.Data.Where(o => o.IsBuy == false).ToList().Sum(op=>op.SumPrice).ToString() : "0");
            ViewData[Constants.ViewBagNames.SumOrderBuyPrice] = (orderlist != null && orderlist.Data != null ? orderlist.Data.Where(o => o.IsBuy == true).ToList().Sum(op => op.SumPrice).ToString() : "0");


            return View();
        }
    }
}
