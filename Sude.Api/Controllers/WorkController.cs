using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sude.Application.Interfaces;
using Sude.Application.Result;
using Sude.Application.Services;
using Sude.Domain.Models.Account;
using Sude.Domain.Models.Common;
using Sude.Domain.Models.Work;
using Sude.Dto.DtoModels.Common;
using Sude.Dto.DtoModels.Content;
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

        private readonly IAttachmentService _AttachmentService;
        private readonly IAddressService _AddressService;
        private readonly ITypeService _TypeService;
        public WorkController(IWorkService WorkService, IWorkTypeService WorkTypeService,
            IAttachmentService attachmentService, IAddressService addressService, ITypeService typeService)
        {
            _WorkService = WorkService;
            _WorkTypeService = WorkTypeService;
            _AttachmentService = attachmentService;
            _AddressService = addressService;
            _TypeService = typeService;
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
                if (resultSet == null || resultSet.Data == null || !resultSet.Data.Any())
                    return NotFound(new ResultSetDto<IEnumerable<WorkDetailDtoModel>>()
                    {
                        IsSucceed = false,
                        Message = "Not found",
                        Data = null


                    });


                var result = resultSet.Data.Select(wt => new WorkDetailDtoModel()
                {
                    WorkId = wt.Id.ToString(),
                    Title = wt.Title,
                    WorkTypeId = wt.WorkType.Id.ToString(),
                    WorkTypeName = wt.WorkType.Title,
                    Desc = wt.Desc
                });
                return Ok(new ResultSetDto<IEnumerable<WorkDetailDtoModel>>()
                {
                    IsSucceed = true,
                    Message = "",
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResultSetDto<IEnumerable<WorkDetailDtoModel>>()
                {
                    IsSucceed = false,
                    Message = ex.Message,
                    Data = null
                });
            }
        }

        //GET : api/GetWork/0
        [HttpGet("{WorkId}")]
        public async Task<ActionResult> GetWorkById(string WorkId)
        {
            try
            {
                var Work = (await _WorkService.GetWorkByIdAsync(Guid.Parse(WorkId))).Data;

                if (Work == null)
                    return NotFound(new ResultSetDto<WorkDetailDtoModel>()
                    {
                        IsSucceed = false,
                        Message = "Not found",
                        Data = null


                    });

                var attachmentResult = await _AttachmentService.GetAttachmentsAsync(Work.Id, null, null);
                var addressresult = new ResultSet<AddressInfo>()
                {
                    Data = null,
                    IsSucceed = false,
                    Message = ""
                };
                if (Work.AddressId != null)
                    addressresult = await _AddressService.GetAddressByIdAsync(Work.AddressId.Value);


                var result = new WorkDetailDtoModel()
                {
                    WorkId = Work.Id.ToString(),
                    Title = Work.Title,
                    WorkTypeId = Work.WorkType.Id.ToString(),
                    WorkTypeName = Work.WorkType.Title,
                    Desc = Work.Desc,
                    Attachments = ((attachmentResult.IsSucceed && attachmentResult.Data != null) ?
                    attachmentResult.Data.Select(a => new AttachmentNewDtoModel()
                    {
                        AttachmentFileAddress = a.AttachmentFileAddress,
                        AttachmentFileType = a.AttachmentFileType,
                        AttachmentId = a.Id.ToString(),
                        Title = a.Title
                    }
                    ).ToList()
                : null),
                    Address = ((addressresult.IsSucceed && addressresult.Data != null) ?
                   new AddressDtoModel()
                   {
                       Address = addressresult.Data.Address,
                       AddressId = addressresult.Data.Id.ToString(),
                       Phone1 = addressresult.Data.Phone1,
                       Phone2 = addressresult.Data.Phone2,
                       PostalCode = addressresult.Data.PostalCode

                   } : null)
                };




                return Ok(new ResultSetDto<WorkDetailDtoModel>()
                {
                    IsSucceed = true,
                    Message = "",
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResultSetDto<WorkDetailDtoModel>()
                {
                    IsSucceed = false,
                    Message = ex.Message,
                    Data = null
                });
            }

        }

        private async Task<List<AttachmentNewDtoModel>> GetWorkAttachments(Guid workId)
        {
            var resultAttachment = await _AttachmentService.GetAttachmentsAsync(workId);
            if (resultAttachment.IsSucceed && resultAttachment.Data != null && resultAttachment.Data.Any())
            {
                return resultAttachment.Data.Select(a => new AttachmentNewDtoModel()
                {
                    AttachmentFileAddress = a.AttachmentFileAddress,
                    AttachmentFileType = a.AttachmentFileType,
                    EntityId = a.EntityId.ToString(),
                    AttachmentId = a.Id.ToString(),
                    AttachmenTypetId = a.AttachmentTypeId.ToString(),
                    Title = a.Title

                }).ToList();

            }
            return null;

        }
        [HttpGet("{UserId}")]
        public async Task<ActionResult> GetWorksByUserId(string UserId)
        {
            try
            {
                ResultSet<IEnumerable<WorkInfo>> resultSet = await _WorkService.GetWorksByUserIdAsync(Guid.Parse(UserId));
                if (resultSet == null || resultSet.Data == null || !resultSet.Data.Any())
                    return NotFound(new ResultSetDto<IEnumerable<WorkDetailDtoModel>>()
                    {
                        IsSucceed = false,
                        Message = "Not found",
                        Data = null


                    });


                var result = resultSet.Data.Select(wt => new WorkDetailDtoModel()
                {
                    WorkId = wt.Id.ToString(),
                    Title = wt.Title,
                    WorkTypeId = wt.WorkType.Id.ToString(),
                    WorkTypeName = wt.WorkType.Title,
                    Desc = wt.Desc,
                    Address = (wt.Address != null ? new AddressDtoModel()
                    {
                        Address = wt.Address.Address,
                        AddressId = wt.AddressId.ToString(),
                        Phone1 = wt.Address.Phone1,
                        Phone2 = wt.Address.Phone2,
                        PostalCode = wt.Address.PostalCode

                    } : null)
                    ,
                    Attachments = GetWorkAttachments(wt.Id).Result


            });
                //foreach (WorkDetailDtoModel work in result)
                //{
                //    work.Attachments = await GetWorkAttachments(Guid.Parse(work.WorkId));

                //}
                return Ok(new ResultSetDto<IEnumerable<WorkDetailDtoModel>>()
                {
                    IsSucceed = true,
                    Message = "",
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResultSetDto<IEnumerable<WorkDetailDtoModel>>()
                {
                    IsSucceed = false,
                    Message = ex.Message,
                    Data = null
                });
            }

        }


        [HttpPost]
        public async Task<ActionResult<ResultSetDto<WorkEditDtoModel>>> EditWork([FromBody] WorkEditDtoModel request)
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
                    return BadRequest(new ResultSetDto<WorkEditDtoModel>()
                    {
                        IsSucceed = false,
                        Message = "اطلاعات کسب و کار موجود نیست",
                        Data = null
                    });

                var resultTypeWork = await _WorkTypeService.GetWorkTypeByIdAsync(Guid.Parse(request.WorkTypeId));

                if (resultTypeWork == null || resultTypeWork.Data == null || !resultTypeWork.IsSucceed)
                    return BadRequest(new ResultSetDto<WorkEditDtoModel>()
                    {
                        IsSucceed = false,
                        Message = "اطلاعات نوع کسب و کار موجود نیست",
                        Data = null
                    });


                WorkInfo WorkEdit = resultWork.Data;
                WorkEdit.Title = request.Title;
                WorkEdit.Desc = request.Desc;
                WorkEdit.WorkType = resultTypeWork.Data;
                AddressInfo address = new AddressInfo();

                if (request.Address != null)
                {

                    if (request.Address.AddressId == null)
                    {

                        address.Address = request.Address.Address;
                        address.Phone1 = request.Address.Phone1;
                        address.Phone2 = request.Address.Phone2;
                        address.PostalCode = request.Address.PostalCode;

                        var addressAddResult = await _AddressService.AddAddressAsync(address);
                        if (!addressAddResult.IsSucceed)
                        {
                            return BadRequest(new ResultSetDto<WorkEditDtoModel>()
                            {
                                IsSucceed = false,
                                Message = addressAddResult.Message,
                                Data = null
                            });
                        }

                        WorkEdit.AddressId = addressAddResult.Data.Id;

                    }
                    else
                    {
                        var addressResult = await _AddressService.GetAddressByIdAsync(Guid.Parse(request.Address.AddressId));

                        if (addressResult.IsSucceed)
                        {
                            WorkEdit.AddressId = addressResult.Data.Id;
                            address = addressResult.Data;
                            address.Address = request.Address.Address;
                            address.Phone1 = request.Address.Phone1;
                            address.Phone2 = request.Address.Phone2;
                            address.PostalCode = request.Address.PostalCode;
                            var addressEditResult = await _AddressService.EditAddressAsync(address);
                            if (!addressEditResult.IsSucceed)
                            {
                                return BadRequest(new ResultSetDto<WorkEditDtoModel>()
                                {
                                    IsSucceed = false,
                                    Message = addressEditResult.Message,
                                    Data = null
                                });
                            }

                        }

                    }





                }



                var result = await _WorkService.EditWorkAsync(WorkEdit, Guid.Parse(request.UserId));
                if (!result.IsSucceed)

                {
                    return BadRequest(new ResultSetDto<WorkEditDtoModel>()
                    {
                        IsSucceed = false,
                        Message = result.Message,
                        Data = null
                    });

                }





                var resultGetAttachments = await _AttachmentService.GetAttachmentsAsync(resultWork.Data.Id, null, null);
                if (resultGetAttachments.IsSucceed && resultGetAttachments.Data != null)
                {
                    foreach (AttachmentInfo attachment in resultGetAttachments.Data)
                    {

                        await _AttachmentService.DeleteAttachmentAsync(attachment.Id);

                        //if (orderDTO.Attachments != null && orderDTO.Attachments.Any())
                        //{
                        //    if(orderDTO.Attachments.Where(a=>a.Title.ToLower() == attachment.Title.ToLower()).Count()<=0)
                        //        await _AttachmentService.GetAttachmentsAsync(orderInfo.Id, null, null);

                        //}

                    }


                }

                if (request.Attachments != null && request.Attachments.Any())
                {



                    var type = _TypeService.GetTypeByKey("AttachmentWorkLogo");
                    foreach (AttachmentNewDtoModel attachmentNew in request.Attachments)
                    {



                        AttachmentInfo attachment = new AttachmentInfo()
                        {
                            AttachmentContent = null,
                            AttachmentFileAddress = Path.Combine("WorkFiles", request.WorkId.ToString(), type.Data.Id.ToString(), request.WorkId.ToString(), attachmentNew.Title),
                            EntityId = resultWork.Data.Id,
                            AttachmentFileType = attachmentNew.AttachmentFileType,
                            Title = attachmentNew.Title
                        };
                        attachmentNew.AttachmentFileAddress = attachment.AttachmentFileAddress;
                        var resultattachmentadd = await _AttachmentService.AddAttachmentAsync(attachment);
                        if (!resultattachmentadd.IsSucceed)
                            return BadRequest(new ResultSetDto<WorkEditDtoModel>()
                            {
                                IsSucceed = false,
                                Message = resultattachmentadd.Message,
                                Data = null
                            });


                    }





                }


                return Ok(new ResultSetDto<WorkEditDtoModel>()
                {
                    IsSucceed = true,
                    Message = "",
                    Data = request
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResultSetDto<WorkEditDtoModel>()
                {
                    IsSucceed = false,
                    Message = ex.Message,
                    Data = null
                });
            }

        }

        [HttpPost]
        public async Task<ActionResult<ResultSetDto<WorkNewDtoModel>>> AddWork([FromBody] WorkNewDtoModel request)
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
                var resultTypeWork = await _WorkTypeService.GetWorkTypeByIdAsync(Guid.Parse(request.WorkTypeId));
                if (resultTypeWork == null || resultTypeWork.Data == null || !resultTypeWork.IsSucceed)
                    return BadRequest(new ResultSetDto<WorkNewDtoModel>()
                    {
                        IsSucceed = false,
                        Message = resultTypeWork.Message,
                        Data = null
                    });

                WorkInfo Work = new WorkInfo()
                {
                    Title = request.Title,
                    Desc = request.Desc,
                    WorkType = resultTypeWork.Data,



                };



                if (request.Address != null)
                {
                    AddressInfo address = new AddressInfo()
                    {
                        Address = request.Address.Address,
                        Phone1 = request.Address.Phone1,
                        Phone2 = request.Address.Phone2,
                        PostalCode = request.Address.PostalCode


                    };
                    var resultSaveAddress = await _AddressService.AddAddressAsync(address);
                    if (resultSaveAddress.IsSucceed)
                        Work.AddressId = resultSaveAddress.Data.Id;

                }



                var resultSave = await _WorkService.AddWorkAsync(Work, Guid.Parse(request.UserId));




                if (!resultSave.IsSucceed)
                {
                    return BadRequest(new ResultSetDto<WorkNewDtoModel>()
                    {
                        IsSucceed = false,
                        Message = resultSave.Message,
                        Data = null
                    });

                }




                request.WorkId = resultSave.Data.Id.ToString();
                WorkUserInfo workUser = new WorkUserInfo()
                {
                    UserId = Guid.Parse(request.UserId),
                    WorkId = Guid.Parse(request.WorkId)

                };
                var resultSaveWorkUser = await _WorkService.AddWorkUserAsync(workUser);
                if (!resultSaveWorkUser.IsSucceed)
                {
                    return BadRequest(new ResultSetDto<WorkNewDtoModel>()
                    {
                        IsSucceed = false,
                        Message = resultSaveWorkUser.Message,
                        Data = null
                    });

                }
                if (request.Attachments != null && request.Attachments.Any())
                {

                    var type = _TypeService.GetTypeByKey("AttachmentWorkLogo")

;

                    foreach (AttachmentNewDtoModel attachmentNew in request.Attachments)
                    {



                        AttachmentInfo attachment = new AttachmentInfo()
                        {
                            AttachmentContent = null,
                            AttachmentFileAddress = Path.Combine("WorkFiles", request.WorkId, type.Data.Id.ToString(), resultSave.Data.Id.ToString(), attachmentNew.Title),
                            EntityId = resultSave.Data.Id,
                            AttachmentFileType = attachmentNew.AttachmentFileType,
                            Title = attachmentNew.Title
                        };
                        attachmentNew.AttachmentFileAddress = attachment.AttachmentFileAddress;
                        var resultattachmentadd = await _AttachmentService.AddAttachmentAsync(attachment);
                        if (!resultattachmentadd.IsSucceed)
                            return BadRequest(new ResultSetDto<WorkNewDtoModel>()
                            {
                                IsSucceed = false,
                                Message = resultattachmentadd.Message,
                                Data = null
                            });


                    }





                }


                return Ok(new ResultSetDto<WorkNewDtoModel>()
                {
                    IsSucceed = true,
                    Message = "",
                    Data = request
                });

            }
            catch (Exception ex)
            {
                return BadRequest(new ResultSetDto<WorkNewDtoModel>()
                {
                    IsSucceed = false,
                    Message = ex.Message,
                    Data = null
                });
            }
        }


        [HttpPost]
        public async Task<ActionResult<ResultSetDto<WorkNewDtoModel>>> DeleteWork([FromBody] string request)
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
                var result = await _WorkService.DeleteWorkAsync(Guid.Parse(request));

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

