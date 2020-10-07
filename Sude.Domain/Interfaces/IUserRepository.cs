using Sude.Domain.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sude.Domain.Interfaces
{
    public interface IUserRepository
    {
        bool IsValidUser(string userName, string password);
        IEnumerable<User> GetUsers();

        bool IsExistUserName(string userName);

        bool IsExistEmail(string email);

        void AddUser(User user);

        User GetUserById(long userId);

        bool EditUser(User user);

        void Save();
    }
}
