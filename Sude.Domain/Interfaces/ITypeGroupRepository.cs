using Sude.Domain.Models.Type;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sude.Domain.Interfaces
{
    public interface ITypeGroupRepository
    {
        IEnumerable<TypeGroupInfo> GetTypeGroups();
       Task<IEnumerable<TypeGroupInfo>> GetTypeGroupsAsync();

      
        void AddTypeGroup(TypeGroupInfo TypeGroup);

        TypeGroupInfo GetTypeGroupByKey(string TypeGroupKey);
        Task<TypeGroupInfo> GetTypeGroupByKeyAsync(string TypeGroupKey);
        TypeGroupInfo GetTypeGroupById(Guid TypeGroupId);

        Task<TypeGroupInfo> GetTypeGroupByIdAsync(Guid TypeGroupId);

        bool EditTypeGroup(TypeGroupInfo TypeGroup);

        void Save();
    }
}
