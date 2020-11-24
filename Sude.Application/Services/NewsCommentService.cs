using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sude.Application.Interfaces;
using Sude.Application.Result;
using Sude.Domain.Interfaces;
using Sude.Domain.Models.Content;

namespace Sude.Application.Services
{
    public class NewsCommentService : INewsCommentService
    {
        private INewsCommentRepository _NewsCommentRepository;

        public NewsCommentService(INewsCommentRepository NewsCommentRepository)
        {
            this._NewsCommentRepository = NewsCommentRepository;
        }
        public ResultSet<IEnumerable<NewsCommentInfo>> GetNewsComments()
        {
            return new ResultSet<IEnumerable<NewsCommentInfo>>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = _NewsCommentRepository.GetNewsComments()
            };
        }

        public ResultSet<NewsCommentInfo> GetNewsCommentById(Guid NewsCommentId)
        {
            NewsCommentInfo NewsComment = _NewsCommentRepository.GetNewsCommentById(NewsCommentId);

            if (NewsComment == null)
                return new ResultSet<NewsCommentInfo>()
                {
                    IsSucceed = false,
                    Message = "NewsComment Not Found",
                    Data = null
                };

            return new ResultSet<NewsCommentInfo>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = NewsComment
            };
        }

        public ResultSet<NewsCommentInfo> AddNewsComment(NewsCommentInfo  NewsComment)
        {


          
 
            _NewsCommentRepository.AddNewsComment(NewsComment);
            _NewsCommentRepository.Save();
           
            return new ResultSet<NewsCommentInfo>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = NewsComment
            };
        }

        public ResultSet EditNewsComment(NewsCommentInfo NewsComment)
        {
           

            if (!_NewsCommentRepository.EditNewsComment(NewsComment))
                return new ResultSet() { IsSucceed = false, Message = "NewsComment Not Edited" };

            try
            {
                _NewsCommentRepository.Save();
            }
            catch
            {
                return new ResultSet() { IsSucceed = false, Message = "NewsComment Not Edited" };
            }
            return new ResultSet() { IsSucceed = true, Message = string.Empty };

        }

        public ResultSet DeleteNewsComment(Guid NewsCommentId)
        {

            if (!_NewsCommentRepository.DeleteNewsComment(NewsCommentId))
                return new ResultSet() { IsSucceed = false, Message = "NewsComment Not Deleted" };

            try
            {
                _NewsCommentRepository.Save();
            }
            catch
            {
                return new ResultSet() { IsSucceed = false, Message = "NewsComment Not Deleted" };
            }
            return new ResultSet() { IsSucceed = true, Message = string.Empty };
        }

        public async Task<ResultSet<IEnumerable<NewsCommentInfo>>> GetNewsCommentsAsync()
        {
            return new ResultSet<IEnumerable<NewsCommentInfo>>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = await _NewsCommentRepository.GetNewsCommentsAsync()
            };
        }

        public async Task<ResultSet<NewsCommentInfo>> AddNewsCommentAsync(NewsCommentInfo NewsComment)
        {
            

            _NewsCommentRepository.AddNewsComment(NewsComment);

            try{await _NewsCommentRepository.SaveAsync();}

            catch(Exception e){return new ResultSet<NewsCommentInfo>() { IsSucceed = false, Message = e.Message };}

            return new ResultSet<NewsCommentInfo>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = NewsComment
            };
        }

        public async Task<ResultSet> EditNewsCommentAsync(NewsCommentInfo NewsComment)
        {
            
            if (!_NewsCommentRepository.EditNewsComment(NewsComment))
                return new ResultSet() { IsSucceed = false, Message = "NewsComment Not Edited" };

            try
            {
                await _NewsCommentRepository.SaveAsync();
            }
            catch(Exception e)
            {
                return new ResultSet() { IsSucceed = false, Message = e.Message };
            }
            return new ResultSet() { IsSucceed = true, Message = string.Empty };
        }

        public async Task<ResultSet> DeleteNewsCommentAsync(Guid NewsCommentId)
        {



























            if (!_NewsCommentRepository.DeleteNewsComment(NewsCommentId))
                return new ResultSet() { IsSucceed = false, Message = "NewsComment Not Deleted" };

            try
            {
                await _NewsCommentRepository.SaveAsync();
            }
            catch
            {
                return new ResultSet() { IsSucceed = false, Message = "NewsComment Not Deleted" };
            }
            return new ResultSet() { IsSucceed = true, Message = string.Empty };
        }

        public async Task<ResultSet<NewsCommentInfo>> GetNewsCommentByIdAsync(Guid NewsCommentId)
        {
            NewsCommentInfo NewsComment = await _NewsCommentRepository.GetNewsCommentByIdAsync(NewsCommentId);

            if (NewsComment == null)
                return new ResultSet<NewsCommentInfo>()
                {
                    IsSucceed = false,
                    Message = "NewsComment Not Found",
                    Data = null
                };

            return new ResultSet<NewsCommentInfo>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = NewsComment
            };
        }
    }
}
