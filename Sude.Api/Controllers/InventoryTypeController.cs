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
            try
            {

                ResultSet<IEnumerable<InventoryTypeInfo>> resultSet =  await _InventoryTypeService.GetInventoryTypesAsync();

                if (resultSet == null || resultSet.Data == null || !resultSet.Data.Any())
                    return NotFound(new ResultSetDto<IEnumerable<InventoryTypeDetailDtoModel>>()
                    {
                        IsSucceed = false,
                        Message = "Not found",
                        Data = null



                    });


                var result = resultSet.Data.Select(s => new InventoryTypeDetailDtoModel()
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
            catch (Exception ex)
            {
                return BadRequest(new ResultSetDto<IEnumerable<InventoryTypeDetailDtoModel>>()
                {
                    IsSucceed = false,
                    Message = ex.Message,
                    Data = null
                });
            }
        }

        //GET : api/GetInventoryType/0
        [HttpGet("{InventoryTypeId}")]
        public async Task<ActionResult> GetInventoryTypeById(string InventoryTypeId)
        {

            try
            {
                var InventoryType = (await _InventoryTypeService.GetInventoryTypeByIdAsync(Guid.Parse(InventoryTypeId))).Data;
                if (InventoryType == null)
                    return NotFound(new ResultSetDto<InventoryTypeDetailDtoModel>()
                    {
                        IsSucceed = false,
                        Message = "Not found",
                        Data = null



                    });

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
            catch (Exception ex)
            {
                return BadRequest(new ResultSetDto<InventoryTypeDetailDtoModel>()
                {
                    IsSucceed = false,
                    Message = ex.Message,
                    Data = null
                });
            }
        }

        [HttpPost]
        public async Task<ActionResult<ResultSetDto<InventoryTypeEditDtoModel>>> EditInventoryType([FromBody] InventoryTypeEditDtoModel request)
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


                var resultInventoryType = await _InventoryTypeService.GetInventoryTypeByIdAsync(Guid.Parse(request.InventoryTypeId));

                if (resultInventoryType == null || resultInventoryType.Data== null)
                    return NotFound(new ResultSetDto<InventoryTypeDetailDtoModel>()
                    {
                        IsSucceed = false,
                        Message = "Not found",
                        Data = null



                    });


                InventoryTypeInfo InventoryTypeEdit = resultInventoryType.Data;
                InventoryTypeEdit.Title = request.Title;

                InventoryTypeEdit.Description = request.Description;

                var result = await _InventoryTypeService.EditInventoryTypeAsync(InventoryTypeEdit);
                if (!result.IsSucceed)
                {
                    return BadRequest(new ResultSetDto<InventoryTypeEditDtoModel>()
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
            catch (Exception ex)
            {
                return BadRequest(new ResultSetDto<InventoryTypeEditDtoModel>()
                {
                    IsSucceed = false,
                    Message = ex.Message,
                    Data = null
                });
            }
        }

        [HttpPost]
        public async Task<ActionResult<ResultSetDto<InventoryTypeNewDtoModel>>> AddInventoryType([FromBody] InventoryTypeNewDtoModel request)
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

                InventoryTypeInfo InventoryType = new InventoryTypeInfo()
                {
                    Title = request.Title,
                    Description = request.Description

                };

                var resultSave = await _InventoryTypeService.AddInventoryTypeAsync(InventoryType);

                if (!resultSave.IsSucceed)
                {
                    return BadRequest(new ResultSetDto<InventoryTypeNewDtoModel>()
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
            catch (Exception ex)
            {
                return BadRequest(new ResultSetDto<InventoryTypeNewDtoModel>()
                {
                    IsSucceed = false,
                    Message = ex.Message,
                    Data = null
                });
            }
        }


        [HttpPost]
        public async Task<ActionResult<ResultSetDto<InventoryTypeNewDtoModel>>> DeleteInventoryType([FromBody] string request)
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


                var result = await _InventoryTypeService.DeleteInventoryTypeAsync(Guid.Parse(request));

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
                return BadRequest(new ResultSetDto<InventoryTypeNewDtoModel>()
                {
                    IsSucceed = false,
                    Message = ex.Message,
                    Data = null
                });
            }
        }


    }
}

