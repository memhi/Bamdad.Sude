using Sude.Domain.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sude.Domain.Interfaces
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<CustomerInfo>> GetCustomersAsync();
        IEnumerable<CustomerInfo> GetCustomers();
        Task<IEnumerable<CustomerInfo>> GetCustomersByWorkIdAsync(Guid workId);


        bool AddCustomer(CustomerInfo customer);
        bool EditCustomer(CustomerInfo customer);
        bool DeleteCustomer(Guid customerId);

        Task<CustomerInfo> GetCustomerByIdAsync(Guid customerId);
        CustomerInfo GetCustomerById(Guid customerId);
       

        void Save();
        Task SaveAsync();
    }
}
