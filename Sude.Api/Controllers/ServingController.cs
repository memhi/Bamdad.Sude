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
        public ServingController(IServingService servingService)
        {
            _servingService = servingService;
        }

        //GET : api/GetServings
        [HttpGet]
        //[Consumes("application/xml")]
        //[Consumes("application/json")]
        [Authorize]
        public async Task<ActionResult> GetServings()
        {
            var result = (await _servingService.GetServingsAsync()).Data.Select(s => new ServingDetailDtoModel()
            {
                ServingId = s.Id.ToString(),
                Title = s.Title,
                Price = s.Price,
                Desc = s.Desc
            });
            return Ok(new ResultSetDto<IEnumerable<ServingDetailDtoModel>>()
            {
                IsSuced = true,
                Message = "",
                Data = result
            });
        }

        //GET : api/GetServing/0
        [HttpGet("{servingId}")]
        public async Task<ActionResult> GetServingById(string servingId)
        {
            var serving = (await _servingService.GetServingByIdAsync(Guid.Parse(servingId))).Data;
            if (serving == null)
                NotFound();

            var result = new ServingDetailDtoModel()
            {
                ServingId = serving.Id.ToString(),
                Title = serving.Title,
                Price = serving.Price,
                Desc = serving.Desc
            };

            return Ok(new ResultSetDto<ServingDetailDtoModel>()
            {
                IsSuced = true,
                Message = "",
                Data = result
            });
        }

        [HttpPost]
        public async Task<ActionResult<ResultSetDto<ServingEditDtoModel>>> EditServing([FromBody]ServingEditDtoModel request)
        {
            if (!ModelState.IsValid)
            {
                string message = "";
                foreach (var er in ModelState.Values.SelectMany(modelstate=> modelstate.Errors))
                        message += er.ErrorMessage + " \n";

                return Ok(new ResultSetDto()
                {
                    IsSuced = false,
                    Message = message
                });
            }

            

            var resultServing  = await _servingService.GetServingByIdAsync(Guid.Parse(request.ServingId));

            if (!resultServing.IsSucced)
                return BadRequest(resultServing.Message);

            Serving servingEdit = resultServing.Data;
            servingEdit.Title = request.Title;
            servingEdit.Price = request.Price;
            servingEdit.Desc = request.Desc;

            var result = await _servingService.EditServingAsync(servingEdit);
            if(!result.IsSucced)
                return BadRequest(result.Message);

            return Ok(new ResultSetDto<ServingEditDtoModel>()
            {
                IsSuced = true,
                Message = "",
                Data = request
            });
        }

        [HttpPost]
        public async Task<ActionResult<ResultSetDto<ServingNewDtoModel>>> AddServing([FromBody] ServingNewDtoModel request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Serving serving = new Serving()
            {
                Title = request.Title,
                Price = request.Price,
                Desc = request.Desc
            };

            var resultSave = await _servingService.AddServingAsync(serving);

            if (!resultSave.IsSucced)
                return BadRequest(resultSave.Message);


            request.ServingId = resultSave.Data.Id.ToString();

            return Ok(new ResultSetDto<ServingNewDtoModel>()
            {
                IsSuced = true,
                Message = "",
                Data = request
            });
        }


        [HttpPost]
        public async Task<ActionResult<ResultSetDto<ServingNewDtoModel>>> DeleteServing([FromBody] string request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

           

            var result = await _servingService.DeleteServingAsync(Guid.Parse(request));

            if (!result.IsSucced)
                return Ok(new ResultSetDto()
                {
                    IsSuced = false,
                    Message = result.Message
                });


            return Ok(new ResultSetDto()
            {
                IsSuced = true,
                Message = ""
            });
        }


    }
}

