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
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _CustomerService;
 

        public CustomerController(ICustomerService customerService)
        {


            _CustomerService = customerService;

        }

        //GET : api/GetWorks
        [HttpGet("{workId}")]
        //[Consumes("application/xml")]
        //[Consumes("application/json")]
        // [Authorize]
        public async Task<ActionResult> GetCustomersByWorkId(string workId)
        {
            try
            {
                ResultSet<IEnumerable<CustomerInfo>> resultSet = await _CustomerService.GetCustomersByWorkIdAsync(Guid.Parse(workId));
                if (resultSet == null)
                    NotFound();

                var result = resultSet.Data.Select(c => new CustomerDetailDtoModel()
                {
                    CustomerId = c.Id.ToString(),
                    NationalCode = c.NationalCode,
                    Phone = c.Phone,
                     Title=c.Title,
                    WorkId = c.WorkId.ToString() 


                }) ;
                return Ok(new ResultSetDto<IEnumerable<CustomerDetailDtoModel>>()
                {
                    IsSucceed = true,
                    Message = "",
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return Ok(new ResultSetDto<IEnumerable<CustomerDetailDtoModel>>()
                {
                    IsSucceed = false,
                    Message = ex.Message + "&&&" + ex.StackTrace,
                    Data = null
                });
            }
        }

        [HttpGet]
        //[Consumes("application/xml")]
        //[Consumes("application/json")]
        // [Authorize]
        public async Task<ActionResult> GetCustomers()
        {
            try
            {
                ResultSet<IEnumerable<CustomerInfo>> resultSet = await _CustomerService.GetCustomersAsync();
                if (resultSet == null)
                    NotFound();

                var result = resultSet.Data.Select(c => new CustomerDetailDtoModel()
                {
                    CustomerId = c.Id.ToString(),
                    NationalCode = c.NationalCode,
                    Phone = c.Phone,
                    Title = c.Title,
                    WorkId = c.WorkId.ToString()


                });
                return Ok(new ResultSetDto<IEnumerable<CustomerDetailDtoModel>>()
                {
                    IsSucceed = true,
                    Message = "",
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return Ok(new ResultSetDto<IEnumerable<CustomerDetailDtoModel>>()
                {
                    IsSucceed = false,
                    Message = ex.Message + "&&&" + ex.StackTrace,
                    Data = null
                });
            }
        }


    }
}

