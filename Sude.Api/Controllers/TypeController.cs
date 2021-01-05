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
using Sude.Domain.Models.Type;
using Sude.Domain.Models.Work;
using Sude.Dto.DtoModels.Result;
using Sude.Dto.DtoModels.Type;
using Sude.Dto.DtoModels.Work;

namespace Sude.Api.Controllers
{
    [Route("api/[controller]/[action]/")]
    [ApiController]
    public class TypeController : ControllerBase
    {
        private readonly ITypeService _TypeService;
 
 
        public TypeController(ITypeService typeService)
        {
            _TypeService = typeService;
        
        }

        [HttpGet("{typeKey}")]
        public async Task<ActionResult> GetTypeByKey(string typeKey)
        {
            try
            {
                ResultSet<TypeInfo> resultSet = await _TypeService.GetTypeByKeyAsync(typeKey);
                if (resultSet == null || resultSet.Data == null)
                    return NotFound(new ResultSetDto<TypeDetailDtoModel>()
                    {
                        IsSucceed = false,
                        Message = "Not found",
                        Data = null


                    });

              


                var result =  new TypeDetailDtoModel()
                {
                    TypeId = resultSet.Data.Id.ToString(),
                    Title = resultSet.Data.TypeTitle,
                     Key = resultSet.Data.TypeKey,
                      TypeGroupId = resultSet.Data.TypeGroupId.ToString()
                  
                };
                return Ok(new ResultSetDto<TypeDetailDtoModel>()
                {
                    IsSucceed = true,
                    Message = "",
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResultSetDto<TypeDetailDtoModel>()
                {
                    IsSucceed = false,
                    Message = ex.Message ,
                    Data = null
                });
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetTypeById(string id)
        {
            try
            {
                ResultSet<TypeInfo> resultSet = await _TypeService.GetTypeByIdAsync(Guid.Parse(id));
                if (resultSet == null || resultSet.Data == null)
                    return NotFound(new ResultSetDto<TypeDetailDtoModel>()
                    {
                        IsSucceed = false,
                        Message = "Not found",
                        Data = null


                    });




                var result = new TypeDetailDtoModel()
                {
                    TypeId = resultSet.Data.Id.ToString(),
                    Title = resultSet.Data.TypeTitle,
                    Key = resultSet.Data.TypeKey,
                    TypeGroupId = resultSet.Data.TypeGroupId.ToString()

                };
                return Ok(new ResultSetDto<TypeDetailDtoModel>()
                {
                    IsSucceed = true,
                    Message = "",
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResultSetDto<TypeDetailDtoModel>()
                {
                    IsSucceed = false,
                    Message = ex.Message,
                    Data = null
                });
            }
        }

        //GET : api/GetWork/0



        [HttpGet("{groupKey}")]
        public async Task<ActionResult> GetTypesByGroupKey(string groupKey)
        {
            try
            {
                ResultSet<IEnumerable<TypeInfo>> resultSet = await _TypeService.GetTypesByGroupKeyAsync(groupKey);
                if (resultSet == null || resultSet.Data == null || !resultSet.Data.Any())
                    return NotFound(new ResultSet<IEnumerable<TypeDetailDtoModel>>()
                    {
                        IsSucceed = false,
                        Message = "Not found",
                        Data = null


                    });


                var result = resultSet.Data.Select(t => new TypeDetailDtoModel()
                {
                    TypeId = t.Id.ToString(),
                    Title = t.TypeTitle,
                    Key = t.TypeKey,
                    TypeGroupId = t.TypeGroupId.ToString()
                });
                return Ok(new ResultSet<IEnumerable<TypeDetailDtoModel>>()
                {
                    IsSucceed = true,
                    Message = "",
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResultSet<IEnumerable<TypeDetailDtoModel>>()
                {
                    IsSucceed = false,
                    Message = ex.Message,
                    Data = null
                });
            }

        }


    }
}

