using Sude.Domain.Interfaces;
using Sude.Domain.Models.Account;
using Sude.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sude.Persistence.Repository
{
    public class UserRepository : IUserRepository
    {

        private GenericRepository<User> _userRepository;

        public UserRepository(SudeDBContext ctx)
        {
            //this._ctx = ctx;
            this._userRepository = new GenericRepository<User>(ctx);
        }

        public bool IsValidUser(string userName, string password)
        {
            return _userRepository.Get(u => u.UserName == userName && u.Password == password && u.IsActive).Count() > 0;
        }

       

        public bool IsExistEmail(string email)
        {
            //return _ctx.Users.Any(e => e.Email == email);
            return (_userRepository.Get(p=>p.Email== email).Count()>0?true:false);
        }

        public bool IsExistUserName(string userName)
        {
            //return _ctx.Users.Any(u => u.UserName == userName);
            return (_userRepository.Get(p => p.UserName == userName).Count() > 0 ? true : false);
        }
        public IEnumerable<User> GetUsers()
        {
            //return _ctx.Users;
            return _userRepository.Get();
        }
        public void AddUser(User user)
        {
            //_ctx.Users.Add(user);
            _userRepository.Insert(user);
        }

       
        public User GetUserById(long userId)
        {
            //return _ctx.Users.Find(userId);
            return _userRepository.GetById(userId);
        }
        public bool EditUser(User user)
        {
            //_ctx.Entry(user).State = EntityState.Modified;
            ////_ctx.Users.Update(user);
            _userRepository.Update(user);
            return true;
        }
        public void Save()
        {
            //_ctx.SaveChanges();
            _userRepository.Save();
        }

       
    }
}
