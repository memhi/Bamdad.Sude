using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
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

        public async Task<IEnumerable<InventoryTypeInfo>> SearchInventoryTypesByTitleAsync(string title)
        {
            if (string.IsNullOrEmpty(title))
                return await _InventoryTypeRepository.GetAsync();
            else
                return await _InventoryTypeRepository.GetAsync(i => i.Title.Contains(title));
        }
        public bool AddInventoryType(InventoryTypeInfo inventoryType)
        {
            try
            {
                _InventoryTypeRepository.Insert(inventoryType);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool EditInventoryType(InventoryTypeInfo inventoryType)
        {
            try
            {
                _InventoryTypeRepository.Update(inventoryType);
            }
            catch
            { 
                return false; 
            }
            return true;
        }

        public async Task<InventoryTypeInfo> GetInventoryTypeByIdAsync(Guid inventoryTypeId)
        {
            return await _InventoryTypeRepository.GetByIdAsync(inventoryTypeId);
        }
        public  InventoryTypeInfo GetInventoryTypeByTitle(string title)
        {
            IEnumerable<InventoryTypeInfo> its =  _InventoryTypeRepository.Get(it => it.Title == title);
            if (its != null && its.Count() > 0)
                return its.First();
            return null;
        }

        public async Task<InventoryTypeInfo> GetInventoryTypeByTitleAsync(string title)
        {
            IEnumerable<InventoryTypeInfo> its   = await _InventoryTypeRepository.GetAsync(it => it.Title == title);
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

        

        public bool DeleteInventoryType(Guid inventoryTypeId)
        {
            var inventoryType = GetInventoryTypeById(inventoryTypeId);
            if (inventoryType == null)
                return false;
            try
            {                             
                _InventoryTypeRepository.Delete(inventoryType);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public InventoryTypeInfo GetInventoryTypeById(Guid inventoryTypeId)
        {
            return _InventoryTypeRepository.GetById(inventoryTypeId);
        }
       
    }
}
