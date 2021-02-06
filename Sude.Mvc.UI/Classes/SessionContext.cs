


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
using Sude.Dto.DtoModels.Result;
using Sude.Dto.DtoModels.Localization;
using Sude.Dto.DtoModels.Content;
using System.IO;

namespace Sude.Mvc.UI
{

    public class SudeSessionContext
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IWebHostEnvironment _HostingEnvitonment;
        public ICollection<LanguageDetailDtoModel> Languages;
        public ICollection<LocalStringResourceDetailDtoModel> LocalStringResources;
        public SudeSessionContext(IHttpContextAccessor contextAccessor, IWebHostEnvironment hostingEnvitonment)
        {
            _contextAccessor = contextAccessor;
            _HostingEnvitonment = hostingEnvitonment;
            if (Languages == null)
            {
                var result = Api.GetHandler
             .GetApiAsync<ResultSetDto<IEnumerable<LanguageDetailDtoModel>>>(ApiAddress.Localization.GetAllLanguages);
                if (result.Result.IsSucceed && result.Result.Data != null)
                {
                    Languages = result.Result.Data.ToList();
                }
            }

            if (LocalStringResources == null)
            {
                var result = Api.GetHandler
            .GetApiAsync<ResultSetDto<IEnumerable<LocalStringResourceDetailDtoModel>>>(ApiAddress.Localization.GetAllLocalResources);
                if (result.Result.IsSucceed && result.Result.Data != null)
                {
                    LocalStringResources = result.Result.Data.ToList();
                }
            }
        }

