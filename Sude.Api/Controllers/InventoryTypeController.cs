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
    public class InventoryTypeController : ControllerBase
    {
        private readonly IInventoryTypeService _InventoryTypeService;
        public InventoryTypeController(IInventoryTypeService InventoryTypeService)
        {
            _InventoryTypeService = InventoryTypeService;
        }

        //GET : api/GetInventoryTypes
        [HttpGet]
        //[Consumes("application/xml")]
        //[Consumes("application/json")]
       // [Authorize]
        public async Task<ActionResult> GetInventoryTypes()
        {
            var result = (await _InventoryTypeService.GetInventoryTypesAsync()).Data.Select(s => new InventoryTypeDetailDtoModel()
            {
                 InventoryTypeId = s.Id.ToString(),
                Title = s.Title,               
                Description = s.Description
            });
            return Ok(new ResultSetDto<IEnumerable<InventoryTypeDetailDtoModel>>()
            {
                IsSucceed = true,
                Message = "",
                Data = result
            });
        }

        //GET : api/GetInventoryType/0
        [HttpGet("{InventoryTypeId}")]
        public async Task<ActionResult> GetInventoryTypeById(string InventoryTypeId)
        {
            var InventoryType = (await _InventoryTypeService.GetInventoryTypeByIdAsync(Guid.Parse(InventoryTypeId))).Data;
            if (InventoryType == null)
                NotFound();

            var result = new InventoryTypeDetailDtoModel()
            {
                InventoryTypeId = InventoryType.Id.ToString(),
                Title = InventoryType.Title,               
                Description = InventoryType.Description
            };

            return Ok(new ResultSetDto<InventoryTypeDetailDtoModel>()
            {
                IsSucceed = true,
                Message = "",
                Data = result
            });
        }

        [HttpPost]
        public async Task<ActionResult<ResultSetDto<InventoryTypeEditDtoModel>>> EditInventoryType([FromBody] InventoryTypeEditDtoModel request)
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



            var resultInventoryType = await _InventoryTypeService.GetInventoryTypeByIdAsync(Guid.Parse(request.InventoryTypeId));

      
      

            InventoryTypeInfo InventoryTypeEdit = resultInventoryType.Data;
            InventoryTypeEdit.Title = request.Title;
 
            InventoryTypeEdit.Description = request.Description;

            var result = await _InventoryTypeService.EditInventoryTypeAsync(InventoryTypeEdit);
            if (!result.IsSucceed)
            {
                return Ok(new ResultSetDto<InventoryTypeEditDtoModel>()
                {
                    IsSucceed = false,
                    Message = result.Message,
                    Data = null
                });

            }


            return Ok(new ResultSetDto<InventoryTypeEditDtoModel>()
            {
                IsSucceed = true,
                Message = "",
                Data = request
            });
        }

        [HttpPost]
        public async Task<ActionResult<ResultSetDto<InventoryTypeNewDtoModel>>> AddInventoryType([FromBody] InventoryTypeNewDtoModel request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            InventoryTypeInfo InventoryType = new InventoryTypeInfo()
            {
                Title = request.Title,        
                Description = request.Description
              
            };

            var resultSave = await _InventoryTypeService.AddInventoryTypeAsync(InventoryType);

            if (!resultSave.IsSucceed)
            {
                return Ok(new ResultSetDto<InventoryTypeNewDtoModel>()
                {
                    IsSucceed = false,
                    Message = resultSave.Message,
                    Data = null
                });

            }
             //   return BadRequest(resultSave.Message);


            request.InventoryTypeId = resultSave.Data.Id.ToString();

            return Ok(new ResultSetDto<InventoryTypeNewDtoModel>()
            {
                IsSucceed = true,
                Message = "",
                Data = request
            });
        }


        [HttpPost]
        public async Task<ActionResult<ResultSetDto<InventoryTypeNewDtoModel>>> DeleteInventoryType([FromBody] string request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

           

            var result = await _InventoryTypeService.DeleteInventoryTypeAsync(Guid.Parse(request));

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

