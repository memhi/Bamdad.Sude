using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sude.Application.Interfaces;
using Sude.Application.Result;
using Sude.Domain.Interfaces;
using Sude.Domain.Models.Content;

namespace Sude.Application.Services
{
    public class NewsService : INewsService
    {
        private INewsRepository _NewsRepository;

        public NewsService(INewsRepository NewsRepository)
        {
            this._NewsRepository = NewsRepository;
        }
        public ResultSet<IEnumerable<NewsInfo>> GetNews()
        {
            return new ResultSet<IEnumerable<NewsInfo>>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = _NewsRepository.GetNews()
            };
        }

        public ResultSet<NewsInfo> GetNewsById(Guid NewsId)
        {
            NewsInfo News = _NewsRepository.GetNewsById(NewsId);

            if (News == null)
                return new ResultSet<NewsInfo>()
                {
                    IsSucceed = false,
                    Message = "News Not Found",
                    Data = null
                };

            return new ResultSet<NewsInfo>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = News
            };
        }

        public ResultSet<NewsInfo> AddNews(NewsInfo  News)
        {


          
 
            _NewsRepository.AddNews(News);
            _NewsRepository.Save();
           
            return new ResultSet<NewsInfo>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = News
            };
        }

        public ResultSet EditNews(NewsInfo News)
        {
           

            if (!_NewsRepository.EditNews(News))
                return new ResultSet() { IsSucceed = false, Message = "News Not Edited" };

            try
            {
                _NewsRepository.Save();
            }
            catch
            {
                return new ResultSet() { IsSucceed = false, Message = "News Not Edited" };
            }
            return new ResultSet() { IsSucceed = true, Message = string.Empty };

        }

        public ResultSet DeleteNews(Guid NewsId)
        {

            if (!_NewsRepository.DeleteNews(NewsId))
                return new ResultSet() { IsSucceed = false, Message = "News Not Deleted" };

            try
            {
                _NewsRepository.Save();
            }
            catch
            {
                return new ResultSet() { IsSucceed = false, Message = "News Not Deleted" };
            }
            return new ResultSet() { IsSucceed = true, Message = string.Empty };
        }

        public async Task<ResultSet<IEnumerable<NewsInfo>>> GetNewsAsync()
        {
            return new ResultSet<IEnumerable<NewsInfo>>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = await _NewsRepository.GetNewsAsync()
            };
        }

        public ResultSet<IEnumerable<NewsInfo>> GetHomePageNews()
        {
            return new ResultSet<IEnumerable<NewsInfo>>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = _NewsRepository.GetHomePageNews()
            };
        }

        public async Task<ResultSet<NewsInfo>> AddNewsAsync(NewsInfo News)
        {
            

            _NewsRepository.AddNews(News);

            try{await _NewsRepository.SaveAsync();}

            catch(Exception e){return new ResultSet<NewsInfo>() { IsSucceed = false, Message = e.Message };}

            return new ResultSet<NewsInfo>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = News
            };
        }

        public async Task<ResultSet> EditNewsAsync(NewsInfo News)
        {
            
            if (!_NewsRepository.EditNews(News))
                return new ResultSet() { IsSucceed = false, Message = "News Not Edited" };

            try
            {
                await _NewsRepository.SaveAsync();
            }
            catch(Exception e)
            {
                return new ResultSet() { IsSucceed = false, Message = e.Message };
            }
            return new ResultSet() { IsSucceed = true, Message = string.Empty };
        }

        public async Task<ResultSet> DeleteNewsAsync(Guid NewsId)
        {


            if (!_NewsRepository.DeleteNews(NewsId))
                return new ResultSet() { IsSucceed = false, Message = "News Not Deleted" };

            try
            {
                await _NewsRepository.SaveAsync();
            }
            catch
            {
                return new ResultSet() { IsSucceed = false, Message = "News Not Deleted" };
            }
            return new ResultSet() { IsSucceed = true, Message = string.Empty };
        }

        public async Task<ResultSet<NewsInfo>> GetNewsByUrlAsync(string UrlAddress)
        {
            NewsInfo News = await _NewsRepository.GetNewsByUrlAsync(UrlAddress);

            if (News == null)
                return new ResultSet<NewsInfo>()
                {
                    IsSucceed = false,
                    Message = "Blog Not Found",
                    Data = null
                };

            return new ResultSet<NewsInfo>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = News
            };
        }
        public async Task<ResultSet<NewsInfo>> GetNewsByIdAsync(Guid NewsId)
        {
            NewsInfo News = await _NewsRepository.GetNewsByIdAsync(NewsId);

            if (News == null)
                return new ResultSet<NewsInfo>()
                {
                    IsSucceed = false,
                    Message = "News Not Found",
                    Data = null
                };

            return new ResultSet<NewsInfo>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = News
            };
        }
    }
}
