using Sude.Application.Result;
using Sude.Domain.Models.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sude.Application.Interfaces
{
    public interface INewsService
    {
        ResultSet<IEnumerable<NewsInfo>> GetNews();
        ResultSet<NewsInfo> AddNews(NewsInfo News);
        ResultSet EditNews(NewsInfo News);
        ResultSet DeleteNews(Guid NewsId);
        ResultSet<NewsInfo> GetNewsById(Guid NewsId);
        Task<ResultSet<IEnumerable<NewsInfo>>> GetNewsAsync();
        Task<ResultSet<NewsInfo>> AddNewsAsync(NewsInfo News);
        Task<ResultSet> EditNewsAsync(NewsInfo News);
        Task<ResultSet> DeleteNewsAsync(Guid NewsId);
        Task<ResultSet<NewsInfo>> GetNewsByIdAsync(Guid NewsId);
        ResultSet<IEnumerable<NewsInfo>> GetHomePageNews();
    }
}
