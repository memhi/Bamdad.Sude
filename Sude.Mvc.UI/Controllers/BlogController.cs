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
using Sude.Dto.DtoModels.Content;

namespace Sude.Mvc.UI.Controllers
{
    public class BlogController : Controller
    {
        private readonly ILogger<BlogController> _logger;

        public BlogController(ILogger<BlogController> logger)
        {
            _logger = logger;
        }

        public async Task<ActionResult> DetailByUrl(string UrlAddress)
        {
            ResultSetDto<BlogDetailDtoModel> result = await Api.GetHandler
             .GetApiAsync<ResultSetDto<BlogDetailDtoModel>>(ApiAddress.Blog.GetBlogByUrl + UrlAddress);

            var BlogDetail = result.Data;

            return View(viewName: "DetailByUrl", model: BlogDetail);
        }


      
    }
}
