﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sude.Application.Interfaces;
using Sude.Application.Result;
using Sude.Domain.Models.Localization;
using Sude.Dto.DtoModels.Localization;
using Sude.Dto.DtoModels.Result;
using Sude.Dto.DtoModels.Serving;

namespace Sude.Api.Controllers
{
    [Route("api/[controller]/[action]/")]
    [ApiController]
    public class LocalizationController : ControllerBase
    {
        private readonly ILanguageService _LanguageService;

        public LocalizationController(ILanguageService languageService)
        {
            _LanguageService = languageService;

        }


        [HttpPost]
        public async Task<ActionResult<ResultSetDto<LocalStringResourceDetailDtoModel>>> AddLocalStringResourceAsync([FromBody] LocalStringResourceDetailDtoModel request)
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

             //   var result = new LocalStringResourceDetailDtoModel();

                var resourceGetList = await _LanguageService.GetLocalStringResourcesAsync(Guid.Parse(request.LanguageId), request.ResourceName);
                if (resourceGetList.IsSucceed && resourceGetList.Data != null && resourceGetList.Data.Any())
                {

                    LocalStringResourceInfo resourceInfo = resourceGetList.Data.FirstOrDefault();
                     request = new LocalStringResourceDetailDtoModel()
                    {
                        LanguageId = resourceInfo.LanguageId.ToString(),
                        LocalStringResourceId = resourceInfo.Id.ToString(),
                        ResourceName = resourceInfo.ResourceName,
                        ResourceValue = resourceInfo.ResourceValue
                    };

                }

                else
                {
                    LocalStringResourceInfo resource = new LocalStringResourceInfo()
                    {
                        LanguageId = Guid.Parse(request.LanguageId),
                        ResourceName = request.ResourceName,
                        ResourceValue = request.ResourceValue



                    };
                    var resultSave = await _LanguageService.AddLocalStringResourceAsync(resource);
                    if (!resultSave.IsSucceed)
                    {
                        return BadRequest(new ResultSetDto<LocalStringResourceDetailDtoModel>()
                        {
                            IsSucceed = false,
                            Message = resultSave.Message,
                            Data = null
                        });

                    }
                    request.LocalStringResourceId = resultSave.Data.Id.ToString();
                }

               

              
              


              

                return Ok(new ResultSetDto<LocalStringResourceDetailDtoModel>()
                {
                    IsSucceed = true,
                    Message = "",
                    Data = request
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResultSetDto<LocalStringResourceDetailDtoModel>()
                {
                    IsSucceed = false,
                    Message = ex.Message,
                    Data = null
                });
            }
        }




