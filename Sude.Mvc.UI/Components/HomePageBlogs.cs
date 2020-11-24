using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sude.Dto.DtoModels.Result;
using Sude.Dto.DtoModels.Content;
using Sude.Mvc.UI.ApiManagement;

namespace Sude.Mvc.UI.Components
{
    public class HomePageBlogsViewComponent: ViewComponent
    {


        public HomePageBlogsViewComponent()
        {
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {

            ResultSetDto<IEnumerable<BlogDetailDtoModel>> blogList = await Api.GetHandler
       .GetApiAsync<ResultSetDto<IEnumerable<BlogDetailDtoModel>>>(ApiAddress.Blog.GetHomePageBlogs);          


            return View(blogList.Data);
        }
    }
}