        public virtual void LogoutUser()
        {
            CurrentOrderDetails = null;
            CurrentWork = null;
            CurrentWorkId = null;
            CurrentWorkName = null;
            CurrentUser = null;
            UserWorks = null;
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

        public string SessionId
        {
            get
            {
                string SId= _contextAccessor.HttpContext.Session.GetString(Constants.SessionNames.SessionId);
                if (string.IsNullOrEmpty(SId))
                {
                    SId = Guid.NewGuid().ToString();
                    _contextAccessor.HttpContext.Session.SetString(Constants.SessionNames.SessionId,SId);
                }
                return SId;
            }
           
        }

        public string GetLocalResourceValue(string Name, params object[]  parameters)
        {


            LocalStringResourceDetailDtoModel localStringResource = LocalStringResources.FirstOrDefault(lr => lr.LanguageId.ToLower() == CurrentLanguage.LanguageId.ToLower() && lr.ResourceName.ToLower() == Name.ToLower());
            if (localStringResource == null)
                return Name;
            if (parameters != null && parameters.Any())
                return string.Format(localStringResource.ResourceValue, parameters);
            return localStringResource.ResourceValue;

        }

        public LanguageDetailDtoModel CurrentLanguage
        {
            get
            {

                LanguageDetailDtoModel lan = _contextAccessor.HttpContext.Session.GetObject<LanguageDetailDtoModel>(Constants.SessionNames.CurrentLanguage);


                if (lan == null)
                {

                 
                        lan = Languages.OrderBy(l=>l.DisplayOrder).FirstOrDefault();           

                    _contextAccessor.HttpContext.Session.SetObject(Constants.SessionNames.CurrentLanguage, lan);
                   
                      
                }
                return lan;

            }
            set
            {
                _contextAccessor.HttpContext.Session.SetObject(Constants.SessionNames.CurrentLanguage, value);


            }
        }

        public string GetAttachmentAddressFile(string workId,string pictureTypeId, string entityId, bool isEdit)
        {
            string workDirPath = Path.Combine(_HostingEnvitonment.WebRootPath, "WorkFiles", workId);
            if (!Directory.Exists(workDirPath))
                Directory.CreateDirectory(workDirPath);
            string attachmentsDirPath = Path.Combine(_HostingEnvitonment.WebRootPath, "WorkFiles", workId, pictureTypeId);
            if (!Directory.Exists(attachmentsDirPath))
                Directory.CreateDirectory(attachmentsDirPath);
            string fileDirPath = Path.Combine(_HostingEnvitonment.WebRootPath, "WorkFiles", workId, pictureTypeId, entityId);
            if (!Directory.Exists(fileDirPath))
                Directory.CreateDirectory(fileDirPath);
            else if (isEdit)
            {
                Directory.Delete(fileDirPath, true);
                Directory.CreateDirectory(fileDirPath);
            }

            return fileDirPath;


        }
     
        public bool MoveTempAttachmentFiles(string workId,string pictureTypeId, string entityId, bool isEdit)
        {
            List<AttachmentNewDtoModel> attachments = CurrentAttachmentPictures;
            if (attachments != null)
            {


                string fileDirPath = GetAttachmentAddressFile(workId, pictureTypeId, entityId, isEdit);
             

                foreach (AttachmentNewDtoModel attachment in attachments)
                {
                    string fileAddress = Path.Combine(_HostingEnvitonment.WebRootPath, attachment.AttachmentFileAddress);
                    FileInfo file = new FileInfo(fileAddress);
                    if (file.Exists)
                    {

                        file.MoveTo(Path.Combine(fileDirPath, attachment.Title));

                    }

                }
            }

            return true;
        }

        public bool CopyMainAttachmentFiles(List<AttachmentNewDtoModel> attachments)
        {
            try
            {

                var directorypath = Path.Combine( _HostingEnvitonment.WebRootPath, "TempUserAttachmentFiles", CurrentUser.id);
                if (!Directory.Exists(directorypath))
                    Directory.CreateDirectory(directorypath);
                foreach (AttachmentNewDtoModel attachment in attachments)
                {
                    string fileAddress = Path.Combine(_HostingEnvitonment.WebRootPath, attachment.AttachmentFileAddress);
                    FileInfo file = new FileInfo(fileAddress);
                    if (file.Exists)
                    {

                        file.CopyTo(Path.Combine(directorypath, attachment.Title));
                        attachment.AttachmentFileAddress = Path.Combine("TempUserAttachmentFiles", CurrentUser.id, attachment.Title);

                    }

                }

                CurrentAttachmentPictures = attachments;
                return true;
            }
            catch (Exception ex)
            {
                return false; ;

            }


        }

        public void DeletePictureTempFiles()
        {
            CurrentAttachmentPictures = null;
            string userDirectoryPath = Path.Combine(_HostingEnvitonment.WebRootPath, "TempUserAttachmentFiles", CurrentUser.id);
            if (Directory.Exists(userDirectoryPath))
            {
                Directory.Delete(userDirectoryPath, true);

            }

        }
        public UserInfo GetUserInfo(string userName, string password)
        {
            XmlUsers users = new XmlUsers();
            users.LoadFrom("\\Areas\\Admin\\user.config", _HostingEnvitonment);
            UserInfo userInfo = users.Users.Where(u => u.userName.ToLower() == userName.ToLower() && u.password == password).FirstOrDefault();
            return userInfo;

        }

        private UserInfo GetUserInfoById(Guid userId)
        {


            TokenResponse tresponse = CurrentTokenClient.RequestClientCredentialsTokenAsync("adminClient01_api").Result;
            var resultuser = Api.GetHandler
          .GetApiAsync<ResultSetDto<UserInfo>>(ApiAddress.IdentityServer.GetUserInfoByUerId + userId.ToString(), tresponse);
            UserInfo userInfo = null;
            if (resultuser != null && resultuser.Result != null && resultuser.Result.Data != null)
            {
                userInfo = resultuser.Result.Data;
            }
            //XmlUsers users = new XmlUsers();
            //users.LoadFrom("\\Areas\\Admin\\user.config", _HostingEnvitonment);
            //UserInfo userInfo = users.Users.Where(u => u.id == userId.ToString()).FirstOrDefault();
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





                        userInfo = GetUserInfoById(userId);
                        if (userInfo != null && userInfo.userName.ToLower() == "bamdad")
                            IsAdmin = true;
                    }
                    if (userInfo != null)
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



        public List<AttachmentNewDtoModel> CurrentAttachmentPictures
        {
            get
            {
                return _contextAccessor.HttpContext.Session.GetObject<List<AttachmentNewDtoModel>>(Constants.SessionNames.AttachmentPictures);

            }
            set
            {


                _contextAccessor.HttpContext.Session.SetObject(Constants.SessionNames.AttachmentPictures, value);
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



        public List<WorkDetailDtoModel> UserWorks
        {
            get
            {
                List<WorkDetailDtoModel> works = _contextAccessor.HttpContext.Session.GetObject<List<WorkDetailDtoModel>>(Constants.SessionNames.UserWorks);
                if ((works == null || !works.Any()) && CurrentUser != null)
                {
                    ResultSetDto<IEnumerable<WorkDetailDtoModel>> workListResult = Api.GetHandler
   .GetApiAsync<ResultSetDto<IEnumerable<WorkDetailDtoModel>>>(ApiAddress.Work.GetWorksByUserId + CurrentUser.id).Result;


                    if (workListResult.IsSucceed && workListResult.Data != null)
                    {
                        works = new List<WorkDetailDtoModel>();
                        foreach (WorkDetailDtoModel workDetailDto in workListResult.Data)
                            works.Add(workDetailDto);
                        _contextAccessor.HttpContext.Session.SetObject(Constants.SessionNames.UserWorks, works);
                    }


                }
                return works;


            }
            set
            {


                _contextAccessor.HttpContext.Session.SetObject(Constants.SessionNames.UserWorks, value);
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
                    BaseAddress = new Uri(ApiManagement.ApiAddress.IdpTokenAddress)
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
