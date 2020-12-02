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

        public ServingInventoryService(IServingInventoryRepository servingInventoryRepository)
        {
            this._ServingInventoryRepository = servingInventoryRepository;
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

        public ResultSet<ServingInventoryInfo> GetServingInventoryById(Guid servingInventoryId)
        {
            ServingInventoryInfo ServingInventory = _ServingInventoryRepository.GetServingInventoryById(servingInventoryId);

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

        public async Task<ResultSet<ServingInventoryInfo>> GetServingInventoryByServingIdAsync(Guid servingId)
        {
            ServingInventoryInfo ServingInventory = await _ServingInventoryRepository.GetServingInventoryByServingIdAsync(servingId);

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

     


        public ResultSet<ServingInventoryInfo> AddServingInventory(ServingInventoryInfo  servingInventory)
        {
            _ServingInventoryRepository.AddServingInventory(servingInventory);
            _ServingInventoryRepository.Save();

            return new ResultSet<ServingInventoryInfo>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = servingInventory
            };
        }

        public ResultSet EditServingInventory(ServingInventoryInfo servingInventory)
        {
            if(!_ServingInventoryRepository.EditServingInventory(servingInventory))
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

        public ResultSet DeleteServingInventory(Guid servingInventoryId)
        {

            if (!_ServingInventoryRepository.DeleteServingInventory(servingInventoryId))
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

        public async Task<ResultSet<ServingInventoryInfo>> AddServingInventoryAsync(ServingInventoryInfo servingInventory)
        {
            _ServingInventoryRepository.AddServingInventory(servingInventory);

            try{await _ServingInventoryRepository.SaveAsync();}

            catch(Exception e){return new ResultSet<ServingInventoryInfo>() { IsSucceed = false, Message = e.Message };}

            return new ResultSet<ServingInventoryInfo>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = servingInventory
            };
        }

        public async Task<ResultSet> EditServingInventoryAsync(ServingInventoryInfo servingInventory)
        {
            if (!_ServingInventoryRepository.EditServingInventory(servingInventory))
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

        public async Task<ResultSet> DeleteServingInventoryAsync(Guid servingInventoryId)
        {

             


            if (!_ServingInventoryRepository.DeleteServingInventory(servingInventoryId))
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

        public async Task<ResultSet<ServingInventoryInfo>> GetServingInventoryByIdAsync(Guid servingInventoryId)
        {
            ServingInventoryInfo ServingInventory = await _ServingInventoryRepository.GetServingInventoryByIdAsync(servingInventoryId);

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
