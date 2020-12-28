


using Microsoft.AspNetCore.Http;

using Sude.Mvc.UI.Admin.Models;
using IdentityModel.Client;
using System.Net.Http;
using System;
using Sude.Mvc.UI.ApiManagement;
using Sude.Dto.DtoModels.Work;
using Sude.Dto.DtoModels.Order;
using System.Collections.Generic;

namespace Sude.Mvc.UI.Admin
{

    public class SudeSessionContext
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public SudeSessionContext(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public virtual void LogoutUser()
        {
            CurrentOrderDetails = null;
            CurrentWork = null;
            CurrentWorkId = null;
            CurrentWorkName = null;
            CurrentUser = null;
   

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

                        TokenResponse tresponse = CurrentTokenClient.RequestClientCredentialsTokenAsync("adminClient01_api").Result;
                        var resultuser = Api.GetHandler
                      .GetApiAsync<UserInfo>(ApiAddress.IdentityServer.GetUserInfoByUerId + userId.ToString(), tresponse);
                        //get customer from cookie (should not be registered)
                        userInfo = resultuser.Result;
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
                    BaseAddress = new Uri("https://bamdadserver:8080/connect/token")
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
