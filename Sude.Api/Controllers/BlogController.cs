using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sude.Application.Interfaces;
using Sude.Application.Result;
using Sude.Application.Services;

using Sude.Domain.Models.Content;
using Sude.Dto.DtoModels.Result;
using Sude.Dto.DtoModels.Content;

using Sude.Persistence.Contexts;
using Microsoft.EntityFrameworkCore.Storage;

namespace Sude.Api.Controllers
{
    [Route("api/[controller]/[action]/")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IBlogService _BlogService;


        public BlogController(IBlogService BlogService)
        {


            _BlogService = BlogService;

        }

        //GET : api/GetWorks
        [HttpGet("{blogd}")]
        //[Consumes("application/xml")]
        //[Consumes("application/json")]
        // [Authorize]


        [HttpGet]
        //[Consumes("application/xml")]
        //[Consumes("application/json")]
        // [Authorize]
        public async Task<ActionResult> GetBlogsAsync()
        {
            try
            {
                ResultSet<IEnumerable<BlogInfo>> resultSet = await _BlogService.GetBlogsAsync();
                if (resultSet == null || resultSet.Data==null || !resultSet.Data.Any())
                    return NotFound(new ResultSetDto<IEnumerable<BlogDetailDtoModel>>()
                    {
                        IsSucceed = false,
                        Message = "Not found",
                        Data = null
                    });

                var result = resultSet.Data.Select(b => new BlogDetailDtoModel()
                {
                    BlogId = b.Id.ToString(),
                    AllowComment = b.AllowComment,
                    Description = b.Description,
                    EndDate = b.EndDate,
                    FullBody = b.FullBody,
                    IsActive = b.IsActive,
                    IsPublish = b.IsPublish,
                    MetaDescription = b.MetaDescription,
                    MetaKeywords = b.MetaKeywords,
                    MetaTitle = b.MetaTitle,
                    ShortBody = b.ShortBody,
                    StartDate = b.StartDate,
                    Tags = b.Tags,
                    Title = b.Title,
                    RegDate=b.RegDate,
                     UrlBlog=b.UrlAddress



                });
    
                return Ok(new ResultSetDto<IEnumerable<BlogDetailDtoModel>>()
                {
                    IsSucceed = true,
                    Message = "",
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResultSetDto<IEnumerable<BlogDetailDtoModel>>()
                {
                    IsSucceed = false,
                    Message = ex.Message + "&&&" + ex.StackTrace,
                    Data = null
                });
            }
        }

        public ActionResult GetHomePageBlogs()
        {
            try
            {
                ResultSet<IEnumerable<BlogInfo>> resultSet = _BlogService.GetHomePageBlogs();
                if (resultSet == null || resultSet.Data == null || !resultSet.Data.Any())
                    return NotFound(new ResultSetDto<IEnumerable<BlogDetailDtoModel>>()
                    {
                        IsSucceed = false,
                        Message = "Not found",
                        Data = null
                    });


                var result = resultSet.Data.Select(b => new BlogDetailDtoModel()
                {
                    BlogId = b.Id.ToString(),
                    AllowComment = b.AllowComment,
                    Description = b.Description,
                    EndDate = b.EndDate,
                    FullBody = b.FullBody,
                    IsActive = b.IsActive,
                    IsPublish = b.IsPublish,
                    MetaDescription = b.MetaDescription,
                    MetaKeywords = b.MetaKeywords,
                    MetaTitle = b.MetaTitle,
                    ShortBody = b.ShortBody,
                    StartDate = b.StartDate,
                    Tags = b.Tags,
                    Title = b.Title,
                    RegDate = b.RegDate,
                    UrlBlog=b.UrlAddress


                });
                return Ok(new ResultSetDto<IEnumerable<BlogDetailDtoModel>>()
                {
                    IsSucceed = true,
                    Message = "",
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResultSetDto<IEnumerable<BlogDetailDtoModel>>()
                {
                    IsSucceed = false,
                    Message = ex.Message + "&&&" + ex.StackTrace,
                    Data = null
                });
            }
        }

        [HttpGet("{UrlAddress}")]
        public async Task<ActionResult> GetBlogByUrlAsync(string UrlAddress)
        {
            try
            {
                ResultSet<BlogInfo> resultSet = await _BlogService.GetBlogByUrlAsync(UrlAddress);
                if (resultSet == null || resultSet.Data == null)
                    return NotFound(new ResultSetDto<BlogDetailDtoModel>()
                    {
                        IsSucceed = false,
                        Message = "Not Found",
                        Data = null
                    });

                BlogDetailDtoModel Blog = new BlogDetailDtoModel()
                {
                    BlogId = resultSet.Data.Id.ToString(),
                    AllowComment = resultSet.Data.AllowComment,
                    Description = resultSet.Data.Description,
                    EndDate = resultSet.Data.EndDate,
                    FullBody = resultSet.Data.FullBody,
                    IsActive = resultSet.Data.IsActive,
                    IsPublish = resultSet.Data.IsPublish,
                    MetaDescription = resultSet.Data.MetaDescription,
                    MetaKeywords = resultSet.Data.MetaKeywords,
                    MetaTitle = resultSet.Data.MetaTitle,
                    ShortBody = resultSet.Data.ShortBody,
                    StartDate = resultSet.Data.StartDate,
                    Tags = resultSet.Data.Tags,
                    Title = resultSet.Data.Title,
                    RegDate = resultSet.Data.RegDate,
                    UrlBlog = resultSet.Data.UrlAddress


                };

                ;
                return Ok(new ResultSetDto<BlogDetailDtoModel>()
                {
                    IsSucceed = true,
                    Message = "",
                    Data = Blog
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResultSetDto<BlogDetailDtoModel>()
                {
                    IsSucceed = false,
                    Message = ex.Message,
                    Data = null
                });
            }
        }


        [HttpGet("{blogId}")]
        public async Task<ActionResult> GetBlogByIdAsync(string blogId )
        {
            try
            {
                ResultSet<BlogInfo> resultSet = await _BlogService.GetBlogByIdAsync(Guid.Parse(blogId));
                if (resultSet == null || resultSet.Data == null )
                    return NotFound(new ResultSetDto<BlogDetailDtoModel>()
                    {
                        IsSucceed = false,
                        Message = "Not Found",
                        Data = null
                    });

                BlogDetailDtoModel blog = new BlogDetailDtoModel()          
                {
                    BlogId = resultSet.Data.Id.ToString(),
                    AllowComment = resultSet.Data.AllowComment,
                    Description = resultSet.Data.Description,
                    EndDate = resultSet.Data.EndDate,
                    FullBody = resultSet.Data.FullBody,
                    IsActive = resultSet.Data.IsActive,
                    IsPublish = resultSet.Data.IsPublish,
                    MetaDescription = resultSet.Data.MetaDescription,
                    MetaKeywords = resultSet.Data.MetaKeywords,
                    MetaTitle = resultSet.Data.MetaTitle,
                    ShortBody = resultSet.Data.ShortBody,
                    StartDate = resultSet.Data.StartDate,
                    Tags = resultSet.Data.Tags,
                    Title = resultSet.Data.Title,
                    RegDate = resultSet.Data.RegDate,
                     UrlBlog=resultSet.Data.UrlAddress


                };
                
                ;
                return Ok(new ResultSetDto<BlogDetailDtoModel>()
                {
                    IsSucceed = true,
                    Message = "",
                    Data = blog
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResultSetDto<BlogDetailDtoModel>()
                {
                    IsSucceed = false,
                    Message = ex.Message,
                    Data = null
                });
            }
        }

        [HttpPost]
        public async Task<ActionResult<ResultSetDto<BlogEditDtoModel>>> EditBlogAsync([FromBody] BlogEditDtoModel requestBlog)
        {
            if (!ModelState.IsValid)
            {
                string message = "";
                foreach (var er in ModelState.Values.SelectMany(modelstate => modelstate.Errors))
                    message += er.ErrorMessage + " \n";

                return BadRequest(new ResultSetDto()
                {
                    IsSucceed = false,
                    Message = message
                });
            }

            try
            {


                var resultBlog = await _BlogService.GetBlogByIdAsync(Guid.Parse(requestBlog.BlogId));

                if (resultBlog == null || resultBlog.Data == null || !resultBlog.IsSucceed)
                    return NotFound(new ResultSetDto<BlogEditDtoModel>()
                    {
                        IsSucceed = false,
                        Message = resultBlog.Message,
                        Data = null
                    });

                BlogInfo BlogEdit = resultBlog.Data;

                BlogEdit.Title = requestBlog.Title;
                BlogEdit.AllowComment = requestBlog.AllowComment;
                BlogEdit.Description = requestBlog.Description;
                BlogEdit.MetaTitle = requestBlog.MetaTitle;
                BlogEdit.EndDate = requestBlog.EndDate;
                BlogEdit.ShortBody = requestBlog.ShortBody;
                BlogEdit.FullBody = requestBlog.FullBody;
                BlogEdit.IsActive = requestBlog.IsActive;
                BlogEdit.IsPublish = requestBlog.IsPublish;
                BlogEdit.MetaKeywords = requestBlog.MetaKeywords;
                BlogEdit.MetaDescription = requestBlog.MetaDescription;
                BlogEdit.StartDate = requestBlog.StartDate;
                BlogEdit.Tags = requestBlog.Tags;
                BlogEdit.Title = requestBlog.Title;
                BlogEdit.UpdateDate = DateTime.Now;

                var result = await _BlogService.EditBlogAsync(BlogEdit);
                if (!result.IsSucceed)
                    return BadRequest(new ResultSetDto<BlogEditDtoModel>()
                    {
                        IsSucceed = false,
                        Message = result.Message,
                        Data = null
                    });

                return Ok(new ResultSetDto<BlogEditDtoModel>()
                {
                    IsSucceed = true,
                    Message = "",
                    Data = requestBlog
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResultSetDto<BlogDetailDtoModel>()
                {
                    IsSucceed = false,
                    Message = ex.Message,
                    Data = null
                });
            }
        }

        [HttpPost]
        public async Task<ActionResult<ResultSetDto<BlogNewDtoModel>>> AddBlog([FromBody] BlogNewDtoModel requestBlog)
        {
            if (!ModelState.IsValid)
            {
                string message = "";
                foreach (var er in ModelState.Values.SelectMany(modelstate => modelstate.Errors))
                    message += er.ErrorMessage + " \n";

                return BadRequest(new ResultSetDto()
                {
                    IsSucceed = false,
                    Message = message
                });
            }


            try
            {

                BlogInfo Blog = new BlogInfo()
                {
                    Title = requestBlog.Title,
                    AllowComment = requestBlog.AllowComment,
                    Description = requestBlog.Description,
                    MetaTitle = requestBlog.MetaTitle,
                    EndDate = requestBlog.EndDate,
                    ShortBody = requestBlog.ShortBody,
                    FullBody = requestBlog.FullBody,
                    IsActive = requestBlog.IsActive,
                    IsPublish = requestBlog.IsPublish,
                    MetaKeywords = requestBlog.MetaKeywords,
                    MetaDescription = requestBlog.MetaDescription,
                    StartDate = requestBlog.StartDate,
                    Tags = requestBlog.Tags,
                    RegDate = DateTime.Now,
                    UrlAddress = Guid.NewGuid().ToString()



            };
           

                var resultSave = await _BlogService.AddBlogAsync(Blog);

                if (!resultSave.IsSucceed)
                    return BadRequest(new ResultSetDto<BlogNewDtoModel>()
                    {
                        IsSucceed = false,
                        Message = resultSave.Message,
                        Data = null
                    });



                requestBlog.BlogId = resultSave.Data.Id.ToString();




                return Ok(new ResultSetDto<BlogNewDtoModel>()
                {
                    IsSucceed = true,
                    Message = "",
                    Data = requestBlog
                });

            }
            catch (Exception ex)
            {
                return BadRequest(new ResultSetDto<BlogDetailDtoModel>()
                {
                    IsSucceed = false,
                    Message = ex.Message,
                    Data = null
                });
            }
        }


        [HttpPost]
        public async Task<ActionResult<ResultSetDto<BlogNewDtoModel>>> DeleteBlog([FromBody] string blogId)
        {
            if (!ModelState.IsValid)
            {
                string message = "";
                foreach (var er in ModelState.Values.SelectMany(modelstate => modelstate.Errors))
                    message += er.ErrorMessage + " \n";

                return BadRequest(new ResultSetDto()
                {
                    IsSucceed = false,
                    Message = message
                });
            }


            try
            {

                var result = await _BlogService.DeleteBlogAsync(Guid.Parse(blogId));

                if (!result.IsSucceed)
                    return BadRequest(new ResultSetDto()
                    {
                        IsSucceed = false,
                        Message = result.Message
                    });


                return Ok(new ResultSetDto()
                {
                    IsSucceed = true,
                    Message = ""
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResultSetDto<BlogDetailDtoModel>()
                {
                    IsSucceed = false,
                    Message = ex.Message,
                    Data = null
                });
            }
        }

    }
}

