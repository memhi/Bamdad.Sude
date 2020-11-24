using Sude.Domain.Models.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sude.Domain.Interfaces
{
    public interface IBlogCommentRepository
    {
        Task<IEnumerable<BlogCommentInfo>> GetBlogCommentsAsync();
        IEnumerable<BlogCommentInfo> GetBlogComments();
 

        bool AddBlogComment(BlogCommentInfo BlogComment);
        bool EditBlogComment(BlogCommentInfo BlogComment);
        bool DeleteBlogComment(Guid BlogCommentId);

        Task<BlogCommentInfo> GetBlogCommentByIdAsync(Guid BlogCommentId);
        BlogCommentInfo GetBlogCommentById(Guid BlogCommentId);
       

        void Save();
        Task SaveAsync();
    }
}
