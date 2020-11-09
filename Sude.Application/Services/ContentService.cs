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
    public class ContentService : IContentService
    {
        private IContentRepository _ContentRepository;

        public ContentService(IContentRepository contentRepository)
        {
            this._ContentRepository = contentRepository;
        }
        public ResultSet<IEnumerable<ContentInfo>> GetContents()
        {
            return new ResultSet<IEnumerable<ContentInfo>>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = _ContentRepository.GetContents()
            };
        }

        public ResultSet<ContentInfo> GetContentById(Guid contentId)
        {
            ContentInfo Content = _ContentRepository.GetContentById(contentId);

            if (Content == null)
                return new ResultSet<ContentInfo>()
                {
                    IsSucceed = false,
                    Message = "Content Not Found",
                    Data = null
                };

            return new ResultSet<ContentInfo>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = Content
            };
        }

        public ResultSet<ContentInfo> AddContent(ContentInfo  content)
        {


          
 
            _ContentRepository.AddContent(content);
            _ContentRepository.Save();
           
            return new ResultSet<ContentInfo>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = content
            };
        }

        public ResultSet EditContent(ContentInfo content)
        {
           

            if (!_ContentRepository.EditContent(content))
                return new ResultSet() { IsSucceed = false, Message = "Content Not Edited" };

            try
            {
                _ContentRepository.Save();
            }
            catch
            {
                return new ResultSet() { IsSucceed = false, Message = "Content Not Edited" };
            }
            return new ResultSet() { IsSucceed = true, Message = string.Empty };

        }

        public ResultSet DeleteContent(Guid contentId)
        {

            if (!_ContentRepository.DeleteContent(contentId))
                return new ResultSet() { IsSucceed = false, Message = "Content Not Deleted" };

            try
            {
                _ContentRepository.Save();
            }
            catch
            {
                return new ResultSet() { IsSucceed = false, Message = "Content Not Deleted" };
            }
            return new ResultSet() { IsSucceed = true, Message = string.Empty };
        }

        public async Task<ResultSet<IEnumerable<ContentInfo>>> GetContentsAsync()
        {
            return new ResultSet<IEnumerable<ContentInfo>>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = await _ContentRepository.GetContentsAsync()
            };
        }

        public async Task<ResultSet<ContentInfo>> AddContentAsync(ContentInfo content)
        {
            

            _ContentRepository.AddContent(content);

            try{await _ContentRepository.SaveAsync();}

            catch(Exception e){return new ResultSet<ContentInfo>() { IsSucceed = false, Message = e.Message };}

            return new ResultSet<ContentInfo>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = content
            };
        }

        public async Task<ResultSet> EditContentAsync(ContentInfo content)
        {
            
            if (!_ContentRepository.EditContent(content))
                return new ResultSet() { IsSucceed = false, Message = "Content Not Edited" };

            try
            {
                await _ContentRepository.SaveAsync();
            }
            catch(Exception e)
            {
                return new ResultSet() { IsSucceed = false, Message = e.Message };
            }
            return new ResultSet() { IsSucceed = true, Message = string.Empty };
        }

        public async Task<ResultSet> DeleteContentAsync(Guid contentId)
        {


            if (!_ContentRepository.DeleteContent(contentId))
                return new ResultSet() { IsSucceed = false, Message = "Content Not Deleted" };

            try
            {
                await _ContentRepository.SaveAsync();
            }
            catch
            {
                return new ResultSet() { IsSucceed = false, Message = "Content Not Deleted" };
            }
            return new ResultSet() { IsSucceed = true, Message = string.Empty };
        }

        public async Task<ResultSet<ContentInfo>> GetContentByIdAsync(Guid ContentId)
        {
            ContentInfo Content = await _ContentRepository.GetContentByIdAsync(ContentId);

            if (Content == null)
                return new ResultSet<ContentInfo>()
                {
                    IsSucceed = false,
                    Message = "Content Not Found",
                    Data = null
                };

            return new ResultSet<ContentInfo>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = Content
            };
        }
    }
}
