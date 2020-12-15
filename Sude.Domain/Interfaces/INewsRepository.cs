using Sude.Domain.Models.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sude.Domain.Interfaces
{
    public interface INewsRepository
    {
        Task<IEnumerable<NewsInfo>> GetNewsAsync();
        IEnumerable<NewsInfo> GetNews();
        IEnumerable<NewsInfo> GetHomePageNews();

        bool AddNews(NewsInfo News);
        bool EditNews(NewsInfo News);
        bool DeleteNews(Guid NewsId);

        Task<NewsInfo> GetNewsByIdAsync(Guid NewsId);
        Task<NewsInfo> GetNewsByUrlAsync(string UrlAddress);
        NewsInfo GetNewsById(Guid NewsId);
       

        void Save();
        Task SaveAsync();
    }
}
