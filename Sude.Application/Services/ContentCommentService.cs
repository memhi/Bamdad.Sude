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
    public class ContentCommentService : IContentCommentService
    {
        private IContentCommentRepository _ContentCommentRepository;

        public ContentCommentService(IContentCommentRepository contentCommentRepository)
        {
            this._ContentCommentRepository = contentCommentRepository;
        }
        public ResultSet<IEnumerable<ContentCommentInfo>> GetContentComments()
        {
            return new ResultSet<IEnumerable<ContentCommentInfo>>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = _ContentCommentRepository.GetContentComments()
            };
        }

        public ResultSet<ContentCommentInfo> GetContentCommentById(Guid contentCommentId)
        {
            ContentCommentInfo ContentComment = _ContentCommentRepository.GetContentCommentById(contentCommentId);

            if (ContentComment == null)
                return new ResultSet<ContentCommentInfo>()
                {
                    IsSucceed = false,
                    Message = "ContentComment Not Found",
                    Data = null
                };

            return new ResultSet<ContentCommentInfo>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = ContentComment
            };
        }

        public ResultSet<ContentCommentInfo> AddContentComment(ContentCommentInfo  contentComment)
        {


          
 
            _ContentCommentRepository.AddContentComment(contentComment);
            _ContentCommentRepository.Save();
           
            return new ResultSet<ContentCommentInfo>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = contentComment
            };
        }

        public ResultSet EditContentComment(ContentCommentInfo contentComment)
        {
           

            if (!_ContentCommentRepository.EditContentComment(contentComment))
                return new ResultSet() { IsSucceed = false, Message = "ContentComment Not Edited" };

            try
            {
                _ContentCommentRepository.Save();
            }
            catch
            {
                return new ResultSet() { IsSucceed = false, Message = "ContentComment Not Edited" };
            }
            return new ResultSet() { IsSucceed = true, Message = string.Empty };

        }

        public ResultSet DeleteContentComment(Guid contentCommentId)
        {

            if (!_ContentCommentRepository.DeleteContentComment(contentCommentId))
                return new ResultSet() { IsSucceed = false, Message = "ContentComment Not Deleted" };

            try
            {
                _ContentCommentRepository.Save();
            }
            catch
            {
                return new ResultSet() { IsSucceed = false, Message = "ContentComment Not Deleted" };
            }
            return new ResultSet() { IsSucceed = true, Message = string.Empty };
        }

        public async Task<ResultSet<IEnumerable<ContentCommentInfo>>> GetContentCommentsAsync()
        {
            return new ResultSet<IEnumerable<ContentCommentInfo>>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = await _ContentCommentRepository.GetContentCommentsAsync()
            };
        }

        public async Task<ResultSet<ContentCommentInfo>> AddContentCommentAsync(ContentCommentInfo contentComment)
        {
            

            _ContentCommentRepository.AddContentComment(contentComment);

            try{await _ContentCommentRepository.SaveAsync();}

            catch(Exception e){return new ResultSet<ContentCommentInfo>() { IsSucceed = false, Message = e.Message };}

            return new ResultSet<ContentCommentInfo>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = contentComment
            };
        }

        public async Task<ResultSet> EditContentCommentAsync(ContentCommentInfo contentComment)
        {
            
            if (!_ContentCommentRepository.EditContentComment(contentComment))
                return new ResultSet() { IsSucceed = false, Message = "ContentComment Not Edited" };

            try
            {
                await _ContentCommentRepository.SaveAsync();
            }
            catch(Exception e)
            {
                return new ResultSet() { IsSucceed = false, Message = e.Message };
            }
            return new ResultSet() { IsSucceed = true, Message = string.Empty };
        }

        public async Task<ResultSet> DeleteContentCommentAsync(Guid contentCommentId)
        {



























            if (!_ContentCommentRepository.DeleteContentComment(contentCommentId))
                return new ResultSet() { IsSucceed = false, Message = "ContentComment Not Deleted" };

            try
            {
                await _ContentCommentRepository.SaveAsync();
            }
            catch
            {
                return new ResultSet() { IsSucceed = false, Message = "ContentComment Not Deleted" };
            }
            return new ResultSet() { IsSucceed = true, Message = string.Empty };
        }

        public async Task<ResultSet<ContentCommentInfo>> GetContentCommentByIdAsync(Guid contentCommentId)
        {
            ContentCommentInfo contentComment = await _ContentCommentRepository.GetContentCommentByIdAsync(contentCommentId);

            if (contentComment == null)
                return new ResultSet<ContentCommentInfo>()
                {
                    IsSucceed = false,
                    Message = "ContentComment Not Found",
                    Data = null
                };

            return new ResultSet<ContentCommentInfo>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = contentComment
            };
        }
    }
}
