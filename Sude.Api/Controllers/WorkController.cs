﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sude.Application.Interfaces;
using Sude.Application.Result;
using Sude.Application.Services;
using Sude.Domain.Models.Work;
using Sude.Dto.DtoModels.Result;
using Sude.Dto.DtoModels.Work;

namespace Sude.Api.Controllers
{
    [Route("api/[controller]/[action]/")]
    [ApiController]
    public class WorkController : ControllerBase
    {
        private readonly IWorkService _WorkService;
        private readonly IWorkTypeService _WorkTypeService;
        public WorkController(IWorkService WorkService,IWorkTypeService WorkTypeService)
        {
            _WorkService = WorkService;
            _WorkTypeService = WorkTypeService;
        }

        //GET : api/GetWorks
        [HttpGet]
        //[Consumes("application/xml")]
        //[Consumes("application/json")]
       // [Authorize]
        public async Task<ActionResult> GetWorks()
        {
            try
            {
                ResultSet<IEnumerable<WorkInfo>> resultSet = await _WorkService.GetWorksAsync();
                if (resultSet == null)
                    NotFound();

                var result = resultSet.Data.Select(wt => new WorkDetailDtoModel()
                {
                    WorkId = wt.Id.ToString(),
                    Title = wt.Title,
                    WorkTypeId = wt.WorkType.Id.ToString(),
                    WorkTypeName = wt.WorkType.Title,
                    Desc = wt.Desc
                }) ;
                return Ok(new ResultSetDto<IEnumerable<WorkDetailDtoModel>>()
                {
                    IsSucceed = true,
                    Message = "",
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return Ok(new ResultSetDto<IEnumerable<WorkDetailDtoModel>>()
                {
                    IsSucceed = false,
                    Message = ex.Message+"&&&"+ ex.StackTrace,
                    Data = null
                });
            }
        }

        //GET : api/GetWork/0
        [HttpGet("{WorkId}")]
        public async Task<ActionResult> GetWorkById(string WorkId)
        {
            var Work = (await _WorkService.GetWorkByIdAsync(Guid.Parse(WorkId))).Data;
            if (Work == null)
                NotFound();

            var result = new WorkDetailDtoModel()
            {
                WorkId = Work.Id.ToString(),
                Title = Work.Title,
                WorkTypeId=Work.WorkType.Id.ToString(),
                WorkTypeName=Work.WorkType.Title,

                Desc = Work.Desc
            };

            return Ok(new ResultSetDto<WorkDetailDtoModel>()
            {
                IsSucceed = true,
                Message = "",
                Data = result
            });
        }

        [HttpPost]
        public async Task<ActionResult<ResultSetDto<WorkEditDtoModel>>> EditWork([FromBody]WorkEditDtoModel request)
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

           


            var resultWork  = await _WorkService.GetWorkByIdAsync(Guid.Parse(request.WorkId));
           
            if (!resultWork.IsSucceed)
                return BadRequest(resultWork.Message);

            var resultTypeWork = await _WorkTypeService.GetWorkTypeByIdAsync(Guid.Parse(request.WorkTypeId));

            if (!resultTypeWork.IsSucceed)
                return BadRequest(resultTypeWork.Message);

            WorkInfo WorkEdit = resultWork.Data;
            WorkEdit.Title = request.Title;         
            WorkEdit.Desc = request.Desc;
            WorkEdit.WorkType = resultTypeWork.Data;
        
             

            var result = await _WorkService.EditWorkAsync(WorkEdit);
            if(!result.IsSucceed)
                return BadRequest(result.Message);

            return Ok(new ResultSetDto<WorkEditDtoModel>()
            {
                IsSucceed = true,
                Message = "",
                Data = request
            });
        }

        [HttpPost]
        public async Task<ActionResult<ResultSetDto<WorkNewDtoModel>>> AddWork([FromBody] WorkNewDtoModel request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var resultTypeWork = await _WorkTypeService.GetWorkTypeByIdAsync(Guid.Parse(request.WorkTypeId));

            if (!resultTypeWork.IsSucceed)
                return BadRequest(resultTypeWork.Message);

            WorkInfo Work = new WorkInfo()
            {
                Title = request.Title,
                Desc = request.Desc,
                WorkType = resultTypeWork.Data
              
            };



            var resultSave = await _WorkService.AddWorkAsync(Work);

            if (!resultSave.IsSucceed)
                return BadRequest(resultSave.Message);

           

            request.WorkId = resultSave.Data.Id.ToString();

            return Ok(new ResultSetDto<WorkNewDtoModel>()
            {
                IsSucceed = true,
                Message = "",
                Data = request
            });
        }


        [HttpPost]
        public async Task<ActionResult<ResultSetDto<WorkNewDtoModel>>> DeleteWork([FromBody] string request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

           

            var result = await _WorkService.DeleteWorkAsync(Guid.Parse(request));

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

