using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Sude.Mvc.UI.Admin;

namespace Sude.Mvc.UI
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class CheckSessionMiddleware
    {
        private readonly RequestDelegate _next;
        public readonly SudeSessionContext _sudeSessionContext;
        public CheckSessionMiddleware(RequestDelegate next, SudeSessionContext sudeSessionContext)
        {
            _sudeSessionContext = sudeSessionContext;
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
   
           
            if (httpContext.Request.Path.StartsWithSegments("/Admin")==true)

            {



                if (_sudeSessionContext.CurrentUser == null && !httpContext.Request.Path.StartsWithSegments("/Admin/Login"))
                {
                    httpContext.Request.Path = "/Admin/Login";
                    httpContext.Response.Redirect("/Admin/Login");
                }
                else
                {
                    if(_sudeSessionContext.CurrentUser!=null  &&(_sudeSessionContext.UserWorks==null || _sudeSessionContext.UserWorks.Count()<0) && !httpContext.Request.Path.StartsWithSegments("/Admin/Work")  && !httpContext.Request.Path.StartsWithSegments("/Admin/Login"))
                    {

                        httpContext.Request.Path = "/Admin/Work";
                        httpContext.Response.Redirect("/Admin/Work");
                    }
                    await _next(httpContext);
                }

                 
                  

               
              
            }
            else
            {
                await _next(httpContext);
            }

        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class CheckSessionMiddlewareExtensions
    {
        public static IApplicationBuilder UseCheckSessionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CheckSessionMiddleware>();
        }
    }
}
