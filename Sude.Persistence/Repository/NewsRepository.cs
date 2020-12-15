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
    public class NewsRepository : INewsRepository
    {

        private GenericRepository<NewsInfo> _NewsRepository;

        public NewsRepository(SudeDBContext ctx)
        {
            this._NewsRepository = new GenericRepository<NewsInfo>(ctx);
        }

        public async Task<IEnumerable<NewsInfo>> GetNewsAsync()
        {
           
            return await _NewsRepository.GetAsync();
        }
        public IEnumerable<NewsInfo> GetHomePageNews()
        {

            return _NewsRepository.GetAsync(null, b => b.OrderByDescending(bl => bl.RegDate), "").Result.ToTake(6);
        }
        public bool AddNews(NewsInfo News)
        {
            try
            {
                _NewsRepository.Insert(News);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool EditNews(NewsInfo News)
        {
            try
            {
                _NewsRepository.Update(News);
            }
            catch
            { 
                return false; 
            }
            return true;
        }
        public async Task<NewsInfo> GetNewsByUrlAsync(string UrlAddress)
        {
            return await _NewsRepository.GetByIdAsync(n => n.UrlAddress == UrlAddress);
        }
        public async Task<NewsInfo> GetNewsByIdAsync(Guid NewsId)
        {
            return await _NewsRepository.GetByIdAsync(NewsId);
        }

        public void Save()
        {
            _NewsRepository.Save();
        }

        public async Task SaveAsync() =>
            await _NewsRepository.SaveAsync();

        public IEnumerable<NewsInfo> GetNews()
        {
            return _NewsRepository.Get();
        }

      

        

        public bool DeleteNews(Guid NewsId)
        {
            var News = GetNewsById(NewsId);
            if (News == null)
                return false;
            try
            {

                News.IsRemoved = true;
                News.RemoveDate = DateTime.Now;
                _NewsRepository.Update(News);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public NewsInfo GetNewsById(Guid NewsId)
        {
            return _NewsRepository.GetById(NewsId);
        }
    }
}
