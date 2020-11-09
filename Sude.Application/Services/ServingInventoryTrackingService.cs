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
    public class ServingInventoryTrackingService : IServingInventoryTrackingService
    {
        private IServingInventoryTrackingRepository _ServingInventoryTrackingRepository;

        public ServingInventoryTrackingService(IServingInventoryTrackingRepository servingInventoryTrackingRepository)
        {
            this._ServingInventoryTrackingRepository = servingInventoryTrackingRepository;
        }
        public ResultSet<IEnumerable<ServingInventoryTrackingInfo>> GetServingInventoryTrackings()
        {
            return new ResultSet<IEnumerable<ServingInventoryTrackingInfo>>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = _ServingInventoryTrackingRepository.GetServingInventoryTrackings()
            };
        }

        public ResultSet<ServingInventoryTrackingInfo> GetServingInventoryTrackingById(Guid servingInventoryTrackingId)
        {
            ServingInventoryTrackingInfo ServingInventoryTracking = _ServingInventoryTrackingRepository.GetServingInventoryTrackingById(servingInventoryTrackingId);

            if (ServingInventoryTracking == null)
                return new ResultSet<ServingInventoryTrackingInfo>()
                {
                    IsSucceed = false,
                    Message = "ServingInventoryTracking Not Found",
                    Data = null
                };

            return new ResultSet<ServingInventoryTrackingInfo>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = ServingInventoryTracking
            };
        }

        public ResultSet<ServingInventoryTrackingInfo> AddServingInventoryTracking(ServingInventoryTrackingInfo  servingInventoryTracking)
        {
            _ServingInventoryTrackingRepository.AddServingInventoryTracking(servingInventoryTracking);
            _ServingInventoryTrackingRepository.Save();

            return new ResultSet<ServingInventoryTrackingInfo>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = servingInventoryTracking
            };
        }

        public ResultSet EditServingInventoryTracking(ServingInventoryTrackingInfo servingInventoryTracking)
        {
            if(!_ServingInventoryTrackingRepository.EditServingInventoryTracking(servingInventoryTracking))
                return new ResultSet() { IsSucceed = false, Message = "ServingInventoryTracking Not Edited" };

            try
            {
                _ServingInventoryTrackingRepository.Save();
            }
            catch
            {
                return new ResultSet() { IsSucceed = false, Message = "ServingInventoryTracking Not Edited" };
            }
            return new ResultSet() { IsSucceed = true, Message = string.Empty };

        }

        public ResultSet DeleteServingInventoryTracking(Guid servingInventoryTrackingId)
        {

            if (!_ServingInventoryTrackingRepository.DeleteServingInventoryTracking(servingInventoryTrackingId))
                return new ResultSet() { IsSucceed = false, Message = "ServingInventoryTracking Not Deleted" };

            try
            {
                _ServingInventoryTrackingRepository.Save();
            }
            catch
            {
                return new ResultSet() { IsSucceed = false, Message = "ServingInventoryTracking Not Deleted" };
            }
            return new ResultSet() { IsSucceed = true, Message = string.Empty };
        }

        public async Task<ResultSet<IEnumerable<ServingInventoryTrackingInfo>>> GetServingInventoryTrackingsAsync()
        {
            return new ResultSet<IEnumerable<ServingInventoryTrackingInfo>>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = await _ServingInventoryTrackingRepository.GetServingInventoryTrackingsAsync()
            };
        }

        public async Task<ResultSet<ServingInventoryTrackingInfo>> AddServingInventoryTrackingAsync(ServingInventoryTrackingInfo servingInventoryTracking)
        {
            _ServingInventoryTrackingRepository.AddServingInventoryTracking(servingInventoryTracking);

            try{await _ServingInventoryTrackingRepository.SaveAsync();}

            catch(Exception e){return new ResultSet<ServingInventoryTrackingInfo>() { IsSucceed = false, Message = e.Message };}

            return new ResultSet<ServingInventoryTrackingInfo>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = servingInventoryTracking
            };
        }

        public async Task<ResultSet> EditServingInventoryTrackingAsync(ServingInventoryTrackingInfo servingInventoryTracking)
        {
            if (!_ServingInventoryTrackingRepository.EditServingInventoryTracking(servingInventoryTracking))
                return new ResultSet() { IsSucceed = false, Message = "ServingInventoryTracking Not Edited" };

            try
            {
                await _ServingInventoryTrackingRepository.SaveAsync();
            }
            catch(Exception e)
            {
                return new ResultSet() { IsSucceed = false, Message = e.Message };
            }
            return new ResultSet() { IsSucceed = true, Message = string.Empty };
        }

        public async Task<ResultSet> DeleteServingInventoryTrackingAsync(Guid servingInventoryTrackingId)
        {






            if (!_ServingInventoryTrackingRepository.DeleteServingInventoryTracking(servingInventoryTrackingId))
                return new ResultSet() { IsSucceed = false, Message = "ServingInventoryTracking Not Deleted" };

            try
            {
                await _ServingInventoryTrackingRepository.SaveAsync();
            }
            catch
            {
                return new ResultSet() { IsSucceed = false, Message = "ServingInventoryTracking Not Deleted" };
            }
            return new ResultSet() { IsSucceed = true, Message = string.Empty };
        }

        public async Task<ResultSet<ServingInventoryTrackingInfo>> GetServingInventoryTrackingByIdAsync(Guid servingInventoryTrackingId)
        {
            ServingInventoryTrackingInfo ServingInventoryTracking = await _ServingInventoryTrackingRepository.GetServingInventoryTrackingByIdAsync(servingInventoryTrackingId);

            if (ServingInventoryTracking == null)
                return new ResultSet<ServingInventoryTrackingInfo>()
                {
                    IsSucceed = false,
                    Message = "ServingInventoryTracking Not Found",
                    Data = null
                };

            return new ResultSet<ServingInventoryTrackingInfo>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = ServingInventoryTracking
            };
        }
    }
}
