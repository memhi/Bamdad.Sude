using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sude.Dto.DtoModels.Order;
using Sude.Dto.DtoModels.Result;
using Sude.Mvc.UI.Admin.Models;
using Sude.Mvc.UI.ApiManagement;

namespace Sude.Mvc.UI.Admin.Controllers
{
    [Area("Admin")]
    public class LoginController : Controller
    {

        public readonly SudeSessionContext _sudeSessionContext;

        private readonly IHttpContextAccessor  _httpContextAccessor;

        public LoginController(SudeSessionContext sudeSessionContext, IHttpContextAccessor httpContextAccessor)
        {
            _sudeSessionContext = sudeSessionContext;
            _httpContextAccessor = httpContextAccessor;



        }
        public IActionResult Index()
        {
            return View();
        }

       
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel request)
        {



            /////// For IDP
            TokenResponse tokenResponse = await _sudeSessionContext.CurrentTokenClient.RequestPasswordTokenAsync(request.Username, request.Password, "adminClient01_api");


            if (tokenResponse.IsError)
            {

                return Json(new ResultSetDto()
                {
                    IsSucceed = false,
                    Message = tokenResponse.Error
                });
            }
            _sudeSessionContext.AccessToken = tokenResponse.AccessToken;
            UserInfo userInfo = new UserInfo
            {
                userName = request.Username
            };
            var resultuser = await Api.GetHandler
        .GetApiAsync<ResultSetDto<UserInfo>>(ApiAddress.IdentityServer.GetUserInfoByUerName + request.Username, tokenResponse);

            //    UserInfo resultuser = _sudeSessionContext.GetUserInfo(request.Username, request.Password);



            if (resultuser==null || resultuser.Data==null ||  string.IsNullOrEmpty(resultuser.Data.id))
            {
         
                return Json(new ResultSetDto()
                {
                    IsSucceed = false,
                    Message = "خطا در بازیابی اطلاعات کاربر"
                });

            }
            else
            {

                _sudeSessionContext.CurrentUser = resultuser.Data;
                if (resultuser.Data.userName.ToLower() == "bamdad")
                    _sudeSessionContext.IsAdmin = true;
                ResultSetDto resultSet = new ResultSetDto()
                {
                    IsSucceed = true,
                    Message = ""

                };

                return Json(resultSet);
               
             
            }
        }


        
        [HttpPost]
        public async Task<ActionResult> Logout(LoginViewModel request)
        {


            _sudeSessionContext.LogoutUser();


            return Json(new ResultSetDto()
            {
                IsSucceed = true,
                Message = ""
            });



        }



        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegisterViewModel request)
        {
            if (!ModelState.IsValid)
            {
                string message = "";
                foreach (var er in ModelState.Values.SelectMany(modelstate => modelstate.Errors))
                    message += er.ErrorMessage + " \n";

                return Json(new ResultSetDto()
                {
                    IsSucceed = false,
                    Message = message
                });
            }

            if(request.Password!=request.ConfirmPassword)
            {
                return Json(new ResultSetDto()
                {
                    IsSucceed = false,
                    Message = "تکرار کلمه عبور صحیح نیست"
                });

            }


            UserInfo userInfo = new UserInfo()
            {
             
                userName = request.Phone,
                name = request.Name,
                phoneNumber = request.Phone,
                password=request.Password,
                confirmPassword=request.ConfirmPassword,
                email=Guid.NewGuid().ToString().Replace("-","")+"test@Test.com"

            };

         //   UserInfo responseCreateUser;
            try
            {
                TokenResponse tresponse = await _sudeSessionContext.CurrentTokenClient.RequestClientCredentialsTokenAsync("adminClient01_api");
              var  responseCreateUser = await Api.GetHandler
                .GetApiAsync<ResultSetDto<UserInfo>>(ApiAddress.IdentityServer.RegisterService, userInfo, tresponse);

                if (responseCreateUser.IsSucceed && responseCreateUser.Data!=null)
                {


                    return Json(new ResultSetDto()
                    {
                        IsSucceed = true,
                        Message = "کاربر ایجاد شد، لطفا با نام کاربری و کلمه عبور وارد شوید"
                    });


                    //UserPassword userPassword = new UserPassword
                    //{
                    //    userId = responseCreateUser.id,
                    //    password = request.Password,
                    //    confirmPassword = request.ConfirmPassword
                    //};


                    //var responsechangePass = await Api.GetHandler
                    //.GetApiAsync<HttpContent>(ApiAddress.IdentityServer.ChangePassword, userPassword, tresponse);
                    //if (responsechangePass!=null)
                    //{
                    //    ModelState.AddModelError("", "changer Pass User: " + responsechangePass.ToString());
                    //}
                }
                else
                {

                    return Json(new ResultSetDto()
                    {
                        IsSucceed = false,
                        Message =" اطلاعات ثبت نام را صحیح وارد کنید"
                    });

                }

           
            }
            catch(Exception ex)
            {
                if(string.IsNullOrEmpty(request.Email))
                {



                }
                return Json(new ResultSetDto()
                {
                    IsSucceed = false,
                    Message = ex.Message
                });

            }
        }
    }
}
