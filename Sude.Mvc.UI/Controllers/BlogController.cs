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

            List<BlogDetailDtoModel> BlogDetail = new List<BlogDetailDtoModel>(); ;
            if (string.IsNullOrEmpty(UrlAddress))
            {
                ResultSetDto<IEnumerable<BlogDetailDtoModel>> result = await Api.GetHandler
             .GetApiAsync<ResultSetDto<IEnumerable<BlogDetailDtoModel>>>(ApiAddress.Blog.GetBlogs);

               if(result.IsSucceed && result.Data!=null)
                {
                    foreach (BlogDetailDtoModel blog in result.Data)
                        BlogDetail.Add(blog);
                }
            }
            else
            {
                ResultSetDto<BlogDetailDtoModel> result = await Api.GetHandler
               .GetApiAsync<ResultSetDto<BlogDetailDtoModel>>(ApiAddress.Blog.GetBlogByUrl + UrlAddress);

                BlogDetail.Add(result.Data);

            }
          



                ViewBag.SitePageTitle = "مقالات";
                if (BlogDetail != null && BlogDetail.Count() == 1)
                    ViewBag.SitePageTitle = BlogDetail.First().Title;
            

            return View(viewName: "DetailByUrl", model: BlogDetail);
        }


      
    }
}