        [HttpGet]
        public async Task<ActionResult> GetLanguagesAsync()
        {
            try
            {
                ResultSet<IEnumerable<LanguageInfo>> resultSet = await _LanguageService.GetLanguagesAsync();
                if (resultSet == null || resultSet.Data == null || !resultSet.Data.Any())
                    return NotFound(new ResultSetDto<IEnumerable<LanguageDetailDtoModel>>()
                    {
                        IsSucceed = false,
                        Message = "Not found",
                        Data = null
                    });

                var result = resultSet.Data.Select(b => new LanguageDetailDtoModel()
                {
                   DisplayOrder=b.DisplayOrder,
                    LanguageCulture=b.LanguageCulture,
                     LanguageId=b.Id.ToString(),
                      Name=b.Name,
                       Rtl=b.Rtl



                });

                return Ok(new ResultSetDto<IEnumerable<LanguageDetailDtoModel>>()
                {
                    IsSucceed = true,
                    Message = "",
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResultSetDto<IEnumerable<LanguageDetailDtoModel>>()
                {
                    IsSucceed = false,
                    Message = ex.Message + "&&&" + ex.StackTrace,
                    Data = null
                });
            }
        }

        [HttpGet("{languageId}")]
        public async Task<ActionResult> GetLanguageByIdAsync(string languageId)
        {
            try
            {
                ResultSet <LanguageInfo> resultSet = await _LanguageService.GetLanguageByIdAsync(Guid.Parse(languageId));
                if (resultSet == null || resultSet.Data == null)
                    return NotFound(new ResultSetDto<LanguageDetailDtoModel>()
                    {
                        IsSucceed = false,
                        Message = "Not Found",
                        Data = null
                    });

                LanguageDetailDtoModel language = new LanguageDetailDtoModel()
                {
                    DisplayOrder = resultSet.Data.DisplayOrder,
                    LanguageCulture = resultSet.Data.LanguageCulture,
                    LanguageId = resultSet.Data.Id.ToString(),
                    Name = resultSet.Data.Name,
                    Rtl = resultSet.Data.Rtl,
                    LocalStringResources = (resultSet.Data.LocalStringResources == null ? null :
                    resultSet.Data.LocalStringResources.Select(lsr => new LocalStringResourceDetailDtoModel()
                    {
                        LanguageId = lsr.LanguageId.ToString(),
                        LocalStringResourceId = lsr.Id.ToString(),
                        ResourceName = lsr.ResourceName,
                        ResourceValue = lsr.ResourceValue
                    }).ToList()
                    )
                                    };

     
                return Ok(new ResultSetDto<LanguageDetailDtoModel>()
                {
                    IsSucceed = true,
                    Message = "",
                    Data = language
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResultSetDto<LanguageDetailDtoModel>()
                {
                    IsSucceed = false,
                    Message = ex.Message,
                    Data = null
                });
            }
        }

        [HttpGet("{languageId}")]
        public async Task<ActionResult> GetLocalStringResourcesByLanguageIdAsync(string languageId)
        {
            try
            {
                ResultSet<IEnumerable<LocalStringResourceInfo>> resultSet = await _LanguageService.GetLocalStringResourcesAsync(Guid.Parse(languageId));

                if (resultSet == null || resultSet.Data == null || !resultSet.Data.Any())
                    return NotFound(new ResultSetDto<IEnumerable<LocalStringResourceDetailDtoModel>>()
                    {
                        IsSucceed = false,
                        Message = "Not found",
                        Data = null
                    });

                var result = resultSet.Data.Select(b => new LocalStringResourceDetailDtoModel()
                {
                    LanguageId=b.LanguageId.ToString(),
                     ResourceValue=b.ResourceValue,
                      LocalStringResourceId=b.Id.ToString(),
                       ResourceName=b.ResourceName



                });

                return Ok(new ResultSetDto<IEnumerable<LocalStringResourceDetailDtoModel>>()
                {
                    IsSucceed = true,
                    Message = "",
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResultSetDto<IEnumerable<LocalStringResourceDetailDtoModel>>()
                {
                    IsSucceed = false,
                    Message = ex.Message + "&&&" + ex.StackTrace,
                    Data = null
                });
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetLocalStringResourcesAsync()
        {
            try
            {
                ResultSet<IEnumerable<LocalStringResourceInfo>> resultSet = await _LanguageService.GetLocalStringResourcesAsync();

                if (resultSet == null || resultSet.Data == null || !resultSet.Data.Any())
                    return NotFound(new ResultSetDto<IEnumerable<LocalStringResourceDetailDtoModel>>()
                    {
                        IsSucceed = false,
                        Message = "Not found",
                        Data = null
                    });

                var result = resultSet.Data.Select(b => new LocalStringResourceDetailDtoModel()
                {
                    LanguageId = b.LanguageId.ToString(),
                    ResourceValue = b.ResourceValue,
                    LocalStringResourceId = b.Id.ToString(),
                    ResourceName = b.ResourceName



                });

                return Ok(new ResultSetDto<IEnumerable<LocalStringResourceDetailDtoModel>>()
                {
                    IsSucceed = true,
                    Message = "",
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResultSetDto<IEnumerable<LocalStringResourceDetailDtoModel>>()
                {
                    IsSucceed = false,
                    Message = ex.Message + "&&&" + ex.StackTrace,
                    Data = null
                });
            }
        }


    }
}
