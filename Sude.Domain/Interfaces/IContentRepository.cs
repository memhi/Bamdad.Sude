using Sude.Domain.Models.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sude.Domain.Interfaces
{
    public interface IContentRepository
    {
        Task<IEnumerable<ContentInfo>> GetContentsAsync();
        IEnumerable<ContentInfo> GetContents();
  

        bool AddContent(ContentInfo content);
        bool EditContent(ContentInfo content);
        bool DeleteContent(Guid contentId);

        Task<ContentInfo> GetContentByIdAsync(Guid contentId);
        ContentInfo GetContentById(Guid contentId);
       

        void Save();
        Task SaveAsync();
    }
}
