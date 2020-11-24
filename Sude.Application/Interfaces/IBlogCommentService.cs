using Sude.Application.Result;
using Sude.Domain.Models.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sude.Application.Interfaces
{
    public interface IBlogCommentService
    {
        ResultSet<IEnumerable<BlogCommentInfo>> GetBlogComments();
        ResultSet<BlogCommentInfo> AddBlogComment(BlogCommentInfo BlogComment);
        ResultSet EditBlogComment(BlogCommentInfo BlogComment);
        ResultSet DeleteBlogComment(Guid BlogCommentId);
        ResultSet<BlogCommentInfo> GetBlogCommentById(Guid BlogCommentId);
        Task<ResultSet<IEnumerable<BlogCommentInfo>>> GetBlogCommentsAsync();
        Task<ResultSet<BlogCommentInfo>> AddBlogCommentAsync(BlogCommentInfo BlogComment);
        Task<ResultSet> EditBlogCommentAsync(BlogCommentInfo BlogComment);
        Task<ResultSet> DeleteBlogCommentAsync(Guid BlogCommentId);
        Task<ResultSet<BlogCommentInfo>> GetBlogCommentByIdAsync(Guid BlogCommentId);
    }
}
