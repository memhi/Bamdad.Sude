using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sude.Application.Interfaces;
using Sude.Application.Result;
using Sude.Domain.Interfaces;
using Sude.Domain.Models.Account;

namespace Sude.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private ICustomerRepository _CustomerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            this._CustomerRepository = customerRepository;
        }
        public ResultSet<IEnumerable<CustomerInfo>> GetCustomers()
        {
            return new ResultSet<IEnumerable<CustomerInfo>>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = _CustomerRepository.GetCustomers()
            };
        }

        public ResultSet<CustomerInfo> GetCustomerById(Guid customerId)
        {
            CustomerInfo Customer = _CustomerRepository.GetCustomerById(customerId);

            if (Customer == null)
                return new ResultSet<CustomerInfo>()
                {
                    IsSucceed = false,
                    Message = "Customer Not Found",
                    Data = null
                };

            return new ResultSet<CustomerInfo>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = Customer
            };
        }

        public ResultSet<CustomerInfo> AddCustomer(CustomerInfo  customer)
        {


          
 
            _CustomerRepository.AddCustomer(customer);
            _CustomerRepository.Save();
           
            return new ResultSet<CustomerInfo>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = customer
            };
        }

        public ResultSet EditCustomer(CustomerInfo customer)
        {
           

            if (!_CustomerRepository.EditCustomer(customer))
                return new ResultSet() { IsSucceed = false, Message = "Customer Not Edited" };

            try
            {
                _CustomerRepository.Save();
            }
            catch
            {
                return new ResultSet() { IsSucceed = false, Message = "Customer Not Edited" };
            }
            return new ResultSet() { IsSucceed = true, Message = string.Empty };

        }

        public ResultSet DeleteCustomer(Guid customerId)
        {

            if (!_CustomerRepository.DeleteCustomer(customerId))
                return new ResultSet() { IsSucceed = false, Message = "Customer Not Deleted" };

            try
            {
                _CustomerRepository.Save();
            }
            catch
            {
                return new ResultSet() { IsSucceed = false, Message = "Customer Not Deleted" };
            }
            return new ResultSet() { IsSucceed = true, Message = string.Empty };
        }

        public async Task<ResultSet<IEnumerable<CustomerInfo>>> GetCustomersAsync()
        {
            return new ResultSet<IEnumerable<CustomerInfo>>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = await _CustomerRepository.GetCustomersAsync()
            };
        }

        public async Task<ResultSet<IEnumerable<CustomerInfo>>> GetCustomersByWorkIdAsync(Guid workId)
        {
            return new ResultSet<IEnumerable<CustomerInfo>>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = await _CustomerRepository.GetCustomersByWorkIdAsync(workId)
            };
        }

        public async Task<ResultSet<CustomerInfo>> AddCustomerAsync(CustomerInfo customer)
        {
            

            _CustomerRepository.AddCustomer(customer);

            try{await _CustomerRepository.SaveAsync();}

            catch(Exception e){return new ResultSet<CustomerInfo>() { IsSucceed = false, Message = e.Message };}

            return new ResultSet<CustomerInfo>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = customer
            };
        }

        public async Task<ResultSet> EditCustomerAsync(CustomerInfo customer)
        {
            
            if (!_CustomerRepository.EditCustomer(customer))
                return new ResultSet() { IsSucceed = false, Message = "Customer Not Edited" };

            try
            {
                await _CustomerRepository.SaveAsync();
            }
            catch(Exception e)
            {
                return new ResultSet() { IsSucceed = false, Message = e.Message };
            }
            return new ResultSet() { IsSucceed = true, Message = string.Empty };
        }

        public async Task<ResultSet> DeleteCustomerAsync(Guid customerId)
        {



























            if (!_CustomerRepository.DeleteCustomer(customerId))
                return new ResultSet() { IsSucceed = false, Message = "Customer Not Deleted" };

            try
            {
                await _CustomerRepository.SaveAsync();
            }
            catch
            {
                return new ResultSet() { IsSucceed = false, Message = "Customer Not Deleted" };
            }
            return new ResultSet() { IsSucceed = true, Message = string.Empty };
        }

        public async Task<ResultSet<CustomerInfo>> GetCustomerByIdAsync(Guid customerId)
        {
            CustomerInfo Customer = await _CustomerRepository.GetCustomerByIdAsync(customerId);

            if (Customer == null)
                return new ResultSet<CustomerInfo>()
                {
                    IsSucceed = false,
                    Message = "Customer Not Found",
                    Data = null
                };

            return new ResultSet<CustomerInfo>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = Customer
            };
        }
    }
}
