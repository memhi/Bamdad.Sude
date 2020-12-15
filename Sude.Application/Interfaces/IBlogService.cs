using Sude.Application.Result;
using Sude.Domain.Models.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sude.Application.Interfaces
{
    public interface IBlogService
    {
        ResultSet<IEnumerable<BlogInfo>> GetBlogs();
        ResultSet<BlogInfo> AddBlog(BlogInfo Blog);
        ResultSet EditBlog(BlogInfo Blog);
        ResultSet DeleteBlog(Guid BlogId);
        ResultSet<BlogInfo> GetBlogById(Guid BlogId);
        Task<ResultSet<IEnumerable<BlogInfo>>> GetBlogsAsync();
        Task<ResultSet<BlogInfo>> AddBlogAsync(BlogInfo Blog);
        Task<ResultSet> EditBlogAsync(BlogInfo Blog);
        ResultSet<IEnumerable<BlogInfo>> GetHomePageBlogs();
        Task<ResultSet> DeleteBlogAsync(Guid BlogId);
        Task<ResultSet<BlogInfo>> GetBlogByIdAsync(Guid BlogId);
        Task<ResultSet<BlogInfo>> GetBlogByUrlAsync(string UrlAddress);
    }
}
