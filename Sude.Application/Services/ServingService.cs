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
    public class ServingService : IServingService
    {
        private IServingRepository _servingRepository;

        public ServingService(IServingRepository servingRepository)
        {
            this._servingRepository = servingRepository;
        }
        public ResultSet<IEnumerable<Serving>> GetServings()
        {
            return new ResultSet<IEnumerable<Serving>>()
            {
                IsSucced = true,
                Message = string.Empty,
                Data = _servingRepository.GetServings()
            };
        }

        public ResultSet<Serving> GetServingById(Guid servingId)
        {
            Serving serving = _servingRepository.GetServingById(servingId);

            if (serving == null)
                return new ResultSet<Serving>()
                {
                    IsSucced = false,
                    Message = "Serving Not Found",
                    Data = null
                };

            return new ResultSet<Serving>()
            {
                IsSucced = true,
                Message = string.Empty,
                Data = serving
            };
        }

        public ResultSet<Serving> AddServing(Serving request)
        {
            _servingRepository.AddServing(request);
            _servingRepository.Save();

            return new ResultSet<Serving>()
            {
                IsSucced = true,
                Message = string.Empty,
                Data = request
            };
        }

        public ResultSet EditServing(Serving request)
        {
            if(!_servingRepository.EditServing(request))
                return new ResultSet() { IsSucced = false, Message = "Serving Not Edited" };

            try
            {
                _servingRepository.Save();
            }
            catch
            {
                return new ResultSet() { IsSucced = false, Message = "Serving Not Edited" };
            }
            return new ResultSet() { IsSucced = true, Message = string.Empty };

        }

        public ResultSet DeleteServing(Guid servingId)
        {

            if (!_servingRepository.DeleteServing(servingId))
                return new ResultSet() { IsSucced = false, Message = "Serving Not Deleted" };

            try
            {
                _servingRepository.Save();
            }
            catch
            {
                return new ResultSet() { IsSucced = false, Message = "Serving Not Deleted" };
            }
            return new ResultSet() { IsSucced = true, Message = string.Empty };
        }

        public async Task<ResultSet<IEnumerable<Serving>>> GetServingsAsync()
        {
            return new ResultSet<IEnumerable<Serving>>()
            {
                IsSucced = true,
                Message = string.Empty,
                Data = await _servingRepository.GetServingsAsync()
            };
        }

        public async Task<ResultSet<Serving>> AddServingAsync(Serving request)
        {
            _servingRepository.AddServing(request);

            try{await _servingRepository.SaveAsync();}

            catch(Exception e){return new ResultSet<Serving>() { IsSucced = false, Message = e.Message };}

            return new ResultSet<Serving>()
            {
                IsSucced = true,
                Message = string.Empty,
                Data = request
            };
        }

        public async Task<ResultSet> EditServingAsync(Serving request)
        {
            if (!_servingRepository.EditServing(request))
                return new ResultSet() { IsSucced = false, Message = "Serving Not Edited" };

            try
            {
                await _servingRepository.SaveAsync();
            }
            catch(Exception e)
            {
                return new ResultSet() { IsSucced = false, Message = e.Message };
            }
            return new ResultSet() { IsSucced = true, Message = string.Empty };
        }

        public async Task<ResultSet> DeleteServingAsync(Guid servingId)
        {
            if (!_servingRepository.DeleteServing(servingId))
                return new ResultSet() { IsSucced = false, Message = "Serving Not Deleted" };

            try
            {
                await _servingRepository.SaveAsync();
            }
            catch
            {
                return new ResultSet() { IsSucced = false, Message = "Serving Not Deleted" };
            }
            return new ResultSet() { IsSucced = true, Message = string.Empty };
        }

        public async Task<ResultSet<Serving>> GetServingByIdAsync(Guid servingId)
        {
            Serving serving = await _servingRepository.GetServingByIdAsync(servingId);

            if (serving == null)
                return new ResultSet<Serving>()
                {
                    IsSucced = false,
                    Message = "Serving Not Found",
                    Data = null
                };

            return new ResultSet<Serving>()
            {
                IsSucced = true,
                Message = string.Empty,
                Data = serving
            };
        }
    }
}
