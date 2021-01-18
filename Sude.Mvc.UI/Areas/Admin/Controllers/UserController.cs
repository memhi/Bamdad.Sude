using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sude.Dto.DtoModels.Result;
using Sude.Mvc.UI.Admin.Models;
using Sude.Mvc.UI.ApiManagement;

namespace Sude.Mvc.UI.Admin.Controllers
{
    public class UserController : BaseAdminController
    {

        public readonly SudeSessionContext _sudeSessionContext;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserController(SudeSessionContext sudeSessionContext, IHttpContextAccessor httpContextAccessor)
        {
            _sudeSessionContext = sudeSessionContext;
            _httpContextAccessor = httpContextAccessor;

        }
        [HttpGet]//

        public async Task<ActionResult> ChangePassword( )
        {
            return PartialView(viewName: "ChangePassword", model: new ChangePasswordViewModel()
            {
                userId = _sudeSessionContext.CurrentUser.id,
                confirmPassword = "",
                currentPassword = "",
                password = ""
            });
        }

        // POST: UserController/Edit/5
        [HttpPost]
        public async Task<ActionResult> ChangePassword(  ChangePasswordViewModel changePassword)
        {

          

            //   UserInfo responseCreateUser;
            try
            {
                changePassword.userId = _sudeSessionContext.CurrentUser.id;
                TokenResponse tresponse = await _sudeSessionContext.CurrentTokenClient.RequestClientCredentialsTokenAsync("adminClient01_api");
                var responseCreateUser = await Api.GetHandler
                  .GetApiAsync<ResultSetDto<ChangePasswordViewModel>>(ApiAddress.IdentityServer.ChangePassword, changePassword, tresponse);

                if (responseCreateUser.IsSucceed)
                {


                    return Json(new ResultSetDto()
                    {
                        IsSucceed = true,
                        Message = ""
                    });


                  
                }
                else
                {

                    return Json(new ResultSetDto()
                    {
                        IsSucceed = false,
                        Message = responseCreateUser.Message
                    });

                }


            }
            catch (Exception ex)
            {
                 
                return Json(new ResultSetDto()
                {
                    IsSucceed = false,
                    Message = ex.Message
                });

            }
        }

      
    }
}
