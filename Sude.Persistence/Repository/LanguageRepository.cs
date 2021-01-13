using Sude.Domain.Interfaces;
using Sude.Domain.Models.Localization;
using Sude.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sude.Persistence.Repository
{
    public class LanguageRepository : ILanguageRepository
    {

        private GenericRepository<LanguageInfo> _LanguageRepository;

        public LanguageRepository(SudeDBContext ctx)
        {
            //this._ctx = ctx;
            this._LanguageRepository = new GenericRepository<LanguageInfo>(ctx);
        }

   
       

       
        public IEnumerable<LanguageInfo> GetLanguages()
        {
            //return _ctx.Languages;
            return _LanguageRepository.Get();
        }

        public async Task<IEnumerable<LanguageInfo>> GetLanguagesAsync()
        {
            //return _ctx.Languages;
            return await _LanguageRepository.GetAsync();
        }
        public bool AddLanguage(LanguageInfo Language)
        {
            try
            {
                _LanguageRepository.Insert(Language);
            }
            catch
            {
                return false;
            }
            return true;
        }

       


        
       
      
        public LanguageInfo GetLanguageById(Guid LanguageId)
        {
            //return _ctx.Languages.Find(LanguageId);
            return _LanguageRepository.GetById(LanguageId);
        }
        public async  Task<LanguageInfo> GetLanguageByIdAsync(Guid LanguageId)
        {
            //return _ctx.Languages.Find(LanguageId);
            return await _LanguageRepository.GetByIdAsync(LanguageId);
        }
        public bool EditLanguage(LanguageInfo Language)
        {
            //_ctx.Entry(Language).State = EntityState.Modified;
            ////_ctx.Languages.Update(Language);
            _LanguageRepository.Update(Language);
            return true;
        }
        public bool DeleteLanguage(Guid languageId)
        {
            var language = GetLanguageById(languageId);
            if (language == null)
                return false;
            try
            {


                _LanguageRepository.Delete(language);
            }
            catch
            {
                return false;
            }
            return true;
        }
        public void Save()
        {
            //_ctx.SaveChanges();
            _LanguageRepository.Save();
        }

        public async Task SaveAsync() =>
           await _LanguageRepository.SaveAsync();
    }
}
