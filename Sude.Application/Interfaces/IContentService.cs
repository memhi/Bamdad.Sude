using Sude.Application.Result;
using Sude.Domain.Models.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sude.Application.Interfaces
{
    public interface IContentService
    {
        ResultSet<IEnumerable<ContentInfo>> GetContents();
        ResultSet<ContentInfo> AddContent(ContentInfo content);
        ResultSet EditContent(ContentInfo content);
        ResultSet DeleteContent(Guid contentId);
        ResultSet<ContentInfo> GetContentById(Guid contentId);
        Task<ResultSet<IEnumerable<ContentInfo>>> GetContentsAsync();
        Task<ResultSet<ContentInfo>> AddContentAsync(ContentInfo content);
        Task<ResultSet> EditContentAsync(ContentInfo content);
        Task<ResultSet> DeleteContentAsync(Guid contentId);
        Task<ResultSet<ContentInfo>> GetContentByIdAsync(Guid contentId);
    }
}
