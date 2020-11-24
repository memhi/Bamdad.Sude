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
    public class BlogController : BaseAdminController
    {
        // GET: BlogController
        [HttpGet]
        //   [Authorize]
        public async Task<ActionResult> Index()
        {
            ResultSetDto<IEnumerable<BlogDetailDtoModel>> Bloglist = await Api.GetHandler
                         .GetApiAsync<ResultSetDto<IEnumerable<BlogDetailDtoModel>>>(ApiAddress.Blog.GetBlogs);

            return View();
        }


        [HttpGet]
        // [Authorize]
        public async Task<ActionResult> SetDefaultBlog(string BlogId)
        {


            HttpContext.Session.SetString("CurrentBlogId", BlogId);


            return Json(BlogId);
        }


        [HttpGet]
        // [Authorize]
        public async Task<ActionResult> List()
        {
            //System.Threading.Thread.Sleep(1000);

            ResultSetDto<IEnumerable<BlogDetailDtoModel>> Bloglist = await Api.GetHandler
                .GetApiAsync<ResultSetDto<IEnumerable<BlogDetailDtoModel>>>(ApiAddress.Blog.GetBlogs);


            return PartialView("BlogList", Bloglist);
        }

        [HttpGet]
        public async Task<ActionResult> Add()
        {



            return PartialView("Add");
        }

        [HttpPost]
        public async Task<ActionResult> Add(BlogNewDtoModel request)
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

            ResultSetDto<BlogNewDtoModel> result = await Api.GetHandler
                .GetApiAsync<ResultSetDto<BlogNewDtoModel>>(ApiAddress.Blog.AddBlog, request);


            return Json(result);

        }

        [HttpGet]//("{BlogId}")]
        public async Task<ActionResult> Edit(string id)
        {
            ResultSetDto<BlogDetailDtoModel> result = await Api.GetHandler
                .GetApiAsync<ResultSetDto<BlogDetailDtoModel>>(ApiAddress.Blog.GetBlogById + id);

            return PartialView(viewName: "Edit", model: new BlogEditDtoModel()
            {
                BlogId = result.Data.BlogId,
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
        public async Task<ActionResult> Edit(BlogEditDtoModel request)
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

            ResultSetDto<BlogEditDtoModel> result = await Api.GetHandler
                .GetApiAsync<ResultSetDto<BlogEditDtoModel>>(ApiAddress.Blog.EditBlog, request);

            return Json(result);

        }

        public async Task<ActionResult> Detail(string id)
        {
            ResultSetDto<BlogDetailDtoModel> result = await Api.GetHandler
             .GetApiAsync<ResultSetDto<BlogDetailDtoModel>>(ApiAddress.Blog.GetBlogById + id);

            var BlogDetail = result.Data;

            return View(viewName: "Detail", model: BlogDetail);
        }


        public async Task<ActionResult> Delete(string id)
        {
            ResultSetDto result = await Api.GetHandler
              .GetApiAsync<ResultSetDto>(ApiAddress.Blog.DeleteBlog, id);

            return Json(result);
        }
    }
}
