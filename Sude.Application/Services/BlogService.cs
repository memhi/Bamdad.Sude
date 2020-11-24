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
    public class BlogService : IBlogService
    {
        private IBlogRepository _BlogRepository;

        public BlogService(IBlogRepository BlogRepository)
        {
            this._BlogRepository = BlogRepository;
        }
      public ResultSet<IEnumerable<BlogInfo>> GetHomePageBlogs()
        {
            return new ResultSet<IEnumerable<BlogInfo>>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = _BlogRepository.GetHomePageBlogs()
            };
    

        }
        public ResultSet<IEnumerable<BlogInfo>> GetBlogs()
        {
            return new ResultSet<IEnumerable<BlogInfo>>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = _BlogRepository.GetBlogs()
            };
        }

        public ResultSet<BlogInfo> GetBlogById(Guid BlogId)
        {
            BlogInfo Blog = _BlogRepository.GetBlogById(BlogId);

            if (Blog == null)
                return new ResultSet<BlogInfo>()
                {
                    IsSucceed = false,
                    Message = "Blog Not Found",
                    Data = null
                };

            return new ResultSet<BlogInfo>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = Blog
            };
        }

        public ResultSet<BlogInfo> AddBlog(BlogInfo  Blog)
        {


          
 
            _BlogRepository.AddBlog(Blog);
            _BlogRepository.Save();
           
            return new ResultSet<BlogInfo>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = Blog
            };
        }

        public ResultSet EditBlog(BlogInfo Blog)
        {
           

            if (!_BlogRepository.EditBlog(Blog))
                return new ResultSet() { IsSucceed = false, Message = "Blog Not Edited" };

            try
            {
                _BlogRepository.Save();
            }
            catch
            {
                return new ResultSet() { IsSucceed = false, Message = "Blog Not Edited" };
            }
            return new ResultSet() { IsSucceed = true, Message = string.Empty };

        }

        public ResultSet DeleteBlog(Guid BlogId)
        {

            if (!_BlogRepository.DeleteBlog(BlogId))
                return new ResultSet() { IsSucceed = false, Message = "Blog Not Deleted" };

            try
            {
                _BlogRepository.Save();
            }
            catch
            {
                return new ResultSet() { IsSucceed = false, Message = "Blog Not Deleted" };
            }
            return new ResultSet() { IsSucceed = true, Message = string.Empty };
        }

        public async Task<ResultSet<IEnumerable<BlogInfo>>> GetBlogsAsync()
        {
            return new ResultSet<IEnumerable<BlogInfo>>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = await _BlogRepository.GetBlogsAsync()
            };
        }

        public async Task<ResultSet<BlogInfo>> AddBlogAsync(BlogInfo Blog)
        {
            

            _BlogRepository.AddBlog(Blog);

            try{await _BlogRepository.SaveAsync();}

            catch(Exception e){return new ResultSet<BlogInfo>() { IsSucceed = false, Message = e.Message };}

            return new ResultSet<BlogInfo>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = Blog
            };
        }

        public async Task<ResultSet> EditBlogAsync(BlogInfo Blog)
        {
            
            if (!_BlogRepository.EditBlog(Blog))
                return new ResultSet() { IsSucceed = false, Message = "Blog Not Edited" };

            try
            {
                await _BlogRepository.SaveAsync();
            }
            catch(Exception e)
            {
                return new ResultSet() { IsSucceed = false, Message = e.Message };
            }
            return new ResultSet() { IsSucceed = true, Message = string.Empty };
        }

        public async Task<ResultSet> DeleteBlogAsync(Guid BlogId)
        {


            if (!_BlogRepository.DeleteBlog(BlogId))
                return new ResultSet() { IsSucceed = false, Message = "Blog Not Deleted" };

            try
            {
                await _BlogRepository.SaveAsync();
            }
            catch
            {
                return new ResultSet() { IsSucceed = false, Message = "Blog Not Deleted" };
            }
            return new ResultSet() { IsSucceed = true, Message = string.Empty };
        }

        public async Task<ResultSet<BlogInfo>> GetBlogByIdAsync(Guid BlogId)
        {
            BlogInfo Blog = await _BlogRepository.GetBlogByIdAsync(BlogId);

            if (Blog == null)
                return new ResultSet<BlogInfo>()
                {
                    IsSucceed = false,
                    Message = "Blog Not Found",
                    Data = null
                };

            return new ResultSet<BlogInfo>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = Blog
            };
        }
    }
}
