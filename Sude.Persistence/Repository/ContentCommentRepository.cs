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
    public class ContentCommentRepository : IContentCommentRepository
    {

        private GenericRepository<ContentCommentInfo> _ContentCommentRepository;

        public ContentCommentRepository(SudeDBContext ctx)
        {
            this._ContentCommentRepository = new GenericRepository<ContentCommentInfo>(ctx);
        }

        public async Task<IEnumerable<ContentCommentInfo>> GetContentCommentsAsync()
        {
           
            return await _ContentCommentRepository.GetAsync();
        }
        public bool AddContentComment(ContentCommentInfo contentComment)
        {
            try
            {
                _ContentCommentRepository.Insert(contentComment);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool EditContentComment(ContentCommentInfo contentComment)
        {
            try
            {
                _ContentCommentRepository.Update(contentComment);
            }
            catch
            { 
                return false; 
            }
            return true;
        }

        public async Task<ContentCommentInfo> GetContentCommentByIdAsync(Guid contentCommentId)
        {
            return await _ContentCommentRepository.GetByIdAsync(contentCommentId);
        }

        public void Save()
        {
            _ContentCommentRepository.Save();
        }

        public async Task SaveAsync() =>
            await _ContentCommentRepository.SaveAsync();

        public IEnumerable<ContentCommentInfo> GetContentComments()
        {
            return _ContentCommentRepository.Get();
        }

      

        

        public bool DeleteContentComment(Guid contentCommentId)
        {
            var contentComment = GetContentCommentById(contentCommentId);
            if (contentComment == null)
                return false;
            try
            {

               contentComment.IsRemoved = true;
                contentComment.RemoveDate = DateTime.Now;
                _ContentCommentRepository.Update(contentComment);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public ContentCommentInfo GetContentCommentById(Guid contentCommentId)
        {
            return _ContentCommentRepository.GetById(contentCommentId);
        }
    }
}
