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
    public class AddressService : IAddressService
    {
        private IAddressRepository _AddressRepository;

        public AddressService(IAddressRepository addressRepository)
        {
            this._AddressRepository = addressRepository;
        }
        public ResultSet<IEnumerable<AddressInfo>> GetAddresss()
        {
            return new ResultSet<IEnumerable<AddressInfo>>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = _AddressRepository.GetAddresss()
            };
        }

        public ResultSet<AddressInfo> GetAddressById(Guid addressId)
        {
            AddressInfo Address = _AddressRepository.GetAddressById(addressId);

            if (Address == null)
                return new ResultSet<AddressInfo>()
                {
                    IsSucceed = false,
                    Message = "Address Not Found",
                    Data = null
                };

            return new ResultSet<AddressInfo>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = Address
            };
        }

        public ResultSet<AddressInfo> AddAddress(AddressInfo  address)
        {


          
 
            _AddressRepository.AddAddress(address);
            _AddressRepository.Save();
           
            return new ResultSet<AddressInfo>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = address
            };
        }

        public ResultSet EditAddress(AddressInfo address)
        {
           

            if (!_AddressRepository.EditAddress(address))
                return new ResultSet() { IsSucceed = false, Message = "Address Not Edited" };

            try
            {
                _AddressRepository.Save();
            }
            catch
            {
                return new ResultSet() { IsSucceed = false, Message = "Address Not Edited" };
            }
            return new ResultSet() { IsSucceed = true, Message = string.Empty };

        }

        public ResultSet DeleteAddress(Guid addressId)
        {

            if (!_AddressRepository.DeleteAddress(addressId))
                return new ResultSet() { IsSucceed = false, Message = "Address Not Deleted" };

            try
            {
                _AddressRepository.Save();
            }
            catch
            {
                return new ResultSet() { IsSucceed = false, Message = "Address Not Deleted" };
            }
            return new ResultSet() { IsSucceed = true, Message = string.Empty };
        }

        public async Task<ResultSet<IEnumerable<AddressInfo>>> GetAddresssAsync()
        {
            return new ResultSet<IEnumerable<AddressInfo>>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = await _AddressRepository.GetAddresssAsync()
            };
        }

        public async Task<ResultSet<AddressInfo>> AddAddressAsync(AddressInfo address)
        {
            

            _AddressRepository.AddAddress(address);

            try{await _AddressRepository.SaveAsync();}

            catch(Exception e){return new ResultSet<AddressInfo>() { IsSucceed = false, Message = e.Message };}

            return new ResultSet<AddressInfo>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = address
            };
        }

        public async Task<ResultSet> EditAddressAsync(AddressInfo address)
        {
            
            if (!_AddressRepository.EditAddress(address))
                return new ResultSet() { IsSucceed = false, Message = "Address Not Edited" };

            try
            {
                await _AddressRepository.SaveAsync();
            }
            catch(Exception e)
            {
                return new ResultSet() { IsSucceed = false, Message = e.Message };
            }
            return new ResultSet() { IsSucceed = true, Message = string.Empty };
        }

        public async Task<ResultSet> DeleteAddressAsync(Guid addressId)
        {



























            if (!_AddressRepository.DeleteAddress(addressId))
                return new ResultSet() { IsSucceed = false, Message = "Address Not Deleted" };

            try
            {
                await _AddressRepository.SaveAsync();
            }
            catch
            {
                return new ResultSet() { IsSucceed = false, Message = "Address Not Deleted" };
            }
            return new ResultSet() { IsSucceed = true, Message = string.Empty };
        }

        public async Task<ResultSet<AddressInfo>> GetAddressByIdAsync(Guid addressId)
        {
            AddressInfo Address = await _AddressRepository.GetAddressByIdAsync(addressId);

            if (Address == null)
                return new ResultSet<AddressInfo>()
                {
                    IsSucceed = false,
                    Message = "Address Not Found",
                    Data = null
                };

            return new ResultSet<AddressInfo>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = Address
            };
        }
    }
}
