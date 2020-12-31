


using Microsoft.AspNetCore.Http;

using Sude.Mvc.UI.Admin.Models;
using IdentityModel.Client;
using System.Net.Http;
using System;
using Sude.Mvc.UI.ApiManagement;
using Sude.Dto.DtoModels.Work;
using Sude.Dto.DtoModels.Order;
using System.Collections.Generic;
using Sude.Mvc.UI.Menu;
using Microsoft.AspNetCore.Hosting;
using System.Linq;

namespace Sude.Mvc.UI.Admin
{

    public class SudeSessionContext
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IWebHostEnvironment _HostingEnvitonment;
        public SudeSessionContext(IHttpContextAccessor contextAccessor, IWebHostEnvironment hostingEnvitonment)
        {
            _contextAccessor = contextAccessor;
            _HostingEnvitonment = hostingEnvitonment;
        }

        public virtual void LogoutUser()
        {
            CurrentOrderDetails = null;
            CurrentWork = null;
            CurrentWorkId = null;
            CurrentWorkName = null;
            CurrentUser = null;
            IsAdmin = false;
   

        }


        protected virtual void RemoveUserCookie()
        {
            var cookieName = Constants.CookieNames.Sude_ManagementWork_Cookie;
         _contextAccessor.HttpContext?.Response?.Cookies.Delete(cookieName);
        }



        protected virtual string GetUserCookie()
        {
            var cookieName = Constants.CookieNames.Sude_ManagementWork_Cookie;
            return _contextAccessor.HttpContext?.Request?.Cookies[cookieName];
        }

        //protected virtual string GetUserNameCookie()
        //{
        //    var cookieName = Constants.CookieNames.Sude_ManagementWork_Cookie_UserName;
        //    return _contextAccessor.HttpContext?.Request?.Cookies[cookieName];
        //}

        //protected virtual void SetUserCookieUserName(string userName)
        //{
        //    if (_contextAccessor.HttpContext?.Response == null)
        //        return;

        //    //delete current cookie value
        //    var cookieName = Constants.CookieNames.Sude_ManagementWork_Cookie_UserName;
        //    _contextAccessor.HttpContext.Response.Cookies.Delete(cookieName);

        //    //get date of cookie expiration
        //    var cookieExpires = 720;
        //    var cookieExpiresDate = DateTime.Now.AddHours(cookieExpires);

        //    //if passed guid is empty set cookie as expired
        //    if (string.IsNullOrEmpty(userName))
        //        cookieExpiresDate = DateTime.Now.AddMonths(-1);

        //    //set new cookie value
        //    var options = new CookieOptions
        //    {
        //        HttpOnly = true,
        //        Expires = cookieExpiresDate,
        //        Secure = true
        //    };
        //    _contextAccessor.HttpContext.Response.Cookies.Append(cookieName, userName, options);
        //}
        protected virtual void SetUserCookie(string userId)
        {
            if (_contextAccessor.HttpContext?.Response == null)
                return;

            //delete current cookie value
            var cookieName = Constants.CookieNames.Sude_ManagementWork_Cookie;
            _contextAccessor.HttpContext.Response.Cookies.Delete(cookieName);

            //get date of cookie expiration
            var cookieExpires = 720;
            var cookieExpiresDate = DateTime.Now.AddHours(cookieExpires);

            //if passed guid is empty set cookie as expired
            if (string.IsNullOrEmpty(userId))
                cookieExpiresDate = DateTime.Now.AddMonths(-1);

            //set new cookie value
            var options = new CookieOptions
            {
                HttpOnly = true,
                Expires = cookieExpiresDate,
                Secure = true
            };
            _contextAccessor.HttpContext.Response.Cookies.Append(cookieName, userId, options);
        }


        public bool? IsAdmin
        {
            get
            {
                return _contextAccessor.HttpContext.Session.GetObject<bool>(Constants.SessionNames.IsAdmin);

            }
            set
            {
                _contextAccessor.HttpContext.Session.SetObject(Constants.SessionNames.IsAdmin, value);

            }
        }

        public UserInfo GetUserInfo(string userName,string password)
        {
            XmlUsers users = new XmlUsers();
            users.LoadFrom("\\Areas\\Admin\\user.config", _HostingEnvitonment);
         UserInfo userInfo=  users.Users.Where(u => u.userName.ToLower() == userName.ToLower() && u.password == password).FirstOrDefault();
            return userInfo;

        }

