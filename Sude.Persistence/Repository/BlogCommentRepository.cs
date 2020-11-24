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
    public class BlogCommentRepository : IBlogCommentRepository
    {

        private GenericRepository<BlogCommentInfo> _BlogCommentRepository;

        public BlogCommentRepository(SudeDBContext ctx)
        {
            this._BlogCommentRepository = new GenericRepository<BlogCommentInfo>(ctx);
        }

        public async Task<IEnumerable<BlogCommentInfo>> GetBlogCommentsAsync()
        {
           
            return await _BlogCommentRepository.GetAsync();
        }
        public bool AddBlogComment(BlogCommentInfo BlogComment)
        {
            try
            {
                _BlogCommentRepository.Insert(BlogComment);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool EditBlogComment(BlogCommentInfo BlogComment)
        {
            try
            {
                _BlogCommentRepository.Update(BlogComment);
            }
            catch
            { 
                return false; 
            }
            return true;
        }

        public async Task<BlogCommentInfo> GetBlogCommentByIdAsync(Guid BlogCommentId)
        {
            return await _BlogCommentRepository.GetByIdAsync(BlogCommentId);
        }

        public void Save()
        {
            _BlogCommentRepository.Save();
        }

        public async Task SaveAsync() =>
            await _BlogCommentRepository.SaveAsync();

        public IEnumerable<BlogCommentInfo> GetBlogComments()
        {
            return _BlogCommentRepository.Get();
        }

      

        

        public bool DeleteBlogComment(Guid BlogCommentId)
        {
            var BlogComment = GetBlogCommentById(BlogCommentId);
            if (BlogComment == null)
                return false;
            try
            {

               BlogComment.IsRemoved = true;
                BlogComment.RemoveDate = DateTime.Now;
                _BlogCommentRepository.Update(BlogComment);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public BlogCommentInfo GetBlogCommentById(Guid BlogCommentId)
        {
            return _BlogCommentRepository.GetById(BlogCommentId);
        }
    }
}
