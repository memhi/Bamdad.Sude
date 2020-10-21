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
        IEnumerable<UserInfo> GetUsers();

        bool IsExistUserName(string userName);

        bool IsExistEmail(string email);

        void AddUser(UserInfo user);

        UserInfo GetUserById(long userId);

        bool EditUser(UserInfo user);

        void Save();
    }
}