        private UserInfo GetUserInfoById(string id)
        {
            XmlUsers users = new XmlUsers();
            users.LoadFrom("\\Areas\\Admin\\user.config", _HostingEnvitonment);
            UserInfo userInfo = users.Users.Where(u => u.id == id).FirstOrDefault();
            return userInfo;

        }
        public UserInfo CurrentUser
        {
            get
            {
                
                UserInfo userInfo = _contextAccessor.HttpContext.Session.GetObject<UserInfo>(Constants.SessionNames.CurrentUser);
      

                if (userInfo == null)
                {
                    var userCookie = GetUserCookie();

                    if (Guid.TryParse(userCookie, out var userId))
                    {
                       
                        userInfo = GetUserInfoById(userCookie);
                        if (userInfo != null && userInfo.userName.ToLower() == "bamdad")
                            IsAdmin = true;
                    }

                    _contextAccessor.HttpContext.Session.SetObject(Constants.SessionNames.CurrentUser, userInfo);
                }
                return userInfo;

            }
            set
            {
                _contextAccessor.HttpContext.Session.SetObject(Constants.SessionNames.CurrentUser, value);
                if (value != null)
                    SetUserCookie(value.id);
                else
                    RemoveUserCookie();



            }
        }



        public IEnumerable<OrderDetailDetailDtoModel> CurrentOrderDetails
        {
            get
            {
                return _contextAccessor.HttpContext.Session.GetObject<IEnumerable<OrderDetailDetailDtoModel>>(Constants.SessionNames.OrderDetails);

            }
            set
            {


                _contextAccessor.HttpContext.Session.SetObject(Constants.SessionNames.OrderDetails, value);
            }
        }


        public WorkDetailDtoModel CurrentWork
        {
            get
            {
                return _contextAccessor.HttpContext.Session.GetObject<WorkDetailDtoModel>(Constants.SessionNames.CurrentWork);

            }
            set
            {


                _contextAccessor.HttpContext.Session.SetObject(Constants.SessionNames.CurrentWork, value);
            }
        }

        public string CurrentWorkId
        {
            get
            {
                return _contextAccessor.HttpContext.Session.GetString(Constants.SessionNames.CurrentWorkId);

            }
            set
            {
                if (value == null)
                {
                    _contextAccessor.HttpContext.Session.Remove(Constants.SessionNames.CurrentWorkId);
                }
                else
                    _contextAccessor.HttpContext.Session.SetString(Constants.SessionNames.CurrentWorkId, value);


            }
        }


        public string CurrentWorkName
        {
            get
            {
                return _contextAccessor.HttpContext.Session.GetString(Constants.SessionNames.CurrentWorkName);

            }
            set
            {
                if (value == null)
                {
                    _contextAccessor.HttpContext.Session.Remove(Constants.SessionNames.CurrentWorkName);
                }
                else
                    _contextAccessor.HttpContext.Session.SetString(Constants.SessionNames.CurrentWorkName, value);


            }
        }




        public TokenResponse RegisterTokenAccess
        {
            get
            {



                return _contextAccessor.HttpContext.Session.GetObject<TokenResponse>(Constants.SessionNames.RegisterTokenAccess);

            }
            set
            {
                _contextAccessor.HttpContext.Session.SetObject(Constants.SessionNames.RegisterTokenAccess, value);

            }
        }
        public string AccessToken
        {

            get
            {

                return _contextAccessor.HttpContext.Session.GetString(Constants.SessionNames.AccessToken);
            }
            set
            {

                _contextAccessor.HttpContext.Session.SetString(Constants.SessionNames.AccessToken, value);
            }
        }

        //public TokenResponse CurrentTokenAccess
        //{
        //    get
        //    {
        //        return _contextAccessor.HttpContext.Session.GetObject<TokenResponse>(Constants.SessionNames.CurrentTokenAccess);

        //    }
        //    set
        //    {
        //        _contextAccessor.HttpContext.Session.SetObject(Constants.SessionNames.CurrentTokenAccess, value.Json);

        //    }
        //}
        public HttpClient CurrentAccessHttpClient
        {
            get
            {

                var _client = new HttpClient()
                {
                    BaseAddress = new Uri("https://idpsts.somee.com/connect/token")
                };

                return _client;
            }

        }
        public TokenClient CurrentTokenClient
        {
            get
            {

                var tokenClient = new TokenClient(CurrentAccessHttpClient,
            new TokenClientOptions
            {
                ClientId = "ClientWithPassword",
                ClientSecret = "secret"
            });

                return tokenClient;
            }

        }


    }





}
