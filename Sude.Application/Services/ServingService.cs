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

       

        //public ResultSet<IEnumerable<ServingInfo>> GetServings()
        //{
        //    return new ResultSet<IEnumerable<ServingInfo>>()
        //    {
        //        IsSucceed = true,
        //        Message = string.Empty,
        //        Data = _servingRepository.GetServings()
        //    };
        //}

        //public ResultSet<ServingInfo> GetServingById(Guid servingId)
        //{
        //    ServingInfo serving = _servingRepository.GetServingById(servingId);

        //    if (serving == null)
        //        return new ResultSet<ServingInfo>()
        //        {
        //            IsSucceed = false,
        //            Message = "Serving Not Found",
        //            Data = null
        //        };

        //    return new ResultSet<ServingInfo>()
        //    {
        //        IsSucceed = true,
        //        Message = string.Empty,
        //        Data = serving
        //    };
        //}

        //public ResultSet<ServingInfo> AddServing(ServingInfo request)
        //{

        //    if (_servingRepository.IsExistServing(request.Title, null, request.Work.Id))
        //    {
        //        return new ResultSet<ServingInfo>()
        //        {
        //            IsSucceed = false,
        //            Message = "Duplicate Date",
        //            Data = null
        //        };
        //    }

        //    _servingRepository.AddServing(request);
        //    _servingRepository.Save();

        //    return new ResultSet<ServingInfo>()
        //    {
        //        IsSucceed = true,
        //        Message = string.Empty,
        //        Data = request
        //    };
        //}

        //public ResultSet EditServing(ServingInfo request)
        //{
        //    if (_servingRepository.IsExistServing(request.Title, request.Id, request.Work.Id))
        //        return new ResultSet<ServingInfo>()
        //        {
        //            IsSucceed = false,
        //            Message = "Duplicate Date",
        //            Data = null
        //        };

        //    if (!_servingRepository.EditServing(request))
        //        return new ResultSet() { IsSucceed = false, Message = "Serving Not Edited" };

        //    try
        //    {
        //        _servingRepository.Save();
        //    }
        //    catch
        //    {
        //        return new ResultSet() { IsSucceed = false, Message = "Serving Not Edited" };
        //    }
        //    return new ResultSet() { IsSucceed = true, Message = string.Empty };

        //}

        //public ResultSet DeleteServing(Guid servingId)
        //{

        //    if (!_servingRepository.DeleteServing(servingId))
        //        return new ResultSet() { IsSucceed = false, Message = "Serving Not Deleted" };

        //    try
        //    {
        //        _servingRepository.Save();
        //    }
        //    catch
        //    {
        //        return new ResultSet() { IsSucceed = false, Message = "Serving Not Deleted" };
        //    }
        //    return new ResultSet() { IsSucceed = true, Message = string.Empty };
        //}

        public async Task<ResultSet<IEnumerable<ServingInfo>>> GetServingsByWorkIdAsync(Guid workId)
        {
            return new ResultSet<IEnumerable<ServingInfo>>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = await _servingRepository.GetServingsByWorkIdAsync(workId)
            };
        }

        public async Task<ResultSet<IEnumerable<ServingInfo>>> GetServingsByWorkIdAndHasTrackingAsync(Guid workId)
        {
            return new ResultSet<IEnumerable<ServingInfo>>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = await _servingRepository.GetServingsByWorkIdAndHasTrackingAsync(workId)
            };
        }


        public async Task<ResultSet<IEnumerable<ServingInfo>>> GetServingsAsync()
        {
            return new ResultSet<IEnumerable<ServingInfo>>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = await _servingRepository.GetServingsAsync()
            };
        }

        public async Task<ResultSet<ServingInfo>> AddServingAsync(ServingInfo request)
        {
            if (_servingRepository.IsExistServing(request.Title, null, request.Work.Id))
                return new ResultSet<ServingInfo>()
            {
                IsSucceed = false,
                Message = "Duplicate Date",
                Data = null
            };

            _servingRepository.AddServing(request);

            try{await _servingRepository.SaveAsync();}

            catch(Exception e){return new ResultSet<ServingInfo>() { IsSucceed = false, Message = e.Message };}

            return new ResultSet<ServingInfo>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = request
            };
        }

        public async Task<ResultSet> EditServingAsync(ServingInfo request)
        {
            if (_servingRepository.IsExistServing(request.Title, request.Id, request.Work.Id))
                return new ResultSet<ServingInfo>()
                {
                    IsSucceed = false,
                    Message = "Duplicate Date",
                    Data = null
                };

            if (!_servingRepository.EditServing(request))
                return new ResultSet() { IsSucceed = false, Message = "Serving Not Edited" };

            try
            {
                await _servingRepository.SaveAsync();
            }
            catch(Exception e)
            {
                return new ResultSet() { IsSucceed = false, Message = e.Message };
            }
            return new ResultSet() { IsSucceed = true, Message = string.Empty };
        }

        public async Task<ResultSet> DeleteServingAsync(Guid servingId)
        {



























            if (!_servingRepository.DeleteServing(servingId))
                return new ResultSet() { IsSucceed = false, Message = "Serving Not Deleted" };

            try
            {
                await _servingRepository.SaveAsync();
            }
            catch
            {
                return new ResultSet() { IsSucceed = false, Message = "Serving Not Deleted" };
            }
            return new ResultSet() { IsSucceed = true, Message = string.Empty };
        }

        public async Task<ResultSet<ServingInfo>> GetServingByIdAsync(Guid servingId)
        {
            ServingInfo serving = await _servingRepository.GetServingByIdAsync(servingId);

            if (serving == null)
                return new ResultSet<ServingInfo>()
                {
                    IsSucceed = false,
                    Message = "Serving Not Found",
                    Data = null
                };

            return new ResultSet<ServingInfo>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = serving
            };
        }
    }
}
