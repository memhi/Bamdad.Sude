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

namespace Sude.Api.Controllers
{
    [Route("api/[controller]/[action]/")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _NewsService;


        public NewsController(INewsService NewsService)
        {


            _NewsService = NewsService;

        }

        //GET : api/GetWorks
        
        //[Consumes("application/xml")]
        //[Consumes("application/json")]
        // [Authorize]


        [HttpGet]
        //[Consumes("application/xml")]
        //[Consumes("application/json")]
        // [Authorize]
        public async Task<ActionResult> GetNewsAsync()
        {
            try
            {
                ResultSet<IEnumerable<NewsInfo>> resultSet = await _NewsService.GetNewsAsync();
                if (resultSet == null)
                    NotFound();

                var result = resultSet.Data.Select(b => new NewsDetailDtoModel()
                {
                    NewsId = b.Id.ToString(),
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
                    RegDate=b.RegDate


                });
                return Ok(new ResultSetDto<IEnumerable<NewsDetailDtoModel>>()
                {
                    IsSucceed = true,
                    Message = "",
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return Ok(new ResultSetDto<IEnumerable<NewsDetailDtoModel>>()
                {
                    IsSucceed = false,
                    Message = ex.Message + "&&&" + ex.StackTrace,
                    Data = null
                });
            }
        }


        public async Task<ActionResult> GetHomePageNewsAsync()
        {
            try
            {
                ResultSet<IEnumerable<NewsInfo>> resultSet =  _NewsService.GetHomePageNews();
                if (resultSet == null)
                    NotFound();

                var result = resultSet.Data.Select(b => new NewsDetailDtoModel()
                {
                    NewsId = b.Id.ToString(),
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
                    RegDate = b.RegDate


                });
                return Ok(new ResultSetDto<IEnumerable<NewsDetailDtoModel>>()
                {
                    IsSucceed = true,
                    Message = "",
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return Ok(new ResultSetDto<IEnumerable<NewsDetailDtoModel>>()
                {
                    IsSucceed = false,
                    Message = ex.Message + "&&&" + ex.StackTrace,
                    Data = null
                });
            }
        }



        [HttpGet("{NewsId}")]
        public async Task<ActionResult> GetNewsByIdAsync(string NewsId )
        {
            try
            {
                ResultSet<NewsInfo> resultSet = await _NewsService.GetNewsByIdAsync(Guid.Parse(NewsId));
                if (resultSet == null)
                    return Ok(new ResultSetDto<IEnumerable<NewsDetailDtoModel>>()
                    {
                        IsSucceed = false,
                        Message = "Not Found",
                        Data = null
                    });

                NewsDetailDtoModel News = new NewsDetailDtoModel()          
                {
                    NewsId = resultSet.Data.Id.ToString(),
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
                    RegDate = resultSet.Data.RegDate


                };
                
                ;
                return Ok(new ResultSetDto<NewsDetailDtoModel>()
                {
                    IsSucceed = true,
                    Message = "",
                    Data = News
                });
            }
            catch (Exception ex)
            {
                return Ok(new ResultSetDto<NewsDetailDtoModel>()
                {
                    IsSucceed = false,
                    Message = ex.Message + "&&&" + ex.StackTrace,
                    Data = null
                });
            }
        }

        [HttpPost]
        public async Task<ActionResult<ResultSetDto<NewsEditDtoModel>>> EditNewsAsync([FromBody] NewsEditDtoModel requestNews)
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

          


            var resultNews = await _NewsService.GetNewsByIdAsync(Guid.Parse(requestNews.NewsId));

            if (!resultNews.IsSucceed)
                return Ok(new ResultSetDto<NewsEditDtoModel>()
                {
                    IsSucceed = false,
                    Message = resultNews.Message,
                    Data = null
                });

            NewsInfo NewsEdit = resultNews.Data;

            NewsEdit.Title = requestNews.Title;
            NewsEdit.AllowComment = requestNews.AllowComment;
            NewsEdit.Description = requestNews.Description;
            NewsEdit.MetaTitle = requestNews.MetaTitle;
            NewsEdit.EndDate = requestNews.EndDate;
            NewsEdit.ShortBody = requestNews.ShortBody;
            NewsEdit.FullBody = requestNews.FullBody;
            NewsEdit.IsActive = requestNews.IsActive;
            NewsEdit.IsPublish = requestNews.IsPublish;
            NewsEdit.MetaKeywords = requestNews.MetaKeywords;
            NewsEdit.MetaDescription = requestNews.MetaDescription;
            NewsEdit.StartDate = requestNews.StartDate;
            NewsEdit.Tags = requestNews.Tags;
            NewsEdit.Title = requestNews.Title;
            NewsEdit.UpdateDate = DateTime.Now;

            var result = await _NewsService.EditNewsAsync(NewsEdit);
            if (!result.IsSucceed)
                return Ok(new ResultSetDto<NewsEditDtoModel>()
                {
                    IsSucceed = false,
                    Message = result.Message,
                    Data = null
                });

            return Ok(new ResultSetDto<NewsEditDtoModel>()
            {
                IsSucceed = true,
                Message = "",
                Data = requestNews
            });
        }

        [HttpPost]
        public async Task<ActionResult<ResultSetDto<NewsNewDtoModel>>> AddNews([FromBody] NewsNewDtoModel requestNews)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

     


            NewsInfo News = new NewsInfo()
            {
                Title = requestNews.Title,
            AllowComment = requestNews.AllowComment,
            Description = requestNews.Description,
            MetaTitle = requestNews.MetaTitle,
            EndDate = requestNews.EndDate,
            ShortBody = requestNews.ShortBody,
            FullBody = requestNews.FullBody,
            IsActive = requestNews.IsActive,
            IsPublish = requestNews.IsPublish,
            MetaKeywords = requestNews.MetaKeywords,
            MetaDescription = requestNews.MetaDescription,
            StartDate = requestNews.StartDate,
            Tags = requestNews.Tags, 
             RegDate= DateTime.Now

     

        };

            var resultSave = await _NewsService.AddNewsAsync(News);

            if (!resultSave.IsSucceed)
                return Ok(new ResultSetDto<NewsNewDtoModel>()
                {
                    IsSucceed = false,
                    Message = resultSave.Message,
                    Data = null
                });



            requestNews.NewsId = resultSave.Data.Id.ToString();

           


            return Ok(new ResultSetDto<NewsNewDtoModel>()
            {
                IsSucceed = true,
                Message = "",
                Data = requestNews
            });
        }


        [HttpPost]
        public async Task<ActionResult<ResultSetDto<NewsNewDtoModel>>> DeleteNews([FromBody] string NewsId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);



            var result = await _NewsService.DeleteNewsAsync(Guid.Parse(NewsId));

            if (!result.IsSucceed)
                return Ok(new ResultSetDto()
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

    }
}

