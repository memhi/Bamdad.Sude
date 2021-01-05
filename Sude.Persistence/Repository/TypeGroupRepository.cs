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
    public class TypeGroupRepository : ITypeGroupRepository
    {

        private GenericRepository<TypeGroupInfo> _TypeGroupRepository;

        public TypeGroupRepository(SudeDBContext ctx)
        {
            //this._ctx = ctx;
            this._TypeGroupRepository = new GenericRepository<TypeGroupInfo>(ctx);
        }

   
       

       
        public IEnumerable<TypeGroupInfo> GetTypeGroups()
        {
            //return _ctx.TypeGroups;
            return _TypeGroupRepository.Get();
        }
        public async Task<IEnumerable<TypeGroupInfo>> GetTypeGroupsAsync()
        {
            //return _ctx.TypeGroups;
            return await _TypeGroupRepository.GetAsync();
        }
        public void AddTypeGroup(TypeGroupInfo TypeGroup)
        {
            //_ctx.TypeGroups.Add(TypeGroup);
            _TypeGroupRepository.Insert(TypeGroup);
        }

        public TypeGroupInfo GetTypeGroupByKey(string TypeGroupKey)
        {
      
            return  _TypeGroupRepository.Get(t=>t.TypeGroupKey==TypeGroupKey).FirstOrDefault();
        }


        public async Task<TypeGroupInfo> GetTypeGroupByKeyAsync(string TypeGroupKey)
        {

            IEnumerable<TypeGroupInfo> typeGroups = _TypeGroupRepository.Get(t => t.TypeGroupKey == TypeGroupKey);
            if (typeGroups != null && typeGroups.Any())
                return typeGroups.FirstOrDefault();
            return null;
        }



        public TypeGroupInfo GetTypeGroupById(Guid TypeGroupId)
        {
            //return _ctx.TypeGroups.Find(TypeGroupId);
            return _TypeGroupRepository.GetById(TypeGroupId);
        }

        public async Task<TypeGroupInfo> GetTypeGroupByIdAsync(Guid TypeGroupId)
        {
            //return _ctx.TypeGroups.Find(TypeGroupId);
            return await _TypeGroupRepository.GetByIdAsync(TypeGroupId);
        }
        public bool EditTypeGroup(TypeGroupInfo TypeGroup)
        {
            //_ctx.Entry(TypeGroup).State = EntityState.Modified;
            ////_ctx.TypeGroups.Update(TypeGroup);
            _TypeGroupRepository.Update(TypeGroup);
            return true;
        }
        public void Save()
        {
            //_ctx.SaveChanges();
            _TypeGroupRepository.Save();
        }

       
    }
}
