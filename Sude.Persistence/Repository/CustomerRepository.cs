using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sude.Domain.Interfaces;
using Sude.Domain.Models.Account;
 
using Sude.Persistence.Contexts;

namespace Sude.Persistence.Repository
{
    public class CustomerRepository : ICustomerRepository
    {

        private GenericRepository<CustomerInfo> _CustomerRepository;

        public CustomerRepository(SudeDBContext ctx)
        {
            this._CustomerRepository = new GenericRepository<CustomerInfo>(ctx);
        }
        public async Task<IEnumerable<CustomerInfo>> GetCustomersByWorkIdAsync(Guid workId)
        {
            return await _CustomerRepository.GetAsync(c=>c.WorkId==workId);
        }
        public async Task<IEnumerable<CustomerInfo>> GetCustomersAsync()
        {
           
            return await _CustomerRepository.GetAsync();
        }
        public bool AddCustomer(CustomerInfo customer)
        {
            try
            {
                _CustomerRepository.Insert(customer);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool EditCustomer(CustomerInfo customer)
        {
            try
            {
                _CustomerRepository.Update(customer);
            }
            catch
            { 
                return false; 
            }
            return true;
        }

        public async Task<CustomerInfo> GetCustomerByIdAsync(Guid customerId)
        {
            return await _CustomerRepository.GetByIdAsync(customerId);
        }

        public void Save()
        {
            _CustomerRepository.Save();
        }

        public async Task SaveAsync() =>
            await _CustomerRepository.SaveAsync();

        public IEnumerable<CustomerInfo> GetCustomers()
        {
            return _CustomerRepository.Get();
        }

      

        

        public bool DeleteCustomer(Guid customerId)
        {
            var customer = GetCustomerById(customerId);
            if (customer == null)
                return false;
            try
            {

                customer.IsRemoved = true;
                customer.RemoveDate = DateTime.Now;
                _CustomerRepository.Update(customer);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public CustomerInfo GetCustomerById(Guid customerId)
        {
            return _CustomerRepository.GetById(customerId);
        }
    }
}
