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
    public class AddressRepository : IAddressRepository
    {

        private GenericRepository<AddressInfo> _AddressRepository;

        public AddressRepository(SudeDBContext ctx)
        {
            this._AddressRepository = new GenericRepository<AddressInfo>(ctx);
        }

        public async Task<IEnumerable<AddressInfo>> GetAddresssAsync()
        {
           
            return await _AddressRepository.GetAsync();
        }
        public bool AddAddress(AddressInfo address)
        {
            try
            {
                _AddressRepository.Insert(address);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool EditAddress(AddressInfo address)
        {
            try
            {
                _AddressRepository.Update(address);
            }
            catch
            { 
                return false; 
            }
            return true;
        }

        public async Task<AddressInfo> GetAddressByIdAsync(Guid addressId)
        {
            return await _AddressRepository.GetByIdAsync(addressId);
        }

        public void Save()
        {
            _AddressRepository.Save();
        }

        public async Task SaveAsync() =>
            await _AddressRepository.SaveAsync();

        public IEnumerable<AddressInfo> GetAddresss()
        {
            return _AddressRepository.Get();
        }

      

        

        public bool DeleteAddress(Guid addressId)
        {
            var address = GetAddressById(addressId);
            if (address == null)
                return false;
            try
            {

                address.IsRemoved = true;
                address.RemoveDate = DateTime.Now;
                _AddressRepository.Update(address);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public AddressInfo GetAddressById(Guid addressId)
        {
            return _AddressRepository.GetById(addressId);
        }
    }
}
