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
    public class NewsCommentRepository : INewsCommentRepository
    {

        private GenericRepository<NewsCommentInfo> _NewsCommentRepository;

        public NewsCommentRepository(SudeDBContext ctx)
        {
            this._NewsCommentRepository = new GenericRepository<NewsCommentInfo>(ctx);
        }

        public async Task<IEnumerable<NewsCommentInfo>> GetNewsCommentsAsync()
        {
           
            return await _NewsCommentRepository.GetAsync();
        }
        public bool AddNewsComment(NewsCommentInfo NewsComment)
        {
            try
            {
                _NewsCommentRepository.Insert(NewsComment);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool EditNewsComment(NewsCommentInfo NewsComment)
        {
            try
            {
                _NewsCommentRepository.Update(NewsComment);
            }
            catch
            { 
                return false; 
            }
            return true;
        }

        public async Task<NewsCommentInfo> GetNewsCommentByIdAsync(Guid NewsCommentId)
        {
            return await _NewsCommentRepository.GetByIdAsync(NewsCommentId);
        }

        public void Save()
        {
            _NewsCommentRepository.Save();
        }

        public async Task SaveAsync() =>
            await _NewsCommentRepository.SaveAsync();

        public IEnumerable<NewsCommentInfo> GetNewsComments()
        {
            return _NewsCommentRepository.Get();
        }

      

        

        public bool DeleteNewsComment(Guid NewsCommentId)
        {
            var NewsComment = GetNewsCommentById(NewsCommentId);
            if (NewsComment == null)
                return false;
            try
            {

               NewsComment.IsRemoved = true;
                NewsComment.RemoveDate = DateTime.Now;
                _NewsCommentRepository.Update(NewsComment);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public NewsCommentInfo GetNewsCommentById(Guid NewsCommentId)
        {
            return _NewsCommentRepository.GetById(NewsCommentId);
        }
    }
}
