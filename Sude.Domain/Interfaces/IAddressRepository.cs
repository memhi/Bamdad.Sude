using Sude.Domain.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sude.Domain.Interfaces
{
    public interface IAddressRepository
    {
        Task<IEnumerable<AddressInfo>> GetAddresssAsync();
        IEnumerable<AddressInfo> GetAddresss();
 

        bool AddAddress(AddressInfo address);
        bool EditAddress(AddressInfo address);
        bool DeleteAddress(Guid addressId);

        Task<AddressInfo> GetAddressByIdAsync(Guid addressId);
        AddressInfo GetAddressById(Guid addressId);
       

        void Save();
        Task SaveAsync();
    }
}
