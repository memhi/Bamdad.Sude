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

namespace Sude.Api.Controllers
{
    [Route("api/[controller]/[action]/")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _OrderService;
        private readonly IOrderDetailService _OrderDetailService;
        private readonly ICustomerService _CustomerService;

        private readonly IServingService _ServingService;
        private readonly IServingInventoryService _ServingInventoryService;
        private readonly IServingInventoryTrackingService _ServingInventoryTrackingService;

        public OrderController(IOrderService orderService, IOrderDetailService orderdetailService,
            ICustomerService customerService, IServingService servingService, IServingInventoryService servingInventoryService,
        IServingInventoryTrackingService servingInventoryTrackingService)
        {

            _OrderService = orderService;
            _OrderDetailService = orderdetailService;
            _CustomerService = customerService;
            _ServingService = servingService;
            _ServingInventoryTrackingService = servingInventoryTrackingService;
            _ServingInventoryService = servingInventoryService;

        }

        //GET : api/GetWorks
        [HttpGet]
        //[Consumes("application/xml")]
        //[Consumes("application/json")]
        // [Authorize]
        public async Task<ActionResult> GetOrders()
        {
            try
            {
                ResultSet<IEnumerable<OrderInfo>> resultSet = await _OrderService.GetOrdersAsync();
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
                    IsBuy = o.IsBuy



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
                    return NotFound(new ResultSetDto<OrderInfo>()
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
                     IsBuy=o.IsBuy

                };

                result.Customer = new CustomerDetailDtoModel();
                result.Customer.CustomerId = o.Customer.Id.ToString();
                result.Customer.Phone = o.Customer.Phone;
                result.Customer.Title = o.Customer.Title;
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
                    return NotFound(new ResultSetDto<OrderDetailDtoModel>()
                    {
                        IsSucceed = false,
                        Message = "اطلاعات جزئیات سفارش یافت نشد",
                        Data = null
                    });

                }
                result.OrderDetails = orderDetailDetailDtoModels;

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
                        IsBuy = o.IsBuy


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


        private async Task<string> UpdateServingInventory (Guid servingId,double oldCount, double newCount , bool isOrder,bool IsDelete)
        {
          string  message = "";

       


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
            if(servingresult.Data.HasInventoryTracking)
            {
                var servinginventoryresult = await _ServingInventoryService.GetServingInventoryByServingIdAsync(servingresult.Data.Id);
                ServingInventoryInfo servingInventory;
                if(!servinginventoryresult.IsSucceed || servingresult.Data==null )
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
                    servingInventory.CurrentInventory += (newCount+oldCount);
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
        public async Task<ActionResult> GetOrdersStatistics (OrderStatisticsDtoModel orderStatistics)
        {
            try
            {
                var resultSet =await  _OrderService.GetSearchOrdersCountAsync(orderStatistics.DateFrom, orderStatistics.DateTo,string.IsNullOrEmpty(orderStatistics.WorkId)?null : Guid.Parse(orderStatistics.WorkId));
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
            catch(Exception ex)
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

                    Guid CID, OID;
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

                    OrderInfo order = new OrderInfo()
                    {
                        CustomerId = CID,
                        Description = orderdto.Description,
                        Number = orderdto.OrderNumber,
                        OrderDate = orderdto.OrderDate,
                        WorkId = Guid.Parse(orderdto.WorkId),
                        Status = true,
                        SumPrice = sumprice,
                        IsBuy=orderdto.IsBuy

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
                                OrderId = OID,
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


                           
                           var Message = await UpdateServingInventory(orderDetail.ServingId.Value,0, orderDetail.Count, !orderdto.IsBuy, false); 
                           if(!string.IsNullOrEmpty(Message))
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
                orderInfo.CustomerId = Guid.Parse(orderDTO.CustomerId);
                orderInfo.Description = orderDTO.Description;
                orderInfo.Number = orderDTO.OrderNumber;
                orderInfo.OrderDate = orderDTO.OrderDate;
                orderInfo.IsBuy = orderDTO.IsBuy;
                //  double sumpriceoriginal = resultOrder.Data.SumPrice;

                var orderDetailsOriginal = await _OrderDetailService.GetOrderDetailsAsync(resultOrder.Data.Id);
                foreach (OrderDetailInfo orderDetailDetail in orderDetailsOriginal.Data)
                {
                    if (orderDetailsRequest.Where(od => od.ServingId.ToString() == orderDetailDetail.ServingId.ToString()).Count() <= 0)
                    {

                 
                        var Message = await UpdateServingInventory(orderDetailDetail.ServingId.Value, 0,orderDetailDetail.Count, !orderDTO.IsBuy, true);
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
                   OrderDetailInfo orderDetail  = await _OrderDetailService.GetOrderDetailByServingAndOrderIdAsync(Guid.Parse(orderDTO.OrderId), Guid.Parse(detailDetailDtoModel.ServingId));
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

