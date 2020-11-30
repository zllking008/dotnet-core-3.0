using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.CodeAnalysis.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace MyMiddleware
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddSingleton<MyMiddleware>();
            
            services.AddRazorPages();
            services.AddHttpContextAccessor();
            services.AddSession();
            services.AddHttpClient();

            services.AddMvc();
            services.AddAuthorization();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = UserService.userSignInIdentity;
                options.DefaultSignInScheme = UserService.userSignInIdentity;
                options.DefaultChallengeScheme = UserService.userSignInIdentity;
                options.RequireAuthenticatedSignIn = false;
                
            }).AddCookie(UserService.userSignInIdentity,options=> {
                options.LoginPath =new PathString("/user/login");
                options.ReturnUrlParameter = "returnUrl";
                options.LogoutPath = new PathString("/user/logout");
                options.Cookie.Name = CookieAuthenticationDefaults.AuthenticationScheme;
                options.SlidingExpiration = true;
                options.AccessDeniedPath = new PathString("/user/AccessDenied");
                options.Cookie.Path = "/";
                options.ExpireTimeSpan = TimeSpan.FromDays(1);
                options.Events.OnRedirectToLogin = context =>
                {
                    
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    return Task.CompletedTask;
                };

            });
            
            services.AddSingleton<CustomAuthMiddleware>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            //app.UseRequestCulture();

            //app.Run(async context=> {
            //    context.Response.ContentType = "text/html;charset=utf-8";
            //    await context.Response.WriteAsync(
            //        $"Hello {CultureInfo.CurrentCulture.DisplayName}"
            //        );
            //});

            //app.Map("/Route1", Route1);
            //app.Map("/Route2", Route2);
            //app.Map("/Route3", route3 => {


            //    route3.Map("/route3a", route3a =>{
            //        route3a.Run(async context =>
            //        {
            //            await context.Response.WriteAsync("/route3/route3a");
            //        });
            //    });
            //    route3.Map("/route3b",route3b=> {
            //        route3b.Run(async context => {
            //            await context.Response.WriteAsync("/route3/route3b");
            //        });
            //    });
            //    route3.Run(async context => {
            //        await context.Response.WriteAsync("/route3");
            //    });

            //});
            //app.Use(async (context, next) => {
            //    await next.Invoke();
            //});
            //app.Use(next => context => {
            //   return next(context);
            //});

            //app.UseMiddleware<MyMiddleware>();
            //app.Run(async (context) => {
            //    await context.Response.WriteAsync("Hello there!");
            //});

            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseRouting();
            app.UseCurrentRequestContext();
            //app.UseIdentityServer();
            app.UseAuthorization();
            app.UseAuthentication();
            //app.Map("/userLogin",route=> {
            //    route.Run(async context =>
            //    {

            //        await context.Response.WriteAsync("userlogin111");
            //    });
            //});
            //app.UseCustomAuth();

            app.UseWhen(context => context.Request.Path.StartsWithSegments("/user/Login"), appBuilder =>
            {
                appBuilder.UseCustomAuth();
            });

            app.UseEndpoints(endpoints =>
            {

                //endpoints.MapGet("/", async context =>
                //{
                //    await context.Response.WriteAsync("Hello");
                //});
                endpoints.MapControllers();
                endpoints.MapControllerRoute(
                    "default",
                    "{controller=user}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }

        private void Route1(IApplicationBuilder app)
        {
            app.Run(async (context)=> {
                await context.Response.WriteAsync("Route1");
            });
        }

        private void Route2(IApplicationBuilder app)
        {
            app.Run(async (context) => {
                await context.Response.WriteAsync("Route2");
            });
        }

        private void Route3(IApplicationBuilder app)
        {
            app.Use(async (context,next) =>
            {
                await next.Invoke();
            });
        }
    }
}
