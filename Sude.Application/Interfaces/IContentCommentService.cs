using Sude.Application.Result;
using Sude.Domain.Models.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sude.Application.Interfaces
{
    public interface IContentCommentService
    {
        ResultSet<IEnumerable<ContentCommentInfo>> GetContentComments();
        ResultSet<ContentCommentInfo> AddContentComment(ContentCommentInfo contentComment);
        ResultSet EditContentComment(ContentCommentInfo contentComment);
        ResultSet DeleteContentComment(Guid contentCommentId);
        ResultSet<ContentCommentInfo> GetContentCommentById(Guid contentCommentId);
        Task<ResultSet<IEnumerable<ContentCommentInfo>>> GetContentCommentsAsync();
        Task<ResultSet<ContentCommentInfo>> AddContentCommentAsync(ContentCommentInfo contentComment);
        Task<ResultSet> EditContentCommentAsync(ContentCommentInfo contentComment);
        Task<ResultSet> DeleteContentCommentAsync(Guid contentCommentId);
        Task<ResultSet<ContentCommentInfo>> GetContentCommentByIdAsync(Guid contentCommentId);
    }
}
