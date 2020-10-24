using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sude.Domain.Interfaces;
using Sude.Domain.Models.Serving;
using Sude.Persistence.Contexts;

namespace Sude.Persistence.Repository
{
    public class InventoryTypeRepository : IInventoryTypeRepository
    {

        private GenericRepository<InventoryTypeInfo> _InventoryTypeRepository;

        public InventoryTypeRepository(SudeDBContext ctx)
        {
            this._InventoryTypeRepository = new GenericRepository<InventoryTypeInfo>(ctx);
        }

        public async Task<IEnumerable<InventoryTypeInfo>> GetInventoryTypesAsync()
        {
            return await _InventoryTypeRepository.GetAsync();
        }
        public bool AddInventoryType(InventoryTypeInfo InventoryType)
        {
            try
            {
                _InventoryTypeRepository.Insert(InventoryType);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool EditInventoryType(InventoryTypeInfo InventoryType)
        {
            try
            {
                _InventoryTypeRepository.Update(InventoryType);
            }
            catch
            { 
                return false; 
            }
            return true;
        }

        public async Task<InventoryTypeInfo> GetInventoryTypeByIdAsync(Guid InventoryTypeId)
        {
            return await _InventoryTypeRepository.GetByIdAsync(InventoryTypeId);
        }
        public  InventoryTypeInfo GetInventoryTypeByTitle(string Title)
        {
            IEnumerable<InventoryTypeInfo> its =  _InventoryTypeRepository.Get(it => it.Title == Title);
            if (its != null && its.Count() > 0)
                return its.First();
            return null;
        }

        public async Task<InventoryTypeInfo> GetInventoryTypeByTitleAsync(string Title)
        {
            IEnumerable<InventoryTypeInfo> its   = await _InventoryTypeRepository.GetAsync(it => it.Title == Title);
            if (its != null && its.Count() > 0)
                return its.First();
            return null;
        }
        

        public void Save()
        {
            _InventoryTypeRepository.Save();
        }

        public async Task SaveAsync() =>
            await _InventoryTypeRepository.SaveAsync();

        public IEnumerable<InventoryTypeInfo> GetInventoryTypes()
        {
            return _InventoryTypeRepository.Get();
        }

        public bool IsExistInventoryType(string title)
        {
            return (_InventoryTypeRepository.Get(p => p.Title == title).Count() > 0 ? true : false);
        }

        

        public bool DeleteInventoryType(Guid InventoryTypeId)
        {
            var InventoryType = GetInventoryTypeById(InventoryTypeId);
            if (InventoryType == null)
                return false;
            try
            {                             
                _InventoryTypeRepository.Delete(InventoryType);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public InventoryTypeInfo GetInventoryTypeById(Guid InventoryTypeId)
        {
            return _InventoryTypeRepository.GetById(InventoryTypeId);
        }
       
    }
}
