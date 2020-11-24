using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using Sude.Dto.DtoModels.Result;
using Sude.Dto.DtoModels.Content;
using Sude.Mvc.UI.ApiManagement;

namespace Sude.Mvc.UI.Admin.Controllers.Content
{
    public class NewsController : BaseAdminController
    {
        // GET: NewsController
        [HttpGet]
        //   [Authorize]
        public async Task<ActionResult> Index()
        {
            ResultSetDto<IEnumerable<NewsDetailDtoModel>> Newslist = await Api.GetHandler
                         .GetApiAsync<ResultSetDto<IEnumerable<NewsDetailDtoModel>>>(ApiAddress.News.GetNews);

            return View();
        }


        [HttpGet]
        // [Authorize]
        public async Task<ActionResult> SetDefaultNews(string NewsId)
        {


            HttpContext.Session.SetString("CurrentNewsId", NewsId);


            return Json(NewsId);
        }


        [HttpGet]
        // [Authorize]
        public async Task<ActionResult> List()
        {
            //System.Threading.Thread.Sleep(1000);

            ResultSetDto<IEnumerable<NewsDetailDtoModel>> Newslist = await Api.GetHandler
                .GetApiAsync<ResultSetDto<IEnumerable<NewsDetailDtoModel>>>(ApiAddress.News.GetNews);


            return PartialView("NewsList", Newslist);
        }

        [HttpGet]
        public async Task<ActionResult> Add()
        {



            return PartialView("Add");
        }

        [HttpPost]
        public async Task<ActionResult> Add(NewsNewDtoModel request)
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

            ResultSetDto<NewsNewDtoModel> result = await Api.GetHandler
                .GetApiAsync<ResultSetDto<NewsNewDtoModel>>(ApiAddress.News.AddNews, request);


            return Json(result);

        }

        [HttpGet]//("{NewsId}")]
        public async Task<ActionResult> Edit(string id)
        {
            ResultSetDto<NewsDetailDtoModel> result = await Api.GetHandler
                .GetApiAsync<ResultSetDto<NewsDetailDtoModel>>(ApiAddress.News.GetNewsById + id);

            return PartialView(viewName: "Edit", model: new NewsEditDtoModel()
            {
                NewsId = result.Data.NewsId,
                Title = result.Data.Title,
                Description = result.Data.Description,
                AllowComment = result.Data.AllowComment,
                EndDate = result.Data.EndDate,
                FullBody = result.Data.FullBody,
                IsActive = result.Data.IsActive,
                IsPublish = result.Data.IsPublish,
                MetaDescription = result.Data.MetaDescription,
                MetaKeywords = result.Data.MetaKeywords,
                MetaTitle = result.Data.MetaTitle,
                ShortBody = result.Data.ShortBody,
                StartDate = result.Data.StartDate,
                Tags = result.Data.Tags
            });
        }
        //[Route("Edit/{request}")]
        [HttpPost]
        public async Task<ActionResult> Edit(NewsEditDtoModel request)
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

            ResultSetDto<NewsEditDtoModel> result = await Api.GetHandler
                .GetApiAsync<ResultSetDto<NewsEditDtoModel>>(ApiAddress.News.EditNews, request);

            return Json(result);

        }

        public async Task<ActionResult> Detail(string id)
        {
            ResultSetDto<NewsDetailDtoModel> result = await Api.GetHandler
             .GetApiAsync<ResultSetDto<NewsDetailDtoModel>>(ApiAddress.News.GetNewsById + id);

            var NewsDetail = result.Data;

            return View(viewName: "Detail", model: NewsDetail);
        }


        public async Task<ActionResult> Delete(string id)
        {
            ResultSetDto result = await Api.GetHandler
              .GetApiAsync<ResultSetDto>(ApiAddress.News.DeleteNews, id);

            return Json(result);
        }
    }
}
