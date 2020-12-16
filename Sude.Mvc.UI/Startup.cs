using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Logging;

namespace Sude.Mvc.UI
{
    public class Startup
    {
        public IWebHostEnvironment Env { get; set; }
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration,IWebHostEnvironment env)
        {
            Configuration = configuration;
            Env = env;
            Sude.Mvc.UI.ApiManagement.ApiAddress.ServerAddress = configuration.GetSection("AppSettings").GetSection("ApiAddress").Value.ToString(); ;
        }

   

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            // services.AddProgressiveWebApp();
            services.AddServerSideBlazor();
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            IdentityModelEventSource.ShowPII = true;
            services.AddSession();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            IMvcBuilder builder = services.AddRazorPages();

 
            if (Env.IsDevelopment())
            {
                builder.AddRazorRuntimeCompilation();
            }

            //services.AddAuthentication(options =>
            //{
            //    options.DefaultScheme = "Cookies";
            //    options.DefaultChallengeScheme = "oidc";
            //})
            //    .AddCookie("Cookies")
            //   .AddOpenIdConnect("oidc", options =>
            //    {
            //        options.Authority = "https://bamdadserver:8080";
            //        options.ClientId = "Sude";
            //        options.ClientSecret = "secret";
            //        options.Scope.Add("openid");
            //        options.Scope.Add("profile");
            //        options.Scope.Add("Sude.Api");
            //        options.ResponseType = "code";
            //        options.UsePkce = true;
            //        options.RequireHttpsMetadata = false;

            //    });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseStatusCodePages();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseSession();
            app.UseCheckSessionMiddleware();
            //   app.UseAuthentication();
            //    app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                   name: "blog",
                   pattern: "Blog/{UrlAddress?}",
                    defaults: new { controller = "Blog", action = "DetailByUrl", });
                endpoints.MapControllerRoute(name: "areaRoute",
               pattern: "{area}/{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
