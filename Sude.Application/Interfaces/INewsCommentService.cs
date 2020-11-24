using Sude.Application.Result;
using Sude.Domain.Models.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sude.Application.Interfaces
{
    public interface INewsCommentService
    {
        ResultSet<IEnumerable<NewsCommentInfo>> GetNewsComments();
        ResultSet<NewsCommentInfo> AddNewsComment(NewsCommentInfo NewsComment);
        ResultSet EditNewsComment(NewsCommentInfo NewsComment);
        ResultSet DeleteNewsComment(Guid NewsCommentId);
        ResultSet<NewsCommentInfo> GetNewsCommentById(Guid NewsCommentId);
        Task<ResultSet<IEnumerable<NewsCommentInfo>>> GetNewsCommentsAsync();
        Task<ResultSet<NewsCommentInfo>> AddNewsCommentAsync(NewsCommentInfo NewsComment);
        Task<ResultSet> EditNewsCommentAsync(NewsCommentInfo NewsComment);
        Task<ResultSet> DeleteNewsCommentAsync(Guid NewsCommentId);
        Task<ResultSet<NewsCommentInfo>> GetNewsCommentByIdAsync(Guid NewsCommentId);
    }
}
