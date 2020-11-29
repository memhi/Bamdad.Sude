using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sude.Application.Interfaces;
using Sude.Application.Result;
using Sude.Domain.Models.Serving;
using Sude.Dto.DtoModels.Result;
using Sude.Dto.DtoModels.Serving;

namespace Sude.Api.Controllers
{
    [Route("api/[controller]/[action]/")]
    [ApiController]
    public class ServingController : ControllerBase
    {
        private readonly IServingService _servingService;
        private readonly IServingInventoryService _servingInventoryService;
        private readonly IWorkService _WorkService;
        public ServingController(IServingService servingService, IWorkService workService, IServingInventoryService servingInventoryService)
        {
            _servingService = servingService;
            _servingInventoryService = servingInventoryService;
            _WorkService = workService;
        }

        //GET : api/GetServings
        [HttpGet]
        //[Consumes("application/xml")]
        //[Consumes("application/json")]
       // [Authorize]
        public async Task<ActionResult> GetServings()
        {

       

            try
            {
                ResultSet<IEnumerable<ServingInfo>> resultSet = await _servingService.GetServingsAsync();
                if (resultSet == null || resultSet.Data == null || !resultSet.Data.Any())
                    return NotFound(new ResultSetDto<IEnumerable<ServingDetailDtoModel>>()
                    {
                        IsSucceed = false,
                        Message = "Not found",
                        Data = null


                    });


                var result = resultSet.Data.Select(s => new ServingDetailDtoModel()
                {
                    ServingId = s.Id.ToString(),
                    Title = s.Title,
                    Price = s.Price,
                    Desc = s.Desc,
                    IsActive = s.IsActive,
                    HasInventoryTracking = s.HasInventoryTracking,
                    WorkId = s.Work.Id.ToString(),
                    WorkName = s.Work.Title
                });
                return Ok(new ResultSetDto<IEnumerable<ServingDetailDtoModel>>()
                {
                    IsSucceed = true,
                    Message = "",
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResultSetDto<IEnumerable<ServingDetailDtoModel>>()
                {
                    IsSucceed = false,
                    Message = ex.Message,
                    Data = null
                });
            }
        }


        [HttpGet("{workId}")]
        //[Consumes("application/xml")]
        //[Consumes("application/json")]
        // [Authorize]
        public async Task<ActionResult> GetServingsByWorkId(string workId)
        {

            try
            {
                Guid wid;
                if (Guid.TryParse(workId, out wid))
                {
                    ResultSet<IEnumerable<ServingInfo>> resultSet = await _servingService.GetServingsByWorkIdAsync(wid);
                    if (resultSet == null || resultSet.Data == null || !resultSet.Data.Any())
                        return NotFound(new ResultSetDto<IEnumerable<ServingDetailDtoModel>>()
                        {
                            IsSucceed = false,
                            Message = "Not found",
                            Data = null


                        });


                    var result = resultSet.Data.Select(s => new ServingDetailDtoModel()
                    {
                        ServingId = s.Id.ToString(),
                        Title = s.Title,
                        Price = s.Price,
                        Desc = s.Desc,
                        IsActive = s.IsActive,
                        HasInventoryTracking = s.HasInventoryTracking,
                        WorkId = s.Work.Id.ToString(),
                        WorkName = s.Work.Title
                    });
                    return Ok(new ResultSetDto<IEnumerable<ServingDetailDtoModel>>()
                    {
                        IsSucceed = true,
                        Message = "",
                        Data = result
                    });

                }
                else
                {
                    return NotFound(new ResultSetDto<IEnumerable<ServingDetailDtoModel>>()
                    {
                        IsSucceed = false,
                        Message = "اطلاعات خدمات کسب و کار موجود نیست",
                        Data = null
                    });

                }
            }
            catch (Exception ex)
            {
                return BadRequest(new ResultSetDto<IEnumerable<ServingDetailDtoModel>>()
                {
                    IsSucceed = false,
                    Message = ex.Message,
                    Data = null
                });
            }
        }


        //GET : api/GetServing/0
        [HttpGet("{servingId}")]
        public async Task<ActionResult> GetServingById(string servingId)
        {
            try
            {
                var serving = (await _servingService.GetServingByIdAsync(Guid.Parse(servingId))).Data;
                if (serving == null)
                    return NotFound(new ResultSetDto<ServingDetailDtoModel>()
                    {
                        IsSucceed = false,
                        Message = "Not found",
                        Data = null


                    });


                var result = new ServingDetailDtoModel()
                {
                    ServingId = serving.Id.ToString(),
                    Title = serving.Title,
                    Price = serving.Price,
                    Desc = serving.Desc,
                    IsActive = serving.IsActive,
                    HasInventoryTracking = serving.HasInventoryTracking,
                    WorkId = serving.Work.Id.ToString(),
                    WorkName = serving.Work.Title
                };

                return Ok(new ResultSetDto<ServingDetailDtoModel>()
                {
                    IsSucceed = true,
                    Message = "",
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResultSetDto<ServingDetailDtoModel>()
                {
                    IsSucceed = false,
                    Message = ex.Message,
                    Data = null
                });
            }
        }

        [HttpPost]
        public async Task<ActionResult<ResultSetDto<ServingEditDtoModel>>> EditServing([FromBody]ServingEditDtoModel request)
        {
            if (!ModelState.IsValid)
            {
                string message = "";
                foreach (var er in ModelState.Values.SelectMany(modelstate=> modelstate.Errors))
                        message += er.ErrorMessage + " \n";

                return BadRequest(new ResultSetDto()
                {
                    IsSucceed = false,
                    Message = message
                });
            }

            try
            {
                var resultWork = await _WorkService.GetWorkByIdAsync(Guid.Parse(request.WorkId));
                if (resultWork == null || resultWork.Data == null || !resultWork.IsSucceed)
                    return BadRequest(new ResultSetDto<ServingEditDtoModel>()
                    {
                        IsSucceed = false,
                        Message = resultWork.Message,
                        Data = null
                    });



                var resultServing = await _servingService.GetServingByIdAsync(Guid.Parse(request.ServingId));

                if ( resultServing==null || resultServing.Data==null || !resultServing.IsSucceed)
                    return BadRequest(new ResultSetDto<ServingEditDtoModel>()
                    {
                        IsSucceed = false,
                        Message = resultServing.Message,
                        Data = null
                    });

                ServingInfo servingEdit = resultServing.Data;
                servingEdit.Title = request.Title;
                servingEdit.Price = request.Price;
                servingEdit.Desc = request.Desc;
                servingEdit.Work = resultWork.Data;
                servingEdit.IsActive = request.IsActive;
                servingEdit.HasInventoryTracking = request.HasInventoryTracking;

                var result = await _servingService.EditServingAsync(servingEdit);
                if (!result.IsSucceed)
                    return BadRequest(new ResultSetDto<ServingEditDtoModel>()
                    {
                        IsSucceed = false,
                        Message = result.Message,
                        Data = null
                    });

                return Ok(new ResultSetDto<ServingEditDtoModel>()
                {
                    IsSucceed = true,
                    Message = "",
                    Data = request
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResultSetDto<ServingEditDtoModel>()
                {
                    IsSucceed = false,
                    Message = ex.Message,
                    Data = null
                });
            }
        }

        [HttpPost]
        public async Task<ActionResult<ResultSetDto<ServingNewDtoModel>>> AddServing([FromBody] ServingNewDtoModel request)
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

                var resultWork = await _WorkService.GetWorkByIdAsync(Guid.Parse(request.WorkId));
                if (resultWork == null || resultWork.Data == null || !resultWork.IsSucceed)
                    return BadRequest(new ResultSetDto<ServingNewDtoModel>()
                    {
                        IsSucceed = false,
                        Message = resultWork.Message,
                        Data = null
                    });


                ServingInfo serving = new ServingInfo()
                {
                    Title = request.Title,
                    Price = request.Price,
                    Desc = request.Desc,
                    Work = resultWork.Data,
                    IsActive = request.IsActive,
                    HasInventoryTracking = request.HasInventoryTracking

                };

                var resultSave = await _servingService.AddServingAsync(serving);

                if (!resultSave.IsSucceed)
                    return BadRequest(new ResultSetDto<ServingNewDtoModel>()
                    {
                        IsSucceed = false,
                        Message = resultSave.Message,
                        Data = null
                    });



                request.ServingId = resultSave.Data.Id.ToString();

                if (request.HasInventoryTracking)
                {

                    ServingInventoryInfo servingInventory = new ServingInventoryInfo()
                    {
                        CurrentInventory = 0,
                        ServingId = resultSave.Data.Id,
                        Description = ""
                    };
                    var resultsaveservinginventory = await _servingInventoryService.AddServingInventoryAsync(servingInventory);
                    if (!resultsaveservinginventory.IsSucceed)
                    {
                        return BadRequest(new ResultSetDto<ServingNewDtoModel>()
                        {
                            IsSucceed = false,
                            Message = resultsaveservinginventory.Message,
                            Data = null
                        });

                    }
                }


                return Ok(new ResultSetDto<ServingNewDtoModel>()
                {
                    IsSucceed = true,
                    Message = "",
                    Data = request
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResultSetDto<ServingNewDtoModel>()
                {
                    IsSucceed = false,
                    Message = ex.Message,
                    Data = null
                });
            }
        }


        [HttpPost]
        public async Task<ActionResult<ResultSetDto<ServingNewDtoModel>>> DeleteServing([FromBody] string request)
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

                var result = await _servingService.DeleteServingAsync(Guid.Parse(request));

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
                return BadRequest(new ResultSetDto()
                {
                    IsSucceed = false,
                    Message = ex.Message
               
                });
            }
        }


    }
}

