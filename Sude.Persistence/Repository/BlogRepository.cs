using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sude.Domain.Interfaces;
using Sude.Domain.Models.Content;
 
using Sude.Persistence.Contexts;

namespace Sude.Persistence.Repository
{
    public class BlogRepository : IBlogRepository
    {

        private GenericRepository<BlogInfo> _BlogRepository;

        public BlogRepository(SudeDBContext ctx)
        {
            this._BlogRepository = new GenericRepository<BlogInfo>(ctx);
        }

        public IEnumerable<BlogInfo> GetHomePageBlogs()
        {

            return   _BlogRepository.GetAsync(null, b => b.OrderByDescending(bl => bl.RegDate), "").Result.ToTake(6);
        }
        public async Task<IEnumerable<BlogInfo>> GetBlogsAsync()
        {
           
            return await _BlogRepository.GetAsync();
        }
        public bool AddBlog(BlogInfo Blog)
        {
            try
            {
                _BlogRepository.Insert(Blog);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool EditBlog(BlogInfo Blog)
        {
            try
            {
                _BlogRepository.Update(Blog);
            }
            catch
            { 
                return false; 
            }
            return true;
        }

        public async Task<BlogInfo> GetBlogByUrlAsync(string UrlAddress)
        {
            return await _BlogRepository.GetByIdAsync(b=>b.UrlAddress==UrlAddress);
        }
        public async Task<BlogInfo> GetBlogByIdAsync(Guid BlogId)
        {
            return await _BlogRepository.GetByIdAsync(BlogId);
        }

        public void Save()
        {
            _BlogRepository.Save();
        }

        public async Task SaveAsync() =>
            await _BlogRepository.SaveAsync();

        public IEnumerable<BlogInfo> GetBlogs()
        {
            return _BlogRepository.Get();
        }

      

        

        public bool DeleteBlog(Guid BlogId)
        {
            var Blog = GetBlogById(BlogId);
            if (Blog == null)
                return false;
            try
            {

                Blog.IsRemoved = true;
                Blog.RemoveDate = DateTime.Now;
                _BlogRepository.Update(Blog);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public BlogInfo GetBlogById(Guid BlogId)
        {
            return _BlogRepository.GetById(BlogId);
        }
    }
}
