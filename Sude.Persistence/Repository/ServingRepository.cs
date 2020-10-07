using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sude.Domain.Interfaces;
using Sude.Domain.Models.Serving;
using Sude.Persistence.Contexts;

namespace Sude.Persistence.Repository
{
    public class ServingRepository : IServingRepository
    {

        private GenericRepository<Serving> _servingRepository;

        public ServingRepository(SudeDBContext ctx)
        {
            this._servingRepository = new GenericRepository<Serving>(ctx);
        }

        public async Task<IEnumerable<Serving>> GetServingsAsync()
        {
            return await _servingRepository.GetAsync();
        }
        public bool AddServing(Serving serving)
        {
            try
            {
                _servingRepository.Insert(serving);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool EditServing(Serving serving)
        {
            try
            {
                _servingRepository.Update(serving);
            }
            catch
            { 
                return false; 
            }
            return true;
        }

        public async Task<Serving> GetServingByIdAsync(Guid servingId)
        {
            return await _servingRepository.GetByIdAsync(servingId);
        }

        public void Save()
        {
            _servingRepository.Save();
        }

        public async Task SaveAsync() =>
            await _servingRepository.SaveAsync();

        public IEnumerable<Serving> GetServings()
        {
            return _servingRepository.Get();
        }

        public bool IsExistServing(string title)
        {
            return (_servingRepository.Get(p => p.Title == title).Count() > 0 ? true : false);
        }

        

        public bool DeleteServing(Guid servingId)
        {
            var serving = GetServingById(servingId);
            if (serving == null)
                return false;
            try
            {
                //_servingRepository.Delete(serving);
                serving.IsRemoved = true;
                serving.RemoveDate = DateTime.Now;
                _servingRepository.Update(serving);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public Serving GetServingById(Guid servingId)
        {
            return _servingRepository.GetById(servingId);
        }
    }
}
