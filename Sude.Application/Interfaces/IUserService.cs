using Sude.Application.Result;
using Sude.Domain.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sude.Application.Interfaces
{
    public interface IUserService
    {
        bool IsValidtUser(string userName, string password);
        ResultSet<IEnumerable<User>> GetUsers();
        ResultSet<User> SaveUser(User request);
        ResultSet UserSatusChange(long userId);
        ResultSet UpdateUser(long userId,User request);
        ResultSet<User> GetUserById(long userId);

    }
}
