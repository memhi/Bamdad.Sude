using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sude.Domain.Interfaces;
using Sude.Domain.Models.Content;
 
using Sude.Persistence.Contexts;

namespace Sude.Persistence.Repository
{
    public class ContentRepository : IContentRepository
    {

        private GenericRepository<ContentInfo> _ContentRepository;

        public ContentRepository(SudeDBContext ctx)
        {
            this._ContentRepository = new GenericRepository<ContentInfo>(ctx);
        }

        public async Task<IEnumerable<ContentInfo>> GetContentsAsync()
        {
           
            return await _ContentRepository.GetAsync();
        }
        public bool AddContent(ContentInfo content)
        {
            try
            {
                _ContentRepository.Insert(content);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool EditContent(ContentInfo content)
        {
            try
            {
                _ContentRepository.Update(content);
            }
            catch
            { 
                return false; 
            }
            return true;
        }

        public async Task<ContentInfo> GetContentByIdAsync(Guid contentId)
        {
            return await _ContentRepository.GetByIdAsync(contentId);
        }

        public void Save()
        {
            _ContentRepository.Save();
        }

        public async Task SaveAsync() =>
            await _ContentRepository.SaveAsync();

        public IEnumerable<ContentInfo> GetContents()
        {
            return _ContentRepository.Get();
        }

      

        

        public bool DeleteContent(Guid contentId)
        {
            var content = GetContentById(contentId);
            if (content == null)
                return false;
            try
            {

                content.IsRemoved = true;
                content.RemoveDate = DateTime.Now;
                _ContentRepository.Update(content);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public ContentInfo GetContentById(Guid contentId)
        {
            return _ContentRepository.GetById(contentId);
        }
    }
}
