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

namespace Sude.Api.Controllers
{
    [Route("api/[controller]/[action]/")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _OrderService;
        private readonly IOrderDetailService _OrderDetailService;
        private readonly ICustomerService _CustomerService;

        public OrderController(IOrderService orderService, IOrderDetailService orderdetailService, ICustomerService customerService)
        {

            _OrderService = orderService;
            _OrderDetailService = orderdetailService;
            _CustomerService = customerService;

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
                if (resultSet == null)
                    NotFound();

                var result = resultSet.Data.Select(o => new OrderDetailDtoModel()
                {
                    OrderId = o.Id.ToString(),
                    OrderNumber = o.Number,
                    WorkId = o.WorkId.ToString(),
                    WorkName = o.Work.Title,
                    OrderDate = o.OrderDate,
                    SumPrice = o.SumPrice,
                    Description = o.Description,


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
                return Ok(new ResultSetDto<IEnumerable<OrderDetailDtoModel>>()
                {
                    IsSucceed = false,
                    Message = ex.Message + "&&&" + ex.StackTrace,
                    Data = null
                });
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetOrderDetailsAsync(string orderId)
        {
            try
            {
                ResultSet<IEnumerable<OrderDetailInfo>> resultSet = await _OrderDetailService.GetOrderDetailsAsync(Guid.Parse(orderId));
                if (resultSet == null)
                    NotFound();

                var result = resultSet.Data.Select(o => new OrderDetailDetailDtoModel()
                {
                    OrderId = o.OrderId.ToString(),
                    Count = o.Count,
                    OrderDetailId = o.Id.ToString(),
                    Price = o.Price,
                    ServingId = o.ServingId.ToString(),
                    ServingTitle = o.Serving.Title,
                    Description = o.Description,


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
                return Ok(new ResultSetDto<IEnumerable<OrderDetailDtoModel>>()
                {
                    IsSucceed = false,
                    Message = ex.Message + "&&&" + ex.StackTrace,
                    Data = null
                });
            }
        }


        //GET : api/GetWork/0
        [HttpGet("{orderId}")]
        public async Task<ActionResult> GetOrderById(string orderId)
        {
            var o = (await _OrderService.GetOrderByIdAsync(Guid.Parse(orderId))).Data;
            if (o == null)
                NotFound();

            var result = new OrderDetailDtoModel()
            {
                OrderId = o.Id.ToString(),
                OrderNumber = o.Number,
                WorkId = o.WorkId.ToString(),
                WorkName = o.Work.Title,
                OrderDate = o.OrderDate,
                SumPrice = o.SumPrice,
                Description = o.Description
            };

            result.Customer = new CustomerDetailDtoModel();
            result.Customer.CustomerId = o.Customer.Id.ToString();
            result.Customer.Phone = o.Customer.Phone;
            result.Customer.Title = o.Customer.Title;

            if (o.Details != null && o.Details.Count > 0)
                foreach (OrderDetailInfo orderDetail in o.Details)
                {
                    OrderDetailDetailDtoModel orderDetailDetail = new OrderDetailDetailDtoModel()
                    {
                        Count = orderDetail.Count,
                        Price = orderDetail.Price,
                        ServingId = orderDetail.ServingId.ToString(),
                        OrderId = result.OrderId.ToString(),
                        OrderDetailId = orderDetail.Id.ToString(),
                        Description = orderDetail.Description
                    };

                    result.OrderDetails.Add(orderDetailDetail);

                }


            return Ok(new ResultSetDto<OrderDetailDtoModel>()
            {
                IsSucceed = true,
                Message = "",
                Data = result
            });
        }



        [HttpPost]
        public async Task<ActionResult<ResultSetDto<OrderNewDtoModel>>> AddOrder([FromBody] OrderNewDtoModel orderdto)
        {
            if (!ModelState.IsValid)
            {
                string message = "";
                foreach (var er in ModelState.Values.SelectMany(modelstate => modelstate.Errors))
                    message += er.ErrorMessage + " \n";

                return Ok(new ResultSetDto<OrderDetailDtoModel>()
                {
                    IsSucceed = false,
                    Message = message,
                    Data = null
                });
            }

            if (string.IsNullOrEmpty(orderdto.OrderId))
            {
                double sumprice = 0;
                if (orderdto.OrderDetails != null && orderdto.OrderDetails.Count > 0)
                {

                    foreach (OrderDetailNewDtoModel detailNewDtoModel in orderdto.OrderDetails)
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
                        return Ok(new ResultSetDto<OrderNewDtoModel>()
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
                    SumPrice = sumprice

                };
                var resultorder = await _OrderService.AddOrderAsync(order);
                if (!resultorder.IsSucceed)
                    return Ok(new ResultSetDto<OrderNewDtoModel>()
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

                    foreach (OrderDetailNewDtoModel detailNewDtoModel in orderdto.OrderDetails)
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
                            return Ok(new ResultSetDto<OrderNewDtoModel>()
                            {
                                IsSucceed = false,
                                Message = resultorderdetail.Message,
                                Data = null
                            });

                        detailNewDtoModel.OrderId = OID.ToString();
                        detailNewDtoModel.OrderDetailId = resultorderdetail.Data.Id.ToString();

                    }



                }


            }

            else
            {
                return Ok(new ResultSetDto<OrderNewDtoModel>()
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



        [HttpPost]
        public async Task<ActionResult<ResultSetDto<OrderDetailDtoModel>>> EditOrder([FromBody] OrderEditDtoModel orderDTO)
        {

            if (!ModelState.IsValid)
            {
                string message = "";
                foreach (var er in ModelState.Values.SelectMany(modelstate => modelstate.Errors))
                    message += er.ErrorMessage + " \n";

                return Ok(new ResultSetDto<OrderEditDtoModel>()
                {
                    IsSucceed = false,
                    Message = message,
                    Data = null
                });
            }





            var resultOrder = await _OrderService.GetOrderByIdAsync(Guid.Parse(orderDTO.OrderId));

            if (!resultOrder.IsSucceed)
                return Ok(new ResultSetDto<OrderEditDtoModel>()
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

            var resultSaveOrder = await _OrderService.EditOrderAsync(orderInfo);
            if (!resultSaveOrder.IsSucceed)
                return Ok(new ResultSetDto<OrderEditDtoModel>()
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

