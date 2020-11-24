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
    public class HomePageNewsViewComponent: ViewComponent
    {


        public HomePageNewsViewComponent()
        {
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {

            ResultSetDto<IEnumerable<NewsDetailDtoModel>> newsList = await Api.GetHandler
       .GetApiAsync<ResultSetDto<IEnumerable<NewsDetailDtoModel>>>(ApiAddress.News.GetHomePageNews);          


            return View(newsList.Data);
        }
    }
}
