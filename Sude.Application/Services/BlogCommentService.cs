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
    public class BlogCommentService : IBlogCommentService
    {
        private IBlogCommentRepository _BlogCommentRepository;

        public BlogCommentService(IBlogCommentRepository BlogCommentRepository)
        {
            this._BlogCommentRepository = BlogCommentRepository;
        }
        public ResultSet<IEnumerable<BlogCommentInfo>> GetBlogComments()
        {
            return new ResultSet<IEnumerable<BlogCommentInfo>>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = _BlogCommentRepository.GetBlogComments()
            };
        }

        public ResultSet<BlogCommentInfo> GetBlogCommentById(Guid BlogCommentId)
        {
            BlogCommentInfo BlogComment = _BlogCommentRepository.GetBlogCommentById(BlogCommentId);

            if (BlogComment == null)
                return new ResultSet<BlogCommentInfo>()
                {
                    IsSucceed = false,
                    Message = "BlogComment Not Found",
                    Data = null
                };

            return new ResultSet<BlogCommentInfo>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = BlogComment
            };
        }

        public ResultSet<BlogCommentInfo> AddBlogComment(BlogCommentInfo  BlogComment)
        {


          
 
            _BlogCommentRepository.AddBlogComment(BlogComment);
            _BlogCommentRepository.Save();
           
            return new ResultSet<BlogCommentInfo>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = BlogComment
            };
        }

        public ResultSet EditBlogComment(BlogCommentInfo BlogComment)
        {
           

            if (!_BlogCommentRepository.EditBlogComment(BlogComment))
                return new ResultSet() { IsSucceed = false, Message = "BlogComment Not Edited" };

            try
            {
                _BlogCommentRepository.Save();
            }
            catch
            {
                return new ResultSet() { IsSucceed = false, Message = "BlogComment Not Edited" };
            }
            return new ResultSet() { IsSucceed = true, Message = string.Empty };

        }

        public ResultSet DeleteBlogComment(Guid BlogCommentId)
        {

            if (!_BlogCommentRepository.DeleteBlogComment(BlogCommentId))
                return new ResultSet() { IsSucceed = false, Message = "BlogComment Not Deleted" };

            try
            {
                _BlogCommentRepository.Save();
            }
            catch
            {
                return new ResultSet() { IsSucceed = false, Message = "BlogComment Not Deleted" };
            }
            return new ResultSet() { IsSucceed = true, Message = string.Empty };
        }

        public async Task<ResultSet<IEnumerable<BlogCommentInfo>>> GetBlogCommentsAsync()
        {
            return new ResultSet<IEnumerable<BlogCommentInfo>>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = await _BlogCommentRepository.GetBlogCommentsAsync()
            };
        }

        public async Task<ResultSet<BlogCommentInfo>> AddBlogCommentAsync(BlogCommentInfo BlogComment)
        {
            

            _BlogCommentRepository.AddBlogComment(BlogComment);

            try{await _BlogCommentRepository.SaveAsync();}

            catch(Exception e){return new ResultSet<BlogCommentInfo>() { IsSucceed = false, Message = e.Message };}

            return new ResultSet<BlogCommentInfo>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = BlogComment
            };
        }

        public async Task<ResultSet> EditBlogCommentAsync(BlogCommentInfo BlogComment)
        {
            
            if (!_BlogCommentRepository.EditBlogComment(BlogComment))
                return new ResultSet() { IsSucceed = false, Message = "BlogComment Not Edited" };

            try
            {
                await _BlogCommentRepository.SaveAsync();
            }
            catch(Exception e)
            {
                return new ResultSet() { IsSucceed = false, Message = e.Message };
            }
            return new ResultSet() { IsSucceed = true, Message = string.Empty };
        }

        public async Task<ResultSet> DeleteBlogCommentAsync(Guid BlogCommentId)
        {



























            if (!_BlogCommentRepository.DeleteBlogComment(BlogCommentId))
                return new ResultSet() { IsSucceed = false, Message = "BlogComment Not Deleted" };

            try
            {
                await _BlogCommentRepository.SaveAsync();
            }
            catch
            {
                return new ResultSet() { IsSucceed = false, Message = "BlogComment Not Deleted" };
            }
            return new ResultSet() { IsSucceed = true, Message = string.Empty };
        }

        public async Task<ResultSet<BlogCommentInfo>> GetBlogCommentByIdAsync(Guid BlogCommentId)
        {
            BlogCommentInfo BlogComment = await _BlogCommentRepository.GetBlogCommentByIdAsync(BlogCommentId);

            if (BlogComment == null)
                return new ResultSet<BlogCommentInfo>()
                {
                    IsSucceed = false,
                    Message = "BlogComment Not Found",
                    Data = null
                };

            return new ResultSet<BlogCommentInfo>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = BlogComment
            };
        }
    }
}
