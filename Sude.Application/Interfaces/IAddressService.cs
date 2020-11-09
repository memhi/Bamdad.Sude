using Sude.Application.Result;
using Sude.Domain.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sude.Application.Interfaces
{
    public interface IAddressService
    {
        ResultSet<IEnumerable<AddressInfo>> GetAddresss();
        ResultSet<AddressInfo> AddAddress(AddressInfo address);
        ResultSet EditAddress(AddressInfo address);
        ResultSet DeleteAddress(Guid addressId);
        ResultSet<AddressInfo> GetAddressById(Guid addressId);
        Task<ResultSet<IEnumerable<AddressInfo>>> GetAddresssAsync();
        Task<ResultSet<AddressInfo>> AddAddressAsync(AddressInfo address);
        Task<ResultSet> EditAddressAsync(AddressInfo address);
        Task<ResultSet> DeleteAddressAsync(Guid addressId);
        Task<ResultSet<AddressInfo>> GetAddressByIdAsync(Guid addressId);
    }
}
