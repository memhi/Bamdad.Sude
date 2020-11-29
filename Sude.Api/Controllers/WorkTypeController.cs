using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sude.Application.Interfaces;
using Sude.Application.Result;
using Sude.Domain.Models.Work;
using Sude.Dto.DtoModels.Result;
using Sude.Dto.DtoModels.Work;

namespace Sude.Api.Controllers
{
    [Route("api/[controller]/[action]/")]
    [ApiController]
    public class WorkTypeController : ControllerBase
    {
        private readonly IWorkTypeService _WorkTypeService;
        public WorkTypeController(IWorkTypeService WorkTypeService)
        {
            _WorkTypeService = WorkTypeService;
        }

        //GET : api/GetWorkTypes
        [HttpGet]
        //[Consumes("application/xml")]
        //[Consumes("application/json")]
       // [Authorize]
        public async Task<ActionResult> GetWorkTypes()
        {
            try
            {
                ResultSet<IEnumerable<WorkTypeInfo>> resultSet = await _WorkTypeService.GetWorkTypesAsync();
                if (resultSet == null || resultSet.Data == null || !resultSet.Data.Any())
                    return NotFound(new ResultSetDto<IEnumerable<WorkTypeDetailDtoModel>>()
                    {
                        IsSucceed = false,
                        Message = "Not found",
                        Data = null


                    });


                var result = resultSet.Data.Select(wt => new WorkTypeDetailDtoModel()
                {
                    WorkTypeId = wt.Id.ToString(),
                    Title = wt.Title,
                    Desc = wt.Desc
                });
                return Ok(new ResultSetDto<IEnumerable<WorkTypeDetailDtoModel>>()
                {
                    IsSucceed = true,
                    Message = "",
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResultSetDto<IEnumerable<WorkTypeDetailDtoModel>>()
                {
                    IsSucceed = false,
                    Message = ex.Message+"&&&"+ ex.StackTrace,
                    Data = null
                });
            }
        }

        //GET : api/GetWorkType/0
        [HttpGet("{WorkTypeId}")]
        public async Task<ActionResult> GetWorkTypeById(string WorkTypeId)
        {

            try
            {
                var WorkType = (await _WorkTypeService.GetWorkTypeByIdAsync(Guid.Parse(WorkTypeId))).Data;
                if (WorkType == null)
                    return NotFound(new ResultSetDto<WorkTypeDetailDtoModel>()
                    {
                        IsSucceed = false,
                        Message = "Not found",
                        Data = null


                    });


                var result = new WorkTypeDetailDtoModel()
                {
                    WorkTypeId = WorkType.Id.ToString(),
                    Title = WorkType.Title,

                    Desc = WorkType.Desc
                };

                return Ok(new ResultSetDto<WorkTypeDetailDtoModel>()
                {
                    IsSucceed = true,
                    Message = "",
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResultSetDto<WorkTypeDetailDtoModel>()
                {
                    IsSucceed = false,
                    Message = ex.Message,
                    Data = null
                });
            }
        }

        [HttpPost]
        public async Task<ActionResult<ResultSetDto<WorkTypeEditDtoModel>>> EditWorkType([FromBody]WorkTypeEditDtoModel request)
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

                var resultWorkType = await _WorkTypeService.GetWorkTypeByIdAsync(Guid.Parse(request.WorkTypeId));

                if ( resultWorkType.Data==null || !resultWorkType.IsSucceed)
                    return BadRequest(new ResultSetDto<WorkTypeEditDtoModel>()
                    {
                        IsSucceed = false,
                        Message = resultWorkType.Message,
                        Data = null
                    });

                WorkTypeInfo WorkTypeEdit = resultWorkType.Data;
                WorkTypeEdit.Title = request.Title;
                WorkTypeEdit.Desc = request.Desc;

                var result = await _WorkTypeService.EditWorkTypeAsync(WorkTypeEdit);
                if (!result.IsSucceed)
                    return BadRequest(new ResultSetDto<WorkTypeEditDtoModel>()
                    {
                        IsSucceed = false,
                        Message = result.Message,
                        Data = null
                    });
                return Ok(new ResultSetDto<WorkTypeEditDtoModel>()
                {
                    IsSucceed = true,
                    Message = "",
                    Data = request
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResultSetDto<WorkTypeEditDtoModel>()
                {
                    IsSucceed = false,
                    Message = ex.Message,
                    Data = null
                });
            }
        }

        [HttpPost]
        public async Task<ActionResult<ResultSetDto<WorkTypeNewDtoModel>>> AddWorkType([FromBody] WorkTypeNewDtoModel request)
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

                WorkTypeInfo WorkType = new WorkTypeInfo()
                {
                    Title = request.Title,
                    Desc = request.Desc

                };

                var resultSave = await _WorkTypeService.AddWorkTypeAsync(WorkType);

                if (!resultSave.IsSucceed)
                {
                    return BadRequest(new ResultSetDto<WorkTypeNewDtoModel>()
                    {
                        IsSucceed = false,
                        Message = resultSave.Message,
                        Data = null
                    });

                }


                request.WorkTypeId = resultSave.Data.Id.ToString();

                return Ok(new ResultSetDto<WorkTypeNewDtoModel>()
                {
                    IsSucceed = true,
                    Message = "",
                    Data = request
                });

            }
            catch (Exception ex)
            {
                return BadRequest(new ResultSetDto<WorkTypeNewDtoModel>()
                {
                    IsSucceed = false,
                    Message = ex.Message,
                    Data = null
                });
            }

        }


        [HttpPost]
        public async Task<ActionResult<ResultSetDto<WorkTypeNewDtoModel>>> DeleteWorkType([FromBody] string request)
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

                var result = await _WorkTypeService.DeleteWorkTypeAsync(Guid.Parse(request));

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

