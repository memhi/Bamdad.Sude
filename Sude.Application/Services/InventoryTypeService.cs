using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sude.Application.Interfaces;
using Sude.Application.Result;
using Sude.Domain.Interfaces;
using Sude.Domain.Models.Serving;

namespace Sude.Application.Services
{
    public class InventoryTypeService : IInventoryTypeService
    {
        private IInventoryTypeRepository _InventoryTypeRepository;

        public InventoryTypeService(IInventoryTypeRepository InventoryTypeRepository)
        {
            this._InventoryTypeRepository = InventoryTypeRepository;
        }
        public ResultSet<IEnumerable<InventoryTypeInfo>> GetInventoryTypes()
        {
            return new ResultSet<IEnumerable<InventoryTypeInfo>>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = _InventoryTypeRepository.GetInventoryTypes()
            };
        }

        public ResultSet<InventoryTypeInfo> GetInventoryTypeById(Guid InventoryTypeId)
        {
            InventoryTypeInfo InventoryType = _InventoryTypeRepository.GetInventoryTypeById(InventoryTypeId);

            if (InventoryType == null)
                return new ResultSet<InventoryTypeInfo>()
                {
                    IsSucceed = false,
                    Message = "InventoryType Not Found",
                    Data = null
                };

            return new ResultSet<InventoryTypeInfo>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = InventoryType
            };
        }

        public ResultSet<InventoryTypeInfo> AddInventoryType(InventoryTypeInfo request)
        {
            InventoryTypeInfo inventoryType = _InventoryTypeRepository.GetInventoryTypeByTitle(request.Title);
            if (inventoryType != null && inventoryType.Title == request.Title)
            {
                return new ResultSet<InventoryTypeInfo>() { IsSucceed = false, Message = "Duplicate Data" };

            }
            _InventoryTypeRepository.AddInventoryType(request);
            _InventoryTypeRepository.Save();

            return new ResultSet<InventoryTypeInfo>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = request
            };
        }

        public ResultSet EditInventoryType(InventoryTypeInfo request)
        {
            InventoryTypeInfo inventoryType =  _InventoryTypeRepository.GetInventoryTypeByTitle(request.Title);
            if (inventoryType != null && inventoryType.Id != request.Id)
            {
                return new ResultSet<InventoryTypeInfo>() { IsSucceed = false, Message = "Duplicate Data" };

            }

            if (!_InventoryTypeRepository.EditInventoryType(request))
                return new ResultSet() { IsSucceed = false, Message = "InventoryType Not Edited" };

            try
            {
                _InventoryTypeRepository.Save();
            }
            catch
            {
                return new ResultSet() { IsSucceed = false, Message = "InventoryType Not Edited" };
            }
            return new ResultSet() { IsSucceed = true, Message = string.Empty };

        }

        public ResultSet DeleteInventoryType(Guid InventoryTypeId)
        {

            if (!_InventoryTypeRepository.DeleteInventoryType(InventoryTypeId))
                return new ResultSet() { IsSucceed = false, Message = "InventoryType Not Deleted" };

            try
            {
                _InventoryTypeRepository.Save();
            }
            catch
            {
                return new ResultSet() { IsSucceed = false, Message = "InventoryType Not Deleted" };
            }
            return new ResultSet() { IsSucceed = true, Message = string.Empty };
        }

        public async Task<ResultSet<IEnumerable<InventoryTypeInfo>>> GetInventoryTypesAsync()
        {
            return new ResultSet<IEnumerable<InventoryTypeInfo>>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = await _InventoryTypeRepository.GetInventoryTypesAsync()
            };
        }

        public async Task<ResultSet<InventoryTypeInfo>> AddInventoryTypeAsync(InventoryTypeInfo request)
        {
            InventoryTypeInfo inventoryType = await _InventoryTypeRepository.GetInventoryTypeByTitleAsync(request.Title);
            if (inventoryType != null && inventoryType.Title == request.Title)
            {
                return new ResultSet<InventoryTypeInfo>() { IsSucceed = false, Message ="Duplicate Data" };

            }

           _InventoryTypeRepository.AddInventoryType(request);

            try{await _InventoryTypeRepository.SaveAsync();}

            catch(Exception e){return new ResultSet<InventoryTypeInfo>() { IsSucceed = false, Message = e.Message };}

            return new ResultSet<InventoryTypeInfo>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = request
            };
        }

        public async Task<ResultSet> EditInventoryTypeAsync(InventoryTypeInfo request)
        {
            InventoryTypeInfo inventoryType = await _InventoryTypeRepository.GetInventoryTypeByTitleAsync(request.Title);
            if (inventoryType != null && inventoryType.Id != request.Id)
            {
                return new ResultSet<InventoryTypeInfo>() { IsSucceed = false, Message = "Duplicate Data" };

            }

            if (!_InventoryTypeRepository.EditInventoryType(request))
                return new ResultSet() { IsSucceed = false, Message = "InventoryType Not Edited" };

            try
            {
                await _InventoryTypeRepository.SaveAsync();
            }
            catch(Exception e)
            {
                return new ResultSet() { IsSucceed = false, Message = e.Message };
            }
            return new ResultSet() { IsSucceed = true, Message = string.Empty };
        }

        public async Task<ResultSet> DeleteInventoryTypeAsync(Guid InventoryTypeId)
        {



























            if (!_InventoryTypeRepository.DeleteInventoryType(InventoryTypeId))
                return new ResultSet() { IsSucceed = false, Message = "InventoryType Not Deleted" };

            try
            {
                await _InventoryTypeRepository.SaveAsync();
            }
            catch
            {
                return new ResultSet() { IsSucceed = false, Message = "InventoryType Not Deleted" };
            }
            return new ResultSet() { IsSucceed = true, Message = string.Empty };
        }

        public async Task<ResultSet<InventoryTypeInfo>> GetInventoryTypeByIdAsync(Guid InventoryTypeId)
        {
            InventoryTypeInfo InventoryType = await _InventoryTypeRepository.GetInventoryTypeByIdAsync(InventoryTypeId);

            if (InventoryType == null)
                return new ResultSet<InventoryTypeInfo>()
                {
                    IsSucceed = false,
                    Message = "InventoryType Not Found",
                    Data = null
                };

            return new ResultSet<InventoryTypeInfo>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = InventoryType
            };
        }
    }
}
