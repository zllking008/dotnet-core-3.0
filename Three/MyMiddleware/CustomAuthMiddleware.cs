using AspNetCoreCurrentRequestContext;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MyMiddleware
{
    public class CustomAuthMiddleware:IMiddleware
    {
        //private readonly RequestDelegate _next;
        //public CustomAuthMiddleware(RequestDelegate next)
        //{
        //    _next = next;
        //}

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var name = context.Request.Query["name"];
            var id = context.Request.Query["id"];
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(id))
            {
                await next.Invoke(context);
                return;
            }
            //var data = UserService.IsLogin;
            if (!UserService.IsLogin)
            {
                User user = new User() { ID = int.Parse(id), Name = name, LoginTime = DateTime.Now };
                await UserService.SignInAsync(user);
                //await SignInAsync(user);

                context.Request.Query.Append(new KeyValuePair<string, Microsoft.Extensions.Primitives.StringValues>("key",new StringValues("1234")) );
            }
            await next.Invoke(context);
        }

        private async Task SignInAsync(User model)
        {
            await AspNetCoreHttpContext.Current.SignOutAsync(UserService.userSignInIdentity);

            var claims = new[] {
                new Claim(ClaimTypes.Name, model.Name),
                new Claim(ClaimTypes.NameIdentifier, model.ID.ToString()),
                new Claim(ClaimTypes.UserData, Newtonsoft.Json.JsonConvert.SerializeObject(model))
            };
            //var userPrincipal = new ClaimsPrincipal (new ClaimsIdentity (claims, "userLogin"));
            var userPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims));

            var task = AspNetCoreHttpContext.Current.SignInAsync(UserService.userSignInIdentity, userPrincipal, new AuthenticationProperties
            {
                ExpiresUtc = DateTime.UtcNow.AddDays(1), //票据缓存时间
                IsPersistent = false,
                AllowRefresh = false
            });
        }

        private async Task<string> GetAccountInfo(string claimType)
        {
            var t = await AspNetCoreHttpContext.Current.AuthenticateAsync(UserService.userSignInIdentity);
            //AspNetCoreCompatibility.CompatibilityHttpContextAccessor.Current.AuthenticateAsync(userSignInIdentity);
            if (t.Succeeded)
            {
                ClaimsIdentity _identity = (ClaimsIdentity)t.Principal.Identity;
                var userData = _identity.FindFirst(claimType);
                if (string.IsNullOrEmpty(userData.Value)) return null;
                return userData.Value;

            }
            return null;
        }

    }

    public static class CustomAuthMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomAuth(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomAuthMiddleware>();
        }
    }
}
