using Sude.Application.Result;
using Sude.Domain.Models.Type;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sude.Application.Interfaces
{
    public interface ITypeService
    {

        Task<ResultSet<IEnumerable<TypeInfo>>> GetTypesByGroupKeyAsync(string GroupKey);
        ResultSet<TypeInfo> GetTypeById(Guid TypeId);
        Task<ResultSet<TypeInfo>> GetTypeByIdAsync(Guid TypeId);
        ResultSet<TypeInfo> GetTypeByKey(string TypeKey);
        Task<ResultSet<TypeInfo>> GetTypeByKeyAsync(string TypeKey);
        Task<ResultSet<IEnumerable<TypeGroupInfo>>> GetTypesGroupAsync();
        ResultSet<TypeGroupInfo> GetTypeGroupById(Guid TypeGroupId);
        ResultSet<TypeGroupInfo> GetTypeGroupByKey(string TypeGroupKey);
 
            Task<ResultSet<TypeGroupInfo>> GetTypeGroupByIdAsync(Guid TypeGroupId);
            Task<ResultSet<TypeGroupInfo>> GetTypeGroupByKeyAsync(string TypeGroupKey);



    }
}
