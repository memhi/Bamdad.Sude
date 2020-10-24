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
    public class ServingInventoryService : IServingInventoryService
    {
        private IServingInventoryRepository _ServingInventoryRepository;

        public ServingInventoryService(IServingInventoryRepository ServingInventoryRepository)
        {
            this._ServingInventoryRepository = ServingInventoryRepository;
        }
        public ResultSet<IEnumerable<ServingInventoryInfo>> GetServingInventorys()
        {
            return new ResultSet<IEnumerable<ServingInventoryInfo>>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = _ServingInventoryRepository.GetServingInventorys()
            };
        }

        public ResultSet<ServingInventoryInfo> GetServingInventoryById(Guid ServingInventoryId)
        {
            ServingInventoryInfo ServingInventory = _ServingInventoryRepository.GetServingInventoryById(ServingInventoryId);

            if (ServingInventory == null)
                return new ResultSet<ServingInventoryInfo>()
                {
                    IsSucceed = false,
                    Message = "ServingInventory Not Found",
                    Data = null
                };

            return new ResultSet<ServingInventoryInfo>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = ServingInventory
            };
        }

        public ResultSet<ServingInventoryInfo> AddServingInventory(ServingInventoryInfo request)
        {
            _ServingInventoryRepository.AddServingInventory(request);
            _ServingInventoryRepository.Save();

            return new ResultSet<ServingInventoryInfo>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = request
            };
        }

        public ResultSet EditServingInventory(ServingInventoryInfo request)
        {
            if(!_ServingInventoryRepository.EditServingInventory(request))
                return new ResultSet() { IsSucceed = false, Message = "ServingInventory Not Edited" };

            try
            {
                _ServingInventoryRepository.Save();
            }
            catch
            {
                return new ResultSet() { IsSucceed = false, Message = "ServingInventory Not Edited" };
            }
            return new ResultSet() { IsSucceed = true, Message = string.Empty };

        }

        public ResultSet DeleteServingInventory(Guid ServingInventoryId)
        {

            if (!_ServingInventoryRepository.DeleteServingInventory(ServingInventoryId))
                return new ResultSet() { IsSucceed = false, Message = "ServingInventory Not Deleted" };

            try
            {
                _ServingInventoryRepository.Save();
            }
            catch
            {
                return new ResultSet() { IsSucceed = false, Message = "ServingInventory Not Deleted" };
            }
            return new ResultSet() { IsSucceed = true, Message = string.Empty };
        }

        public async Task<ResultSet<IEnumerable<ServingInventoryInfo>>> GetServingInventorysAsync()
        {
            return new ResultSet<IEnumerable<ServingInventoryInfo>>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = await _ServingInventoryRepository.GetServingInventorysAsync()
            };
        }

        public async Task<ResultSet<ServingInventoryInfo>> AddServingInventoryAsync(ServingInventoryInfo request)
        {
            _ServingInventoryRepository.AddServingInventory(request);

            try{await _ServingInventoryRepository.SaveAsync();}

            catch(Exception e){return new ResultSet<ServingInventoryInfo>() { IsSucceed = false, Message = e.Message };}

            return new ResultSet<ServingInventoryInfo>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = request
            };
        }

        public async Task<ResultSet> EditServingInventoryAsync(ServingInventoryInfo request)
        {
            if (!_ServingInventoryRepository.EditServingInventory(request))
                return new ResultSet() { IsSucceed = false, Message = "ServingInventory Not Edited" };

            try
            {
                await _ServingInventoryRepository.SaveAsync();
            }
            catch(Exception e)
            {
                return new ResultSet() { IsSucceed = false, Message = e.Message };
            }
            return new ResultSet() { IsSucceed = true, Message = string.Empty };
        }

        public async Task<ResultSet> DeleteServingInventoryAsync(Guid ServingInventoryId)
        {



























            if (!_ServingInventoryRepository.DeleteServingInventory(ServingInventoryId))
                return new ResultSet() { IsSucceed = false, Message = "ServingInventory Not Deleted" };

            try
            {
                await _ServingInventoryRepository.SaveAsync();
            }
            catch
            {
                return new ResultSet() { IsSucceed = false, Message = "ServingInventory Not Deleted" };
            }
            return new ResultSet() { IsSucceed = true, Message = string.Empty };
        }

        public async Task<ResultSet<ServingInventoryInfo>> GetServingInventoryByIdAsync(Guid ServingInventoryId)
        {
            ServingInventoryInfo ServingInventory = await _ServingInventoryRepository.GetServingInventoryByIdAsync(ServingInventoryId);

            if (ServingInventory == null)
                return new ResultSet<ServingInventoryInfo>()
                {
                    IsSucceed = false,
                    Message = "ServingInventory Not Found",
                    Data = null
                };

            return new ResultSet<ServingInventoryInfo>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = ServingInventory
            };
        }
    }
}
