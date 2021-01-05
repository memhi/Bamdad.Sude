using Sude.Domain.Models.Type;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sude.Domain.Interfaces
{
    public interface ITypeRepository
    {
       
        void AddType(TypeInfo Type);
        Task<IEnumerable<TypeInfo>> GetTypesAsync();

            IEnumerable<TypeInfo> GetTypes();
        TypeInfo GetTypeById(Guid TypeId);
        Task<TypeInfo> GetTypeByIdAsync(Guid TypeId);
        TypeInfo GetTypeByKey(string TypeKey);
        Task<TypeInfo> GetTypeByKeyAsync(string TypeKey);
        Task<IEnumerable<TypeInfo>> GetTypesByGroupKeyAsync(string GroupTypeKey);
        Task<IEnumerable<TypeInfo>> GetTypesByGroupIdAsync(Guid TypeGroupId);

        bool EditType(TypeInfo Type);

        void Save();
    }
}
