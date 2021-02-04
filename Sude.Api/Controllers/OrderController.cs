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
using Sude.Domain.Models.Order;
using Sude.Domain.Models.Account;
using Sude.Dto.DtoModels.Result;
using Sude.Dto.DtoModels.Order;
using Sude.Dto.DtoModels.Account;
using Sude.Persistence.Contexts;
using Sude.Domain.Models.Serving;
using System.Runtime.CompilerServices;
using System.Globalization;
using Sude.Domain.Models.Work;
using Sude.Dto.DtoModels.Work;
using Sude.Domain.Models.Report;
using Sude.Domain.Models.Common;
using Sude.Dto.DtoModels.Content;
using System.IO;

namespace Sude.Api.Controllers
{
    [Route("api/[controller]/[action]/")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _OrderService;
        private readonly IOrderNumberService _OrderNumberService;
        private readonly IOrderDetailService _OrderDetailService;
        private readonly ICustomerService _CustomerService;
        private readonly IWorkService _WorkService;
        private readonly IAttachmentService _AttachmentService;

        private readonly IServingService _ServingService;
        private readonly IServingInventoryService _ServingInventoryService;
        private readonly IServingInventoryTrackingService _ServingInventoryTrackingService;
        private readonly ITypeService _TypeService;

        public OrderController(IOrderService orderService, IOrderDetailService orderdetailService,
            ICustomerService customerService, IServingService servingService, IServingInventoryService servingInventoryService,
        IServingInventoryTrackingService servingInventoryTrackingService, IOrderNumberService orderNumberService,
        IWorkService workService, IAttachmentService attachmentService,ITypeService typeService)
        {

            _OrderService = orderService;
            _OrderDetailService = orderdetailService;
            _OrderNumberService = orderNumberService;
            _CustomerService = customerService;
            _ServingService = servingService;
            _ServingInventoryTrackingService = servingInventoryTrackingService;
            _ServingInventoryService = servingInventoryService;
            _WorkService = workService;
            _AttachmentService = attachmentService;
            _TypeService = typeService;

        }





        [HttpGet]

        public async Task<ActionResult> GetOrdersWithDetails(string workId, DateTime orderDateFrom, DateTime orderDateTo, bool? isBuy = null)
        {
            try
            {



                ResultSet<IEnumerable<OrderInfo>> resultSet = await _OrderService.GetOrdersWithDetailsAsync(orderDateFrom, orderDateTo, Guid.Parse(workId), isBuy);


                if (resultSet == null || resultSet.Data == null || !resultSet.Data.Any())
                    return NotFound(new ResultSetDto<IEnumerable<OrderDetailDtoModel>>()
                    {
                        IsSucceed = false,
                        Message = "Not found",
                        Data = null


                    });


                var result = resultSet.Data.Select(o => new OrderDetailDtoModel()
                {
                    OrderId = o.Id.ToString(),
                    OrderNumber = o.Number,
                    WorkId = o.WorkId.ToString(),
                    WorkName = o.Work.Title,
                    OrderDate = o.OrderDate,
                    SumPrice = o.SumPrice,
                    Description = o.Description,
                    IsBuy = o.IsBuy,
                    PaymentStatusId = o.PaymentStatusId.Value.ToString(),
                    PaymentStatusTitle = o.PaymentStatus.TypeTitle.ToString(),
                    Customer = (o.Customer != null ? new CustomerDetailDtoModel()
                    {
                        CustomerId = o.Customer.Id.ToString(),
                        Title = o.Customer.Title,
                        Phone = o.Customer.Phone,
                        WorkId = o.Customer.WorkId.ToString(),
                        NationalCode = o.Customer.NationalCode

                    } : null),
                    OrderDetails = (o.Details != null ? o.Details.Select(od => new OrderDetailDetailDtoModel()
                    {
                        Count = od.Count,
                        OrderDetailId = od.Id.ToString(),
                        ServingName = od.Serving.Title,
                        Price = od.Price,
                        ServingId = od.ServingId.ToString(),
                        OrderId = od.OrderId.ToString()

                    }).ToList() : null)


                });
                return Ok(new ResultSetDto<IEnumerable<OrderDetailDtoModel>>()
                {
                    IsSucceed = true,
                    Message = "",
                    Data = result,
                    CurrentPage = 1,
                    PageSize = int.MaxValue

                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResultSetDto<IEnumerable<OrderDetailDtoModel>>()
                {
                    IsSucceed = false,
                    Message = ex.Message,
                    Data = null
                });
            }
        }



        [HttpGet]

        public async Task<ActionResult> GetOrders(string workId, int pageIndex, int pageSize,
          DateTime? orderDateFrom = null, DateTime? orderDateTo = null, string customerId = null,
          bool? isBuy = null, string description = null, string orderNumber = null, string? paymentStatusId = null)
        {
            try
            {

                int rowCount = 0;

                ResultSet<IEnumerable<OrderInfo>> resultSet = _OrderService.GetOrders(Guid.Parse(workId), pageIndex, pageSize,
                  out rowCount, orderDateFrom, orderDateTo, (string.IsNullOrEmpty(customerId) ? null : Guid.Parse(customerId)),
 isBuy, description, orderNumber, (string.IsNullOrEmpty(paymentStatusId) ? null : Guid.Parse(paymentStatusId)));


                if (resultSet == null || resultSet.Data == null || !resultSet.Data.Any())
                    return NotFound(new ResultSetDto<IEnumerable<OrderDetailDtoModel>>()
                    {
                        IsSucceed = false,
                        Message = "Not found",
                        Data = null


                    });


                var result = resultSet.Data.Select(o => new OrderDetailDtoModel()
                {
                    OrderId = o.Id.ToString(),
                    OrderNumber = o.Number,
                    WorkId = o.WorkId.ToString(),
                    WorkName = o.Work.Title,
                    OrderDate = o.OrderDate,
                    SumPrice = o.SumPrice,
                    Description = o.Description,
                    IsBuy = o.IsBuy,
                    PaymentStatusId = o.PaymentStatusId.Value.ToString(),
                    PaymentStatusTitle = o.PaymentStatus.TypeTitle.ToString(),
                    Customer = (o.Customer != null ? new CustomerDetailDtoModel()
                    {
                        CustomerId = o.Customer.Id.ToString(),
                        Title = o.Customer.Title,
                        Phone = o.Customer.Phone,
                        WorkId = o.Customer.WorkId.ToString(),
                        NationalCode = o.Customer.NationalCode

                    } : null)



                });
                return Ok(new ResultSetDto<IEnumerable<OrderDetailDtoModel>>()
                {
                    IsSucceed = true,
                    Message = "",
                    Data = result,
                    CurrentPage = pageIndex,
                    PageSize = pageSize,
                    RowCount = rowCount
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResultSetDto<IEnumerable<OrderDetailDtoModel>>()
                {
                    IsSucceed = false,
                    Message = ex.Message,
                    Data = null
                });
            }
        }


        [HttpGet]

        //public async Task<ActionResult> GetOrders()
        //{
        //    try
        //    {



        //        ResultSet<IEnumerable<OrderInfo>> resultSet = await _OrderService.GetOrdersAsync();
        //        if (resultSet == null || resultSet.Data == null || !resultSet.Data.Any())
        //            return NotFound(new ResultSetDto<IEnumerable<OrderDetailDtoModel>>()
        //            {
        //                IsSucceed = false,
        //                Message = "Not found",
        //                Data = null


        //            });


        //        var result = resultSet.Data.Select(o => new OrderDetailDtoModel()
        //        {
        //            OrderId = o.Id.ToString(),
        //            OrderNumber = o.Number,
        //            WorkId = o.WorkId.ToString(),
        //            WorkName = o.Work.Title,
        //            OrderDate = o.OrderDate,
        //            SumPrice = o.SumPrice,
        //            Description = o.Description,
        //            IsBuy = o.IsBuy,
        //            PaymentStatusId = o.PaymentStatusId.Value.ToString(),
        //            PaymentStatusTitle = o.PaymentStatus.TypeTitle.ToString()



        //        });
        //        return Ok(new ResultSetDto<IEnumerable<OrderDetailDtoModel>>()
        //        {
        //            IsSucceed = true,
        //            Message = "",
        //            Data = result
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new ResultSetDto<IEnumerable<OrderDetailDtoModel>>()
        //        {
        //            IsSucceed = false,
        //            Message = ex.Message,
        //            Data = null
        //        });
        //    }
        //}


        [HttpPost]
        //[Consumes("application/xml")]
        //[Consumes("application/json")]
        // [Authorize]
        public async Task<ActionResult<ResultSetDto<IEnumerable<ReportOrderDtoModel>>>> GetReportOrders([FromBody] ReportOrderDtoModel searchOrder)
        {
            try
            {


                int count = 0;

                ResultSet<IEnumerable<ReportOrderGroupInfo>> resultSet = _OrderService.GetReportOrder(searchOrder.DateFrom, searchOrder.DateTo, Guid.Parse(searchOrder.WorkId),
                     searchOrder.IsBuy, searchOrder.PageSize, searchOrder.PageIndex, out count);
                if (resultSet == null || resultSet.Data == null || !resultSet.Data.Any())
                    return NotFound(new ResultSetDto<IEnumerable<ReportOrderDtoModel>>()
                    {
                        IsSucceed = false,
                        Message = "Not found",
                        Data = null


                    });



                var result = resultSet.Data.Select(o => new ReportOrderDtoModel()
                {
                    DateFrom = o.OrderDate,
                    DateTo = o.OrderDate,
                    IsBuy = o.IsBuy,
                    PageIndex = searchOrder.PageIndex,
                    PageSize = searchOrder.PageSize,
                    RowCount = count,
                    WorkId = searchOrder.WorkId,
                    SumPrice = o.SumPrice






                });
                return Ok(new ResultSetDto<IEnumerable<ReportOrderDtoModel>>()
                {
                    IsSucceed = true,
                    Message = "",
                    Data = result,
                    CurrentPage = searchOrder.PageIndex,
                    RowCount = count
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResultSetDto<IEnumerable<ReportOrderDtoModel>>()
                {
                    IsSucceed = false,
                    Message = ex.Message,
                    Data = null
                });
            }
        }



        [HttpGet("{userId}")]
        public async Task<ActionResult> GetUserOrders(string userId)
        {
            try
            {

                ResultSet<IEnumerable<WorkInfo>> resultSetWork = await _WorkService.GetWorksByUserIdAsync(Guid.Parse(userId));
                if (resultSetWork == null || resultSetWork.Data == null || !resultSetWork.Data.Any())
                    return NotFound(new ResultSetDto<IEnumerable<OrderDetailDtoModel>>()
                    {
                        IsSucceed = false,
                        Message = "Not found",
                        Data = null


                    });
                ResultSet<IEnumerable<OrderInfo>> resultSet = new ResultSet<IEnumerable<OrderInfo>>();

                foreach (WorkInfo workInfo in resultSetWork.Data)
                {
                    ResultSet<IEnumerable<OrderInfo>> resultSetOrder = await _OrderService.GetOrdersByWorkIdAsync(workInfo.Id);
                    if (resultSetOrder != null && resultSetOrder.Data != null && resultSetOrder.Data.Any())
                    {
                        if (resultSet.Data == null)
                        {
                            resultSet.Data = resultSetOrder.Data;

                        }
                        else
                            foreach (OrderInfo orderInfo in resultSetOrder.Data)
                            {
                                resultSet.Data.Append(orderInfo);

                            }

                    }
                }


                // ResultSet<IEnumerable<OrderInfo>> resultSet = await _OrderService.GetOrdersAsync();
                if (resultSet == null || resultSet.Data == null || !resultSet.Data.Any())
                    return NotFound(new ResultSetDto<IEnumerable<OrderDetailDtoModel>>()
                    {
                        IsSucceed = false,
                        Message = "Not found",
                        Data = null


                    });


                var result = resultSet.Data.Select(o => new OrderDetailDtoModel()
                {
                    OrderId = o.Id.ToString(),
                    OrderNumber = o.Number,
                    WorkId = o.WorkId.ToString(),
                    WorkName = o.Work.Title,
                    OrderDate = o.OrderDate,
                    SumPrice = o.SumPrice,
                    Description = o.Description,
                    IsBuy = o.IsBuy,
                    PaymentStatusId = o.PaymentStatusId.Value.ToString(),
                    PaymentStatusTitle = o.PaymentStatus.TypeTitle.ToString()



                });
                return Ok(new ResultSetDto<IEnumerable<OrderDetailDtoModel>>()
                {
                    IsSucceed = true,
                    Message = "",
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResultSetDto<IEnumerable<OrderDetailDtoModel>>()
                {
                    IsSucceed = false,
                    Message = ex.Message,
                    Data = null
                });
            }
        }

        //public async Task<ActionResult> SearchOrdersChartReport(string workId,string reportType)
        //{
        //    try
        //    {

        //        PersianCalendar persianCalendar = new PersianCalendar();
        //        var result = new List<object>();
        //        DateTime nowDt = DateTime.Now;
        //        int nowDay = persianCalendar.GetDayOfMonth(nowDt), nowMonth = persianCalendar.GetMonth(nowDt), nowYear = persianCalendar.GetYear(nowDt);
        //        switch (reportType)
        //        {
        //            case "year":
        //                DateTime yearAgoDt = nowDt.AddYears(-1).AddMonths(1);
        //                int agoDay = persianCalendar.GetDayOfMonth(yearAgoDt), agoMonth = persianCalendar.GetMonth(yearAgoDt),agoYear = persianCalendar.GetYear(yearAgoDt);
        //                searchYearDateUser = new DateTime(yearAgoDt.Year, yearAgoDt.Month, 1);
        //                break;
        //            case "month":
        //                DateTime monthAgoDt =  persianCalendar.ToDateTime(nowYear, nowMonth, 0, 0, 0, 0, 0); nowDt.AddDays(-30);
        //                break;
        //            case "week":
        //            default:
        //                break;
        //        }

        //                ResultSet<IEnumerable<OrderInfo>> resultSet = await _OrderService.GetOrdersAsync();
        //        if (resultSet == null || resultSet.Data == null || !resultSet.Data.Any())
        //            return NotFound(new ResultSetDto<IEnumerable<OrderDetailDtoModel>>()
        //            {
        //                IsSucceed = false,
        //                Message = "Not found",
        //                Data = null


        //            });


        //        var result = resultSet.Data.Select(o => new OrderDetailDtoModel()
        //        {
        //            OrderId = o.Id.ToString(),
        //            OrderNumber = o.Number,
        //            WorkId = o.WorkId.ToString(),
        //            WorkName = o.Work.Title,
        //            OrderDate = o.OrderDate,
        //            SumPrice = o.SumPrice,
        //            Description = o.Description,


        //        });
        //        return Ok(new ResultSetDto<IEnumerable<OrderDetailDtoModel>>()
        //        {
        //            IsSucceed = true,
        //            Message = "",
        //            Data = result
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new ResultSetDto<IEnumerable<OrderDetailDtoModel>>()
        //        {
        //            IsSucceed = false,
        //            Message = ex.Message,
        //            Data = null
        //        });
        //    }
        //}

        [HttpGet]
        public async Task<ActionResult> GetOrderDetailsAsync(string orderId)
        {
            try
            {
                ResultSet<IEnumerable<OrderDetailInfo>> resultSet = await _OrderDetailService.GetOrderDetailsAsync(Guid.Parse(orderId));
                if (resultSet == null || resultSet.Data == null || !resultSet.Data.Any())
                    return NotFound(new ResultSetDto<IEnumerable<OrderDetailDetailDtoModel>>()
                    {
                        IsSucceed = false,
                        Message = "Not found",
                        Data = null


                    });


                var result = resultSet.Data.Select(o => new OrderDetailDetailDtoModel()
                {
                    OrderId = o.OrderId.ToString(),
                    Count = o.Count,
                    OrderDetailId = o.Id.ToString(),
                    Price = o.Price,
                    ServingId = o.ServingId.ToString(),
                    ServingName = o.Serving.Title



                });
                return Ok(new ResultSetDto<IEnumerable<OrderDetailDetailDtoModel>>()
                {
                    IsSucceed = true,
                    Message = "",
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResultSetDto<IEnumerable<OrderDetailDetailDtoModel>>()
                {
                    IsSucceed = false,
                    Message = ex.Message,
                    Data = null
                });
            }
        }


        //GET : api/GetWork/0
        [HttpGet("{orderId}")]
        public async Task<ActionResult> GetOrderById(string orderId)
        {

            try
            {
                var o = (await _OrderService.GetOrderByIdAsync(Guid.Parse(orderId))).Data;
                if (o == null)
                    return NotFound(new ResultSetDto<OrderDetailDtoModel>()
                    {
                        IsSucceed = false,
                        Message = "Not found",
                        Data = null


                    });

                var result = new OrderDetailDtoModel()
                {
                    OrderId = o.Id.ToString(),
                    OrderNumber = o.Number,
                    WorkId = o.WorkId.ToString(),
                    WorkName = o.Work.Title,
                    OrderDate = o.OrderDate,
                    SumPrice = o.SumPrice,
                    Description = o.Description,
                    IsBuy = o.IsBuy,
                    PaymentStatusId = o.PaymentStatusId.Value.ToString(),
                    PaymentStatusTitle = o.PaymentStatus.TypeTitle.ToString(),
                    Customer = (o.Customer != null ? new CustomerDetailDtoModel()
                    {
                        CustomerId = o.Customer.Id.ToString(),
                        Title = o.Customer.Title,
                        Phone = o.Customer.Phone,
                        WorkId = o.Customer.WorkId.ToString(),
                        NationalCode = o.Customer.NationalCode

                    } : null)



                };

                result.Customer = new CustomerDetailDtoModel();
                if (o.Customer != null)
                {
                    result.Customer.CustomerId = o.Customer.Id.ToString();
                    result.Customer.Phone = o.Customer.Phone;
                    result.Customer.Title = o.Customer.Title;
                }
                List<OrderDetailDetailDtoModel> orderDetailDetailDtoModels = new List<OrderDetailDetailDtoModel>();

                if (o.Details != null && o.Details.Count > 0)
                {

                    foreach (OrderDetailInfo orderDetail in o.Details)
                    {
                        ResultSet<ServingInfo> servingInfo = await _ServingService.GetServingByIdAsync(orderDetail.ServingId.Value);
                        OrderDetailDetailDtoModel orderDetailDetail = new OrderDetailDetailDtoModel()
                        {
                            Count = orderDetail.Count,
                            Price = orderDetail.Price,
                            ServingId = orderDetail.ServingId.ToString(),
                            OrderId = result.OrderId.ToString(),
                            OrderDetailId = orderDetail.Id.ToString(),

                            ServingName = servingInfo.Data.Title

                        };
                        orderDetailDetailDtoModels.Add(orderDetailDetail);


                    }

                }
                else
                {
                    //return NotFound(new ResultSetDto<OrderDetailDtoModel>()
                    //{
                    //    IsSucceed = false,
                    //    Message = "اطلاعات جزئیات سفارش یافت نشد",
                    //    Data = null
                    //});

                }
                result.OrderDetails = orderDetailDetailDtoModels;


                var resultAttachments = await _AttachmentService.GetAttachmentsAsync(Guid.Parse(result.OrderId), null, null);
                if (resultAttachments.IsSucceed && resultAttachments.Data != null && resultAttachments.Data.Any())
                {
                    //                    list

                    List<AttachmentNewDtoModel> attachments = new List<AttachmentNewDtoModel>();
                    foreach (AttachmentInfo attachment in resultAttachments.Data)
                    {
                        AttachmentNewDtoModel attachmentNewDto = new AttachmentNewDtoModel()
                        {
                            AttachmentFileAddress = attachment.AttachmentFileAddress,
                            AttachmentFileType = attachment.AttachmentFileType,
                            AttachmentId = attachment.Id.ToString(),
                            EntityId = attachment.EntityId.ToString(),
                            Title = attachment.Title


                        };

                        attachments.Add(attachmentNewDto);
                    }
                    result.Attachments = attachments;
                }


                return Ok(new ResultSetDto<OrderDetailDtoModel>()
                {
                    IsSucceed = true,
                    Message = "",
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResultSetDto<OrderDetailDtoModel>()
                {
                    IsSucceed = false,
                    Message = ex.Message,
                    Data = null
                });
            }
        }

        [HttpGet("{workId}")]
        public async Task<ActionResult> GetOrdersByWorkIdAsync(string workId)
        {
            try
            {
                Guid wid;
                if (Guid.TryParse(workId, out wid))
                {
                    ResultSet<IEnumerable<OrderInfo>> resultSet = await _OrderService.GetOrdersByWorkIdAsync(wid);
                    if (resultSet == null || resultSet.Data == null || !resultSet.Data.Any())
                        return NotFound(new ResultSetDto<IEnumerable<OrderDetailDtoModel>>()
                        {
                            IsSucceed = false,
                            Message = "Not found",
                            Data = null


                        });


                    var result = resultSet.Data.Select(o => new OrderDetailDtoModel()
                    {
                        OrderId = o.Id.ToString(),
                        OrderNumber = o.Number,
                        WorkId = o.WorkId.ToString(),
                        WorkName = o.Work.Title,
                        OrderDate = o.OrderDate,
                        SumPrice = o.SumPrice,
                        Description = o.Description,
                        IsBuy = o.IsBuy,
                        PaymentStatusId = o.PaymentStatusId.Value.ToString(),
                        PaymentStatusTitle = o.PaymentStatus.TypeTitle.ToString()


                    });
                    return Ok(new ResultSetDto<IEnumerable<OrderDetailDtoModel>>()
                    {
                        IsSucceed = true,
                        Message = "",
                        Data = result
                    });
                }
                else
                {
                    return BadRequest(new ResultSetDto<IEnumerable<OrderDetailDtoModel>>()
                    {
                        IsSucceed = false,
                        Message = "Not found",
                        Data = null
                    });

                }
            }
            catch (Exception ex)
            {
                return BadRequest(new ResultSetDto<IEnumerable<OrderDetailDtoModel>>()
                {
                    IsSucceed = false,
                    Message = ex.Message + "&&&" + ex.StackTrace,
                    Data = null
                });
            }
        }


        private async Task<string> UpdateServingInventory(Guid servingId, double oldCount, double newCount, bool isOrder, bool IsDelete)
        {
            string message = "";




            if (isOrder)
            {
                newCount = newCount * -1;
            }
            else
            {
                oldCount = oldCount * -1;

            }


            if (IsDelete)
            {
                newCount = newCount * -1;


            }




            var servingresult = await _ServingService.GetServingByIdAsync(servingId);
            if (!servingresult.IsSucceed || servingresult == null || servingresult.Data == null)
            {
                message = "اطلاعات خدمت یا قلم موجود نیست";

                return message;
            }
            if (servingresult.Data.HasInventoryTracking)
            {
                var servinginventoryresult = await _ServingInventoryService.GetServingInventoryByServingIdAsync(servingresult.Data.Id);
                ServingInventoryInfo servingInventory;
                if (!servinginventoryresult.IsSucceed || servingresult.Data == null)
                {
                    servingInventory = new ServingInventoryInfo();
                    servingInventory.CurrentInventory = (newCount + oldCount);
                    servingInventory.ServingId = servingresult.Data.Id;
                    var addservinginventoryresult = await _ServingInventoryService.AddServingInventoryAsync(servingInventory);
                    if (!addservinginventoryresult.IsSucceed)
                    {
                        message = "خطا در ایجاد کنترل موجودی";

                        return message;

                    }

                }
                else
                {
                    servingInventory = servinginventoryresult.Data;
                    servingInventory.CurrentInventory += (newCount + oldCount);
                    var editservinginventoryresult = await _ServingInventoryService.EditServingInventoryAsync(servingInventory);
                    if (!editservinginventoryresult.IsSucceed)
                    {
                        message = "خطا در اصلاح کنترل موجودی";

                        return message;

                    }

                }

                return message;
            }

            return message;
        }

        [HttpPost]
        public async Task<ActionResult> GetOrdersStatistics(OrderStatisticsDtoModel orderStatistics)
        {
            try
            {
                var resultSet = await _OrderService.GetSearchOrdersCountAsync(orderStatistics.DateFrom, orderStatistics.DateTo, string.IsNullOrEmpty(orderStatistics.WorkId) ? null : Guid.Parse(orderStatistics.WorkId));
                if (resultSet.IsSucceed)
                {

                    var result = new OrderStatisticsDtoModel()
                    {
                        SearchCount = resultSet.Data
                    };

                    return Ok(new ResultSetDto<OrderStatisticsDtoModel>()
                    {
                        IsSucceed = true,
                        Message = "",
                        Data = result
                    });



                }
                else
                {
                    return NotFound(new ResultSetDto()
                    {
                        IsSucceed = false,
                        Message = "",

                    });

                }

            }
            catch (Exception ex)
            {
                return BadRequest(new ResultSetDto()
                {
                    IsSucceed = false,
                    Message = ex.Message,

                });

            }

        }



        [HttpPost]
        public async Task<ActionResult<ResultSetDto<OrderNewDtoModel>>> AddOrder([FromBody] OrderNewDtoModel orderdto)
        {
            if (!ModelState.IsValid)
            {
                string message = "";
                foreach (var er in ModelState.Values.SelectMany(modelstate => modelstate.Errors))
                    message += er.ErrorMessage + " \n";

                return BadRequest(new ResultSetDto<OrderDetailDtoModel>()
                {
                    IsSucceed = false,
                    Message = message,
                    Data = null
                });
            }

            try
            {
                if (string.IsNullOrEmpty(orderdto.OrderId))
                {
                    double sumprice = 0;
                    if (orderdto.OrderDetails != null && orderdto.OrderDetails.Count > 0)
                    {

                        foreach (OrderDetailDetailDtoModel detailNewDtoModel in orderdto.OrderDetails)
                        {
                            sumprice += detailNewDtoModel.Price * detailNewDtoModel.Count;
                        }
                    }

                    Guid? CID, OID;
                    if (!orderdto.IsBuy)
                    {
                        if (orderdto.Customer != null && !string.IsNullOrEmpty(orderdto.Customer.CustomerId))
                        {
                            CustomerInfo customer = new CustomerInfo();

                            customer.IsActive = true;
                            customer.NationalCode = orderdto.Customer.NationalCode;
                            customer.Phone = orderdto.Customer.Phone;
                            customer.Title = orderdto.Customer.Title;
                            var resultcustomer = await _CustomerService.AddCustomerAsync(customer);
                            if (!resultcustomer.IsSucceed)
                                return BadRequest(new ResultSetDto<OrderNewDtoModel>()
                                {
                                    IsSucceed = false,
                                    Message = resultcustomer.Message,
                                    Data = null
                                });
                            CID = resultcustomer.Data.Id;
                        }
                        else
                        {
                            CID = Guid.Parse(orderdto.CustomerId);

                        }
                    }
                    else
                        CID = null;

                    ResultSet<OrderNumberInfo> ordernumberresult = _OrderNumberService.GenerateOrderNumberByWorkId(Guid.Parse(orderdto.WorkId), orderdto.IsBuy);

                    if (!ordernumberresult.IsSucceed || ordernumberresult.Data == null)
                    {
                        return BadRequest(new ResultSetDto<OrderNewDtoModel>()
                        {
                            IsSucceed = false,
                            Message = ordernumberresult.Message,
                            Data = null
                        });

                    }

                    string ordenumber = (orderdto.IsBuy ? "B-" : "S-") + ordernumberresult.Data.LastNumber.ToString();




                    OrderInfo order = new OrderInfo()
                    {
                        CustomerId = CID,
                        Description = orderdto.Description,
                        Number = ordenumber,
                        OrderDate = orderdto.OrderDate,
                        WorkId = Guid.Parse(orderdto.WorkId),
                        Status = true,
                        SumPrice = sumprice,
                        IsBuy = orderdto.IsBuy,
                        PaymentStatusId = (string.IsNullOrEmpty(orderdto.PaymentStatusId) == true ? null : Guid.Parse(orderdto.PaymentStatusId))


                    };
                    var resultorder = await _OrderService.AddOrderAsync(order);
                    if (!resultorder.IsSucceed)
                        return BadRequest(new ResultSetDto<OrderNewDtoModel>()
                        {
                            IsSucceed = false,
                            Message = resultorder.Message,
                            Data = null
                        });

                    OID = resultorder.Data.Id;
                    orderdto.OrderId = OID.ToString();
                    if (orderdto.Customer != null)
                        orderdto.Customer.CustomerId = CID.ToString();
                    if (orderdto.OrderDetails != null && orderdto.OrderDetails.Count > 0)
                    {

                        foreach (OrderDetailDetailDtoModel detailNewDtoModel in orderdto.OrderDetails)
                        {
                            OrderDetailInfo orderDetail = new OrderDetailInfo()
                            {
                                Count = detailNewDtoModel.Count,
                                OrderId = OID.Value,
                                Price = detailNewDtoModel.Price,
                                ServingId = Guid.Parse(detailNewDtoModel.ServingId)

                            };

                            var resultorderdetail = await _OrderDetailService.AddOrderDetailAsync(orderDetail);
                            if (!resultorderdetail.IsSucceed)
                                return BadRequest(new ResultSetDto<OrderNewDtoModel>()
                                {
                                    IsSucceed = false,
                                    Message = resultorderdetail.Message,
                                    Data = null
                                });



                            var Message = await UpdateServingInventory(orderDetail.ServingId.Value, 0, orderDetail.Count, !orderdto.IsBuy, false);
                            if (!string.IsNullOrEmpty(Message))
                                return BadRequest(new ResultSetDto<OrderNewDtoModel>()
                                {
                                    IsSucceed = false,
                                    Message = Message,
                                    Data = null
                                });

                            detailNewDtoModel.OrderId = OID.ToString();
                            detailNewDtoModel.OrderDetailId = resultorderdetail.Data.Id.ToString();

                        }



                    }
                    if (orderdto.Attachments != null && orderdto.Attachments.Any())
                    {

                        var type = _TypeService.GetTypeByKey("AttachmentOrderBuyPicture")
                            
;
                        
                        foreach (AttachmentNewDtoModel attachmentNew in orderdto.Attachments)
                        {



                            AttachmentInfo attachment = new AttachmentInfo()
                            {
                                AttachmentContent = null,
                                AttachmentFileAddress = Path.Combine("WorkFiles",orderdto.WorkId, type.Data.Id.ToString(), order.Id.ToString(), attachmentNew.Title),
                                EntityId = order.Id,
                                AttachmentFileType = attachmentNew.AttachmentFileType,
                                Title = attachmentNew.Title
                            };
                            attachmentNew.AttachmentFileAddress = attachment.AttachmentFileAddress;
                            var resultattachmentadd = await _AttachmentService.AddAttachmentAsync(attachment);
                            if (!resultattachmentadd.IsSucceed)
                                return BadRequest(new ResultSetDto<OrderNewDtoModel>()
                                {
                                    IsSucceed = false,
                                    Message = resultattachmentadd.Message,
                                    Data = null
                                });


                        }





                    }
                }
                else
                {
                    return BadRequest(new ResultSetDto<OrderNewDtoModel>()
                    {
                        IsSucceed = false,
                        Message = "Id is incorrect",
                        Data = null
                    });

                }


                    return Ok(new ResultSetDto<OrderNewDtoModel>()
                    {
                        IsSucceed = true,
                        Message = "",
                        Data = orderdto
                    });

                }
            catch (Exception ex)
            {
                return BadRequest(new ResultSetDto<OrderNewDtoModel>()
                {
                    IsSucceed = false,
                    Message = ex.Message,
                    Data = null
                });
            }


        }



        [HttpPost]
        public async Task<ActionResult<ResultSetDto<OrderEditDtoModel>>> SetPayment([FromBody] OrderEditDtoModel orderDTO)
        {

            if (!ModelState.IsValid)
            {
                string message = "";
                foreach (var er in ModelState.Values.SelectMany(modelstate => modelstate.Errors))
                    message += er.ErrorMessage + " \n";

                return BadRequest(new ResultSetDto<OrderEditDtoModel>()
                {
                    IsSucceed = false,
                    Message = message,
                    Data = null
                });
            }

            try
            {




                var resultOrder = await _OrderService.GetOrderByIdAsync(Guid.Parse(orderDTO.OrderId));

                if (!resultOrder.IsSucceed)
                    return BadRequest(new ResultSetDto<OrderEditDtoModel>()
                    {
                        IsSucceed = false,
                        Message = resultOrder.Message,
                        Data = null
                    });


                OrderInfo orderInfo = resultOrder.Data;
                orderInfo.PaymentStatusId = (string.IsNullOrEmpty(orderDTO.PaymentStatusId) == true ? null : Guid.Parse(orderDTO.PaymentStatusId));



                var resultSaveOrder = await _OrderService.EditOrderAsync(orderInfo);
                if (!resultSaveOrder.IsSucceed)
                    return BadRequest(new ResultSetDto<OrderEditDtoModel>()
                    {
                        IsSucceed = false,
                        Message = resultSaveOrder.Message,
                        Data = null
                    });


            

                return Ok(new ResultSetDto<OrderEditDtoModel>()
                {
                    IsSucceed = true,
                    Message = "",
                    Data = orderDTO
                });

            }
            catch (Exception ex)
            {
                return BadRequest(new ResultSetDto<OrderEditDtoModel>()
                {
                    IsSucceed = false,
                    Message = ex.Message,
                    Data = null
                });
            }




        }



        [HttpPost]
        public async Task<ActionResult<ResultSetDto<OrderDetailDtoModel>>> EditOrder([FromBody] OrderEditDtoModel orderDTO)
        {

            if (!ModelState.IsValid)
            {
                string message = "";
                foreach (var er in ModelState.Values.SelectMany(modelstate => modelstate.Errors))
                    message += er.ErrorMessage + " \n";

                return BadRequest(new ResultSetDto<OrderEditDtoModel>()
                {
                    IsSucceed = false,
                    Message = message,
                    Data = null
                });
            }

            try
            {

                ICollection<OrderDetailDetailDtoModel> orderDetailsRequest = orderDTO.OrderDetails;


                var resultOrder = await _OrderService.GetOrderByIdAsync(Guid.Parse(orderDTO.OrderId));

                if (!resultOrder.IsSucceed)
                    return BadRequest(new ResultSetDto<OrderEditDtoModel>()
                    {
                        IsSucceed = false,
                        Message = resultOrder.Message,
                        Data = null
                    });


                OrderInfo orderInfo = resultOrder.Data;
                orderInfo.CustomerId = (orderDTO.CustomerId == null ? null : Guid.Parse(orderDTO.CustomerId));
                orderInfo.Description = orderDTO.Description;
                orderInfo.OrderDate = orderDTO.OrderDate;
                orderInfo.IsBuy = orderDTO.IsBuy;
                orderInfo.PaymentStatusId = (string.IsNullOrEmpty(orderDTO.PaymentStatusId) == true ? null : Guid.Parse(orderDTO.PaymentStatusId));


                var orderDetailsOriginal = await _OrderDetailService.GetOrderDetailsAsync(resultOrder.Data.Id);
                foreach (OrderDetailInfo orderDetailDetail in orderDetailsOriginal.Data)
                {
                    if (orderDetailsRequest.Where(od => od.ServingId.ToString() == orderDetailDetail.ServingId.ToString()).Count() <= 0)
                    {


                        var Message = await UpdateServingInventory(orderDetailDetail.ServingId.Value, 0, orderDetailDetail.Count, !orderDTO.IsBuy, true);
                        if (!string.IsNullOrEmpty(Message))
                            return BadRequest(new ResultSetDto<OrderEditDtoModel>()
                            {
                                IsSucceed = false,
                                Message = Message,
                                Data = null
                            });


                        var resultdetaildelete = await _OrderDetailService.DeleteOrderDetailAsync(orderDetailDetail.Id);
                        if (!resultdetaildelete.IsSucceed)
                        {
                            return BadRequest(new ResultSetDto<OrderEditDtoModel>()
                            {
                                IsSucceed = false,
                                Message = resultdetaildelete.Message,
                                Data = null
                            });

                        }
                    }

                }

                double sumpricerequestorder = 0;
                foreach (OrderDetailDetailDtoModel detailDetailDtoModel in orderDTO.OrderDetails)
                {

                    sumpricerequestorder += (detailDetailDtoModel.Price * detailDetailDtoModel.Count);
                    ServingInventoryInfo servingInventory = new ServingInventoryInfo();
                    OrderDetailInfo orderDetail = await _OrderDetailService.GetOrderDetailByServingAndOrderIdAsync(Guid.Parse(orderDTO.OrderId), Guid.Parse(detailDetailDtoModel.ServingId));
                    if (orderDetail != null)
                    {







                        if (orderDetail.Serving.HasInventoryTracking)
                        {


                            var Message = await UpdateServingInventory(orderDetail.ServingId.Value, orderDetail.Count, detailDetailDtoModel.Count, !orderDTO.IsBuy, false);
                            if (!string.IsNullOrEmpty(Message))
                                return BadRequest(new ResultSetDto<OrderEditDtoModel>()
                                {
                                    IsSucceed = false,
                                    Message = Message,
                                    Data = null
                                });



                        }

                        orderDetail.Count = detailDetailDtoModel.Count;
                        orderDetail.Price = detailDetailDtoModel.Price;
                        orderDetail.ServingId = Guid.Parse(detailDetailDtoModel.ServingId);

                        var orderdetailoriginalsave = await _OrderDetailService.EditOrderDetailAsync(orderDetail);
                        if (!orderdetailoriginalsave.IsSucceed)
                        {

                            return BadRequest(new ResultSetDto<OrderEditDtoModel>()
                            {
                                IsSucceed = false,
                                Message = orderdetailoriginalsave.Message,
                                Data = null
                            });

                        }

                    }

                    else
                    {

                        OrderDetailInfo orderDetailInfo = new OrderDetailInfo()
                        {
                            Count = detailDetailDtoModel.Count,
                            OrderId = Guid.Parse(orderDTO.OrderId),
                            Price = detailDetailDtoModel.Price,
                            ServingId = Guid.Parse(detailDetailDtoModel.ServingId)


                        };
                        var orderdetailoriginalsave = await _OrderDetailService.AddOrderDetailAsync(orderDetailInfo);
                        if (!orderdetailoriginalsave.IsSucceed)
                        {

                            return BadRequest(new ResultSetDto<OrderEditDtoModel>()
                            {
                                IsSucceed = false,
                                Message = orderdetailoriginalsave.Message,
                                Data = null
                            });

                        }



                        var Message = await UpdateServingInventory(orderDetailInfo.ServingId.Value, 0, detailDetailDtoModel.Count, !orderDTO.IsBuy, false);
                        if (!string.IsNullOrEmpty(Message))
                            return BadRequest(new ResultSetDto<OrderEditDtoModel>()
                            {
                                IsSucceed = false,
                                Message = Message,
                                Data = null
                            });





                    }


                }

                orderInfo.SumPrice = sumpricerequestorder;



                var resultSaveOrder = await _OrderService.EditOrderAsync(orderInfo);
                if (!resultSaveOrder.IsSucceed)
                    return BadRequest(new ResultSetDto<OrderEditDtoModel>()
                    {
                        IsSucceed = false,
                        Message = resultSaveOrder.Message,
                        Data = null
                    });

                var resultGetAttachments = await _AttachmentService.GetAttachmentsAsync(orderInfo.Id, null, null);
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

                if (orderDTO.Attachments != null && orderDTO.Attachments.Any())
                {



                    var type = _TypeService.GetTypeByKey("AttachmentOrderBuyPicture");
                    foreach (AttachmentNewDtoModel attachmentNew in orderDTO.Attachments)
                    {



                        AttachmentInfo attachment = new AttachmentInfo()
                        {
                            AttachmentContent = null,
                            AttachmentFileAddress = Path.Combine("WorkFiles", orderInfo.WorkId.ToString(), type.Data.Id.ToString(), orderInfo.Id.ToString(), attachmentNew.Title),
                            EntityId = orderInfo.Id,
                            AttachmentFileType = attachmentNew.AttachmentFileType,
                            Title = attachmentNew.Title
                        };
                        attachmentNew.AttachmentFileAddress = attachment.AttachmentFileAddress;
                        var resultattachmentadd = await _AttachmentService.AddAttachmentAsync(attachment);
                        if (!resultattachmentadd.IsSucceed)
                            return BadRequest(new ResultSetDto<OrderNewDtoModel>()
                            {
                                IsSucceed = false,
                                Message = resultattachmentadd.Message,
                                Data = null
                            });


                    }





                }

                return Ok(new ResultSetDto<OrderEditDtoModel>()
                {
                    IsSucceed = true,
                    Message = "",
                    Data = orderDTO
                });

            }
            catch (Exception ex)
            {
                return BadRequest(new ResultSetDto<OrderEditDtoModel>()
                {
                    IsSucceed = false,
                    Message = ex.Message,
                    Data = null
                });
            }




        }





        [HttpPost]
        public async Task<ActionResult<ResultSetDto<OrderDetailDtoModel>>> DeleteOrder([FromBody] string orderId)
        {



            try
            {
                var o = (await _OrderService.GetOrderByIdAsync(Guid.Parse(orderId))).Data;
                if (o == null)
                    return NotFound(new ResultSetDto<OrderDetailDtoModel>()
                    {
                        IsSucceed = false,
                        Message = "Not found",
                        Data = null


                    });

                //  double sumpriceoriginal = resultOrder.Data.SumPrice;

                var orderDetailsOriginal = await _OrderDetailService.GetOrderDetailsAsync(o.Id);
                foreach (OrderDetailInfo orderDetailDetail in orderDetailsOriginal.Data)
                {

                    var Message = await UpdateServingInventory(orderDetailDetail.ServingId.Value, 0, orderDetailDetail.Count, !o.IsBuy, true);
                    if (!string.IsNullOrEmpty(Message))
                        return BadRequest(new ResultSetDto<OrderDetailDtoModel>()
                        {
                            IsSucceed = false,
                            Message = Message,
                            Data = null
                        });


                }





                var resultDeleteOrder = await _OrderService.DeleteOrderAsync(o.Id);
                if (!resultDeleteOrder.IsSucceed)
                    return BadRequest(new ResultSetDto<OrderDetailDtoModel>()
                    {
                        IsSucceed = false,
                        Message = resultDeleteOrder.Message,
                        Data = null
                    });

                return Ok(new ResultSetDto<OrderDetailDtoModel>()
                {
                    IsSucceed = true,
                    Message = "",
                    Data = null
                });

            }
            catch (Exception ex)
            {
                return BadRequest(new ResultSetDto<OrderDetailDtoModel>()
                {
                    IsSucceed = false,
                    Message = ex.Message,
                    Data = null
                });
            }




        }




        //[HttpPost]
        //public async Task<ActionResult<ResultSetDto<WorkNewDtoModel>>> DeleteWork([FromBody] string request)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);



        //    var result = await _WorkService.DeleteWorkAsync(Guid.Parse(request));

        //    if (!result.IsSucceed)
        //        return Ok(new ResultSetDto()
        //        {
        //            IsSucceed = false,
        //            Message = result.Message
        //        });


        //    return Ok(new ResultSetDto()
        //    {
        //        IsSucceed = true,
        //        Message = ""
        //    });
        //}


    }
}

