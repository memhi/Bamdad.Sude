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

            TokenResponse tokenResponse = await _sudeSessionContext.CurrentTokenClient.RequestPasswordTokenAsync(request.Username, request.Password, "adminClient01_api");

            _sudeSessionContext.AccessToken = tokenResponse.AccessToken;
            if (tokenResponse.IsError)
            {
                ModelState.AddModelError("", tokenResponse.Error);
                return View(request);
            }
            UserInfo userInfo = new UserInfo
            {
                userName = request.Username
            };
            var resultuser = await Api.GetHandler
        .GetApiAsync<UserInfo>(ApiAddress.IdentityServer.GetUserInfoByUerName + request.Username, tokenResponse);

            if (string.IsNullOrEmpty(resultuser.id))
            {
                ModelState.AddModelError("", "خطا در بازیابی اطلاعات کاربر");
                return View(request);


            }
            else
            {

                _sudeSessionContext.CurrentUser = resultuser;
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
                email = request.Email,
                userName = request.Username,
                name = request.Name,
                phoneNumber = request.Phone

            };

            UserInfo responseCreateUser;
            try
            {
                TokenResponse tresponse = await _sudeSessionContext.CurrentTokenClient.RequestClientCredentialsTokenAsync("adminClient01_api");
                responseCreateUser = await Api.GetHandler
                .GetApiAsync<UserInfo>(ApiAddress.IdentityServer.RegisterService, userInfo, tresponse);

                if (responseCreateUser != null)
                {
                    UserPassword userPassword = new UserPassword
                    {
                        userId = responseCreateUser.id,
                        password = request.Password,
                        confirmPassword = request.ConfirmPassword
                    };


                    var responsechangePass = await Api.GetHandler
                    .GetApiAsync<HttpContent>(ApiAddress.IdentityServer.ChangePassword, userPassword, tresponse);
                    if (responsechangePass!=null)
                    {
                        ModelState.AddModelError("", "changer Pass User: " + responsechangePass.ToString());
                    }
                }

                return View(request);
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
