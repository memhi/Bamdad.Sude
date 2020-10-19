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
        [Authorize]
        public async Task<ActionResult> GetWorkTypes()
        {
            var result = (await _WorkTypeService.GetWorkTypesAsync()).Data.Select(wt => new WorkTypeDetailDtoModel()
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

        //GET : api/GetWorkType/0
        [HttpGet("{WorkTypeId}")]
        public async Task<ActionResult> GetWorkTypeById(string WorkTypeId)
        {
            var WorkType = (await _WorkTypeService.GetWorkTypeByIdAsync(Guid.Parse(WorkTypeId))).Data;
            if (WorkType == null)
                NotFound();

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

        [HttpPost]
        public async Task<ActionResult<ResultSetDto<WorkTypeEditDtoModel>>> EditWorkType([FromBody]WorkTypeEditDtoModel request)
        {
            if (!ModelState.IsValid)
            {
                string message = "";
                foreach (var er in ModelState.Values.SelectMany(modelstate=> modelstate.Errors))
                        message += er.ErrorMessage + " \n";

                return Ok(new ResultSetDto()
                {
                    IsSucceed = false,
                    Message = message
                });
            }

            

            var resultWorkType  = await _WorkTypeService.GetWorkTypeByIdAsync(Guid.Parse(request.WorkTypeId));

            if (!resultWorkType.IsSucceed)
                return BadRequest(resultWorkType.Message);

            WorkTypeInfo WorkTypeEdit = resultWorkType.Data;
            WorkTypeEdit.Title = request.Title;         
            WorkTypeEdit.Desc = request.Desc;

            var result = await _WorkTypeService.EditWorkTypeAsync(WorkTypeEdit);
            if(!result.IsSucceed)
                return BadRequest(result.Message);

            return Ok(new ResultSetDto<WorkTypeEditDtoModel>()
            {
                IsSucceed = true,
                Message = "",
                Data = request
            });
        }

        [HttpPost]
        public async Task<ActionResult<ResultSetDto<WorkTypeNewDtoModel>>> AddWorkType([FromBody] WorkTypeNewDtoModel request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            WorkTypeInfo WorkType = new WorkTypeInfo()
            {
                Title = request.Title, 
                Desc = request.Desc
              
            };

            var resultSave = await _WorkTypeService.AddWorkTypeAsync(WorkType);

            if (!resultSave.IsSucceed)
                return BadRequest(resultSave.Message);


            request.WorkTypeId = resultSave.Data.Id.ToString();

            return Ok(new ResultSetDto<WorkTypeNewDtoModel>()
            {
                IsSucceed = true,
                Message = "",
                Data = request
            });
        }


        [HttpPost]
        public async Task<ActionResult<ResultSetDto<WorkTypeNewDtoModel>>> DeleteWorkType([FromBody] string request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

           

            var result = await _WorkTypeService.DeleteWorkTypeAsync(Guid.Parse(request));

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

