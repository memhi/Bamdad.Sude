using Sude.Domain.Models.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sude.Domain.Interfaces
{
    public interface IBlogRepository
    {
        Task<IEnumerable<BlogInfo>> GetBlogsAsync();
        IEnumerable<BlogInfo> GetBlogs();
        IEnumerable<BlogInfo> GetHomePageBlogs();

        bool AddBlog(BlogInfo Blog);
        bool EditBlog(BlogInfo Blog);
        bool DeleteBlog(Guid BlogId);

        Task<BlogInfo> GetBlogByIdAsync(Guid BlogId);
        BlogInfo GetBlogById(Guid BlogId);
       

        void Save();
        Task SaveAsync();
    }
}
