using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sude.Domain.Interfaces;
 
using Sude.Domain.Models.Work;
 
using Sude.Persistence.Contexts;

namespace Sude.Persistence.Repository
{
    public class WorkUserRepository : IWorkUserRepository
    {

        private GenericRepository<WorkUserInfo> _WorkUserRepository;

        public WorkUserRepository(SudeDBContext ctx)
        {
            this._WorkUserRepository = new GenericRepository<WorkUserInfo>(ctx);
        }

        public async Task<IEnumerable<WorkUserInfo>> GetWorkUsersAsync()
        {
            return await _WorkUserRepository.GetAsync();
        }

        public async Task<IEnumerable<WorkUserInfo>> GetWorkUsersAsync(Guid? WorkId,Guid UserId)
        {

            return await _WorkUserRepository.GetAsync(wu=>(wu.WorkId==WorkId || WorkId==null) && (wu.UserId == UserId));
        }

        public bool AddWorkUser(WorkUserInfo WorkUser)
        {
            try
            {
                _WorkUserRepository.Insert(WorkUser);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool EditWorkUser(WorkUserInfo WorkUser)
        {
            try
            {
                _WorkUserRepository.Update(WorkUser);
            }
            catch
            { 
                return false; 
            }
            return true;
        }

        public async Task<WorkUserInfo> GetWorkUserByIdAsync(Guid WorkUserId)
        {
            return await _WorkUserRepository.GetByIdAsync(WorkUserId);
        }

        public void Save()
        {
            _WorkUserRepository.Save();
        }

        public async Task SaveAsync() =>
            await _WorkUserRepository.SaveAsync();

        public IEnumerable<WorkUserInfo> GetWorkUsers()
        {
            return _WorkUserRepository.Get();
        }


       

     


        public bool DeleteWorkUser(Guid WorkUserId)
        {

            var WorkUser = GetWorkUserById(WorkUserId);
            if (WorkUser == null )
                return false;
            try
            {
                _WorkUserRepository.Delete(WorkUser);
            }
            catch
            {
                return false;
            }
            return true;

           
        }

        public WorkUserInfo GetWorkUserById(Guid WorkUserId)
        {
            return _WorkUserRepository.GetById(WorkUserId);
        }

       
    }
}
