using Sude.Domain.Interfaces;
using Sude.Domain.Models.Type;
using Sude.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sude.Persistence.Repository
{
    public class TypeRepository : ITypeRepository
    {

        private GenericRepository<TypeInfo> _TypeRepository;

        public TypeRepository(SudeDBContext ctx)
        {
            //this._ctx = ctx;
            this._TypeRepository = new GenericRepository<TypeInfo>(ctx);
        }

   
       

       
        public IEnumerable<TypeInfo> GetTypes()
        {
            //return _ctx.Types;
            return _TypeRepository.Get();
        }

        public async Task<IEnumerable<TypeInfo>> GetTypesAsync()
        {
            //return _ctx.Types;
            return await _TypeRepository.GetAsync();
        }
        public void AddType(TypeInfo Type)
        {
            //_ctx.Types.Add(Type);
            _TypeRepository.Insert(Type);
        }

        public TypeInfo GetTypeByKey(string TypeKey)
        {
      
            return  _TypeRepository.Get(t=>t.TypeKey==TypeKey).FirstOrDefault();
        }


        public async Task<TypeInfo> GetTypeByKeyAsync(string TypeKey)
        {

         IEnumerable<TypeInfo> types=   await _TypeRepository.GetAsync(t => t.TypeKey == TypeKey);
            if (types != null && types.Any())
                return types.FirstOrDefault();
            return null;
        }

        public async Task<IEnumerable<TypeInfo>> GetTypesByGroupKeyAsync(string GroupTypeKey)
        {

            return await _TypeRepository.GetAsync(t => t.TypeGroup.TypeGroupKey == GroupTypeKey);
        }

        public async Task<IEnumerable<TypeInfo>> GetTypesByGroupIdAsync(Guid TypeGroupId)
        {

            return await _TypeRepository.GetAsync(t => t.TypeGroupId == TypeGroupId);
        }
        public TypeInfo GetTypeById(Guid TypeId)
        {
            //return _ctx.Types.Find(TypeId);
            return _TypeRepository.GetById(TypeId);
        }
        public async  Task<TypeInfo> GetTypeByIdAsync(Guid TypeId)
        {
            //return _ctx.Types.Find(TypeId);
            return await _TypeRepository.GetByIdAsync(TypeId);
        }
        public bool EditType(TypeInfo Type)
        {
            //_ctx.Entry(Type).State = EntityState.Modified;
            ////_ctx.Types.Update(Type);
            _TypeRepository.Update(Type);
            return true;
        }
        public void Save()
        {
            //_ctx.SaveChanges();
            _TypeRepository.Save();
        }

       
    }
}
