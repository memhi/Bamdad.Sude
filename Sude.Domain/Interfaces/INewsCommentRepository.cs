using Sude.Domain.Models.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sude.Domain.Interfaces
{
    public interface INewsCommentRepository
    {
        Task<IEnumerable<NewsCommentInfo>> GetNewsCommentsAsync();
        IEnumerable<NewsCommentInfo> GetNewsComments();
 

        bool AddNewsComment(NewsCommentInfo NewsComment);
        bool EditNewsComment(NewsCommentInfo NewsComment);
        bool DeleteNewsComment(Guid NewsCommentId);

        Task<NewsCommentInfo> GetNewsCommentByIdAsync(Guid NewsCommentId);
        NewsCommentInfo GetNewsCommentById(Guid NewsCommentId);
       

        void Save();
        Task SaveAsync();
    }
}
