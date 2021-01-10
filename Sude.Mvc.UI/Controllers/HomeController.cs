using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sude.Mvc.UI.Models;
using Sude.Dto.DtoModels.Result;
using Sude.Dto.DtoModels.Serving;
using Sude.Dto.DtoModels.Order;
using Sude.Dto.DtoModels.Work;
using Sude.Mvc.UI.ApiManagement;
using Sude.Mvc.UI.Admin;

namespace Sude.Mvc.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public readonly SudeSessionContext _sudeSessionContext;
        public HomeController(ILogger<HomeController> logger ,SudeSessionContext sudeSessionContext)
        {
            _sudeSessionContext = sudeSessionContext;
            _logger = logger;
            
        }

        public async Task<ActionResult> Index()
        {
            ResultSetDto<IEnumerable<ServingDetailDtoModel>> servinglist = await Api.GetHandler
  .GetApiAsync<ResultSetDto<IEnumerable<ServingDetailDtoModel>>>(ApiAddress.Serving.GetServings);


            ResultSetDto<IEnumerable<WorkDetailDtoModel>> worklist = await Api.GetHandler
    .GetApiAsync<ResultSetDto<IEnumerable<WorkDetailDtoModel>>>(ApiAddress.Work.GetWorks);



    //        ResultSetDto<IEnumerable<OrderDetailDtoModel>> orderlist = await Api.GetHandler
    //.GetApiAsync<ResultSetDto<IEnumerable<OrderDetailDtoModel>>>(ApiAddress.Order.GetOrders);



            ViewData["WorkCount"] =((worklist  != null && worklist.Data!=null) ? worklist.Data.Count().ToString() : "0");
            ViewData["ServingCount"] = ((servinglist != null && servinglist.Data != null) ? servinglist.Data.Count().ToString() : "0");
            ViewData["OrderCount"] = 0;// ((orderlist != null && orderlist.Data != null) ? orderlist.Data.Count().ToString() : "0");

            ViewBag.SitePageTitle = "سامانه مدیریت کسب و کارهای کوچک";

            return View();
         
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
