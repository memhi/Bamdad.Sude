using Sude.Application.Result;
using Sude.Domain.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sude.Application.Interfaces
{
    public interface ICustomerService
    {
        ResultSet<IEnumerable<CustomerInfo>> GetCustomers();
        ResultSet<CustomerInfo> AddCustomer(CustomerInfo customer);
        Task<ResultSet<IEnumerable<CustomerInfo>>> GetCustomersByWorkIdAsync(Guid workId);
        ResultSet EditCustomer(CustomerInfo customer);
        ResultSet DeleteCustomer(Guid customerId);
        ResultSet<CustomerInfo> GetCustomerById(Guid customerId);
        Task<ResultSet<IEnumerable<CustomerInfo>>> GetCustomersAsync();
        Task<ResultSet<CustomerInfo>> AddCustomerAsync(CustomerInfo customer);
        Task<ResultSet> EditCustomerAsync(CustomerInfo customer);
        Task<ResultSet> DeleteCustomerAsync(Guid customerId);
        Task<ResultSet<CustomerInfo>> GetCustomerByIdAsync(Guid customerId);
        
    }
}
