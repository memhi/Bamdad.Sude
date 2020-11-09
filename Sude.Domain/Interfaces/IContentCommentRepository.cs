using Sude.Domain.Models.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sude.Domain.Interfaces
{
    public interface IContentCommentRepository
    {
        Task<IEnumerable<ContentCommentInfo>> GetContentCommentsAsync();
        IEnumerable<ContentCommentInfo> GetContentComments();
 

        bool AddContentComment(ContentCommentInfo contentComment);
        bool EditContentComment(ContentCommentInfo contentComment);
        bool DeleteContentComment(Guid contentCommentId);

        Task<ContentCommentInfo> GetContentCommentByIdAsync(Guid contentCommentId);
        ContentCommentInfo GetContentCommentById(Guid contentCommentId);
       

        void Save();
        Task SaveAsync();
    }
}
