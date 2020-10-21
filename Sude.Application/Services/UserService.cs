using Sude.Application.Interfaces;
using Sude.Domain.Models.Account;
using Sude.Common;
using Sude.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sude.Application.Result;

namespace Sude.Application.Services
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        public bool IsValidtUser(string userName, string password)
        {
            return _userRepository.IsValidUser(userName, password);
        }

        public ResultSet<IEnumerable<UserInfo>> GetUsers()
        {
            var users = _userRepository.GetUsers().Select(u => new UserInfo()
            {
                Id = u.Id,
                UserName = u.UserName,
                Email = u.Email,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Phone = u.Phone,
                IsActive = u.IsActive,
                Password = string.Empty

            });
            return new ResultSet<IEnumerable<UserInfo>>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = users


            };
        }



        public ResultSet<UserInfo> SaveUser(UserInfo request)
        {
            //if (request.Users.Count() == 0)
            //    return new ResultDto<UsersDtoModel>()
            //    {
            //        IsSucceed = false,
            //        Messege = "",
            //        Data=null
            //    };

            UserInfo user = new UserInfo()
            {
                Email = request.Email,
                UserName = request.UserName,
                Password = SecurityHelper.Secure.HashPassword(request.Password)
            };



            //foreach (var item in userData.UserRoles)
            //{
            //    var role = _roleRepository.GetRoleById(item.Id);//.GetRoles().Where(x=>x.Id==item.Id).First();
            //    userRoles.Add(new UserRole
            //    {
            //        Role = role,
            //        RolId = role.Id,
            //        User = user,
            //        UserId = user.Id,
            //    });
            //}
            //user.UserRoles = userRoles;

            _userRepository.AddUser(user);

            _userRepository.Save();

            return new ResultSet<UserInfo>()
            {
                IsSucceed = true,
                Message = "",
                Data = request
            };
        }

        public ResultSet UserSatusChange(long userId)
        {
            UserInfo user = _userRepository.GetUserById(userId);
            user.IsActive = !user.IsActive;
            _userRepository.EditUser(user);
            _userRepository.Save();
            return new ResultSet()
            {
                IsSucceed = true,
                Message = "تغییر وضعیت با موفقیت انجام شد"
            };
        }

        public ResultSet UpdateUser(long UserId, UserInfo request)
        {
            UserInfo user = _userRepository.GetUserById(UserId);
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.Email = request.Email;
            user.Phone = request.Phone;

            if (_userRepository.EditUser(user))
                return new ResultSet() { IsSucceed = true, Message = string.Empty };

            return new ResultSet() { IsSucceed = true, Message = string.Empty };
        }

        public ResultSet<UserInfo> GetUserById(long userId)
        {
            UserInfo user = _userRepository.GetUserById(userId);
            
            if (user == null)
                return new ResultSet<UserInfo>()
                {
                    IsSucceed = false,
                    Message = "کاربر با این شناسه پیدا نشد",
                    Data = null
                };

            return new ResultSet<UserInfo>()
            {
                IsSucceed = true,
                Message = string.Empty,
                Data = new UserInfo()
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Phone = user.Phone,
                    IsActive = user.IsActive,
                    Password = string.Empty

                }
            };

        }
    }
}
