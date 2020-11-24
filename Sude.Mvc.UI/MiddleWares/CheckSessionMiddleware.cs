using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Sude.Mvc.UI
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class CheckSessionMiddleware
    {
        private readonly RequestDelegate _next;

        public CheckSessionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
   
           
            if (httpContext.Request.Path.StartsWithSegments("/Admin")==true && httpContext.Request.Path.StartsWithSegments("/Admin/Work")==false)

            {


           // string CurrentWorkId= httpContext.Session.GetString("CurrentWorkId");
            //    if(string.IsNullOrEmpty(CurrentWorkId))
              //  {

             
                

               //     await httpContext.Response.WriteAsync("لطفا یک کسب و کار انتخاب کنید");
              //  }

             //   else

                await _next(httpContext);

                 
                  

               
              
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
